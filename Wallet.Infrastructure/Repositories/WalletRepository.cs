using Wallet.Application.Interfaces;
using Wallet.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using WalletEntity = Wallet.DomainLayer.Entities.Wallet;


namespace Wallet.Infrastructure.Repositories;

public class WalletRepository : IWalletRepository
{
    private readonly WalletDbContext _context;

    public WalletRepository(WalletDbContext context)
    {
        _context = context;
    }

    public async Task<WalletEntity> AddAsync(WalletEntity wallet)
    {
        _context.Wallets.Add(wallet);
        return wallet;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<WalletEntity?> GetByIdAsync(int id)
    {
        return await _context.Wallets.FindAsync(id);
    }

}
