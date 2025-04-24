using Wallet.DomainLayer.Entities;

namespace Wallet.Application.Interfaces;

public interface ITransactionRepository
{
    Task AddAsync(Transaction transaction);
    Task SaveChangesAsync(); 
    Task<List<Transaction>> GetByWalletIdAsync(int walletId);

}
