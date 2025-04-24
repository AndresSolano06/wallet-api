using Wallet.Application.Transactions.Commands;
using Wallet.Application.Interfaces;
using Wallet.DomainLayer.Entities;

namespace Wallet.Application.Transactions.Handlers;

public class TransferHandler
{
    private readonly IWalletRepository _walletRepository;
    private readonly ITransactionRepository _transactionRepository;

    public TransferHandler(IWalletRepository walletRepository, ITransactionRepository transactionRepository)
    {
        _walletRepository = walletRepository;
        _transactionRepository = transactionRepository;
    }

    public async Task HandleAsync(TransferRequest request)
    {
        if (request.Amount <= 0)
            throw new ArgumentException("El monto debe ser mayor a 0.");

        if (request.FromWalletId == request.ToWalletId)
            throw new ArgumentException("No se puede transferir a la misma billetera.");

        var fromWallet = await _walletRepository.GetByIdAsync(request.FromWalletId);
        var toWallet = await _walletRepository.GetByIdAsync(request.ToWalletId);

        if (fromWallet is null)
            throw new ArgumentException("La billetera origen no existe.");

        if (toWallet is null)
            throw new ArgumentException("La billetera destino no existe.");

        if (fromWallet.Balance < request.Amount)
            throw new InvalidOperationException("Saldo insuficiente en la billetera origen.");

        // Actualizar saldos
        fromWallet.Balance -= request.Amount;
        fromWallet.UpdatedAt = DateTime.UtcNow;

        toWallet.Balance += request.Amount;
        toWallet.UpdatedAt = DateTime.UtcNow;

        // Registrar movimientos
        var now = DateTime.UtcNow;

        await _transactionRepository.AddAsync(new Transaction
        {
            WalletId = fromWallet.Id,
            Amount = request.Amount,
            Type = "Débito",
            CreatedAt = now
        });

        await _transactionRepository.AddAsync(new Transaction
        {
            WalletId = toWallet.Id,
            Amount = request.Amount,
            Type = "Crédito",
            CreatedAt = now
        });

        await _walletRepository.SaveChangesAsync();
        await _transactionRepository.SaveChangesAsync();
    }
}
