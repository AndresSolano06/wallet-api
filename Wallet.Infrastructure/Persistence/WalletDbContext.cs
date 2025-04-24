using Microsoft.EntityFrameworkCore;
using WalletEntity = Wallet.DomainLayer.Entities.Wallet;
using TransactionEntity = Wallet.DomainLayer.Entities.Transaction;

namespace Wallet.Infrastructure.Persistence
{
    public class WalletDbContext : DbContext
    {
        public WalletDbContext(DbContextOptions<WalletDbContext> options) : base(options) { }

        public DbSet<WalletEntity> Wallets => Set<WalletEntity>();
        public DbSet<TransactionEntity> Transactions => Set<TransactionEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WalletEntity>(entity =>
            {
                entity.HasKey(w => w.Id);
                entity.Property(w => w.Name).IsRequired().HasMaxLength(100);
                entity.Property(w => w.DocumentId).IsRequired().HasMaxLength(20);
                entity.Property(w => w.Balance).HasPrecision(18, 2);
            });

            modelBuilder.Entity<TransactionEntity>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Amount).HasPrecision(18, 2);
                entity.Property(t => t.Type).IsRequired();
                entity.HasOne(t => t.Wallet)
                      .WithMany(w => w.Transactions)
                      .HasForeignKey(t => t.WalletId);
            });
        }
    }
}
