using Microsoft.EntityFrameworkCore;
using Wallet.Application.Interfaces;
using Wallet.DomainLayer.Entities;
using Wallet.Infrastructure.Persistence;

namespace Wallet.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly WalletDbContext _context;

    public TransactionRepository(WalletDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Transaction transaction)
    {
        await _context.Transactions.AddAsync(transaction);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
    public async Task<List<Transaction>> GetByWalletIdAsync(int walletId)
    {
        return await _context.Transactions
            .Where(t => t.WalletId == walletId)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

}
