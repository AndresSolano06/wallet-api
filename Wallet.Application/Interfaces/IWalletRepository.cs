using WalletEntity = Wallet.DomainLayer.Entities.Wallet;

namespace Wallet.Application.Interfaces;

public interface IWalletRepository
{
    Task<WalletEntity> AddAsync(WalletEntity wallet);
    Task SaveChangesAsync();
    Task<WalletEntity?> GetByIdAsync(int id);

}
