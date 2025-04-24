using Xunit;
using Moq;
using FluentAssertions;
using WalletEntity = Wallet.DomainLayer.Entities.Wallet;
using Wallet.Application.Wallets.Handlers;
using Wallet.Application.Wallets.Commands;
using Wallet.Application.Interfaces;

namespace Wallet.Tests.Wallets;

public class RechargeWalletHandlerTests
{
    private readonly Mock<IWalletRepository> _walletRepoMock = new();
    private readonly RechargeWalletHandler _handler;

    public RechargeWalletHandlerTests()
    {
        _handler = new RechargeWalletHandler(_walletRepoMock.Object);
    }

    [Fact]
    public async Task Should_Throw_When_Amount_Is_Zero()
    {
        var command = new RechargeWalletRequest { WalletId = 1, Amount = 0 };

        var act = async () => await _handler.HandleAsync(command);
        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("*mayor a 0*");
    }

    [Fact]
    public async Task Should_Throw_When_Wallet_Not_Found()
    {
        _walletRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((WalletEntity?)null);

        var command = new RechargeWalletRequest { WalletId = 1, Amount = 100 };
        var act = async () => await _handler.HandleAsync(command);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage("*Wallet no encontrada*");
    }

    [Fact]
    public async Task Should_Recharge_When_Valid()
    {
        var wallet = new WalletEntity { Id = 1, Balance = 200 };
        _walletRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(wallet);

        var command = new RechargeWalletRequest { WalletId = 1, Amount = 50 };
        await _handler.HandleAsync(command);

        wallet.Balance.Should().Be(250);
        _walletRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }
}
