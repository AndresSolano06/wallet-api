using Moq;
using Xunit;
using FluentAssertions;
using Wallet.Application.Interfaces;
using Wallet.Application.Transactions.Commands;
using Wallet.Application.Transactions.Handlers;
using WalletEntity = Wallet.DomainLayer.Entities.Wallet;
using TransactionEntity = Wallet.DomainLayer.Entities.Transaction;

namespace Wallet.Tests.Transactions;

public class TransferHandlerTests
{
    private readonly Mock<IWalletRepository> _walletRepoMock = new();
    private readonly Mock<ITransactionRepository> _transactionRepoMock = new();
    private readonly TransferHandler _handler;

    public TransferHandlerTests()
    {
        _handler = new TransferHandler(_walletRepoMock.Object, _transactionRepoMock.Object);
    }

    [Fact]
    public async Task HandleAsync_Should_Throw_When_SameWallet()
    {
        var command = new TransferRequest
        {
            FromWalletId = 1,
            ToWalletId = 1,
            Amount = 100
        };

        Console.WriteLine($"[TEST] Verificando error por misma billetera: {command.FromWalletId}");

        var act = () => _handler.HandleAsync(command);
        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("*misma billetera*");
    }

    [Fact]
    public async Task HandleAsync_Should_Throw_When_Balance_Insufficient()
    {
        _walletRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(new WalletEntity { Id = 1, Balance = 50 });
        _walletRepoMock.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(new WalletEntity { Id = 2 });

        var command = new TransferRequest
        {
            FromWalletId = 1,
            ToWalletId = 2,
            Amount = 100
        };

        Console.WriteLine($"[TEST] Verificando error por saldo insuficiente. Saldo actual: 50, Monto: {command.Amount}");

        var act = () => _handler.HandleAsync(command);
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("*Saldo insuficiente*");
    }

    [Fact]
    public async Task HandleAsync_Should_Work_When_Valid()
    {
        var from = new WalletEntity { Id = 1, Balance = 200 };
        var to = new WalletEntity { Id = 2, Balance = 100 };

        _walletRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(from);
        _walletRepoMock.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(to);

        var command = new TransferRequest
        {
            FromWalletId = 1,
            ToWalletId = 2,
            Amount = 50
        };

        Console.WriteLine($"[TEST] Antes de transferir: FROM = {from.Balance}, TO = {to.Balance}");

        await _handler.HandleAsync(command);

        Console.WriteLine($"[TEST] Después de transferir: FROM = {from.Balance}, TO = {to.Balance}");

        from.Balance.Should().Be(150);
        to.Balance.Should().Be(150);
        _walletRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        _transactionRepoMock.Verify(r => r.AddAsync(It.IsAny<TransactionEntity>()), Times.Exactly(2));
    }
}
