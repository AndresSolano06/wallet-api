using Wallet.Application.Interfaces;
using Wallet.Application.Wallets.Responses;
using WalletEntity = Wallet.DomainLayer.Entities.Wallet;

namespace Wallet.Application.Wallets.Handlers;

public class GetWalletHandler
{
    private readonly IWalletRepository _walletRepository;

    public GetWalletHandler(IWalletRepository walletRepository)
    {
        _walletRepository = walletRepository;
    }

    public async Task<WalletResponse?> HandleAsync(int id)
    {
        var wallet = await _walletRepository.GetByIdAsync(id);

        if (wallet is null)
            return null;

        return new WalletResponse
        {
            Id = wallet.Id,
            DocumentId = wallet.DocumentId,
            Name = wallet.Name,
            Balance = wallet.Balance,
            CreatedAt = wallet.CreatedAt,
            UpdatedAt = wallet.UpdatedAt
        };
    }
}
