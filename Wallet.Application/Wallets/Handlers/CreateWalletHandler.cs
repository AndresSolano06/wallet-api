using Wallet.Application.Wallets.Commands;
using Wallet.Application.Wallets.Responses;
using Wallet.Application.Interfaces;
using WalletEntity = Wallet.DomainLayer.Entities.Wallet;

namespace Wallet.Application.Wallets.Handlers;

public class CreateWalletHandler
{
    private readonly IWalletRepository _walletRepository;

    public CreateWalletHandler(IWalletRepository walletRepository)
    {
        _walletRepository = walletRepository;
    }

    public async Task<WalletResponse> HandleAsync(CreateWalletRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.DocumentId))
            throw new ArgumentException("Name and DocumentId are required");

        var wallet = new WalletEntity
        {
            DocumentId = request.DocumentId,
            Name = request.Name,
            Balance = 0,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var created = await _walletRepository.AddAsync(wallet);
        await _walletRepository.SaveChangesAsync();

        return new WalletResponse
        {
            Id = created.Id,
            DocumentId = created.DocumentId,
            Name = created.Name,
            Balance = created.Balance,
            CreatedAt = created.CreatedAt,
            UpdatedAt = created.UpdatedAt
        };
    }
}
