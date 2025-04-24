using Wallet.Application.Interfaces;
using Wallet.Application.Wallets.Commands;

namespace Wallet.Application.Wallets.Handlers;

public class RechargeWalletHandler
{
    private readonly IWalletRepository _walletRepository;

    public RechargeWalletHandler(IWalletRepository walletRepository)
    {
        _walletRepository = walletRepository;
    }

    public async Task HandleAsync(RechargeWalletRequest request)
    {
        if (request.Amount <= 0)
            throw new ArgumentException("La recarga debe ser mayor a 0");

        var wallet = await _walletRepository.GetByIdAsync(request.WalletId);
        if (wallet is null)
            throw new KeyNotFoundException("Wallet no encontrada");

        wallet.Balance += request.Amount;
        wallet.UpdatedAt = DateTime.UtcNow;

        await _walletRepository.SaveChangesAsync();
    }
}
