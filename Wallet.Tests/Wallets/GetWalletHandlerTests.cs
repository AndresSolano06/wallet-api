using Xunit;
using Moq;
using FluentAssertions;
using Wallet.Application.Interfaces;
using Wallet.Application.Wallets.Handlers;
using WalletEntity = Wallet.DomainLayer.Entities.Wallet;
using Wallet.Application.Wallets.Responses;

namespace Wallet.Tests.Wallets;

public class GetWalletHandlerTests
{
    private readonly Mock<IWalletRepository> _walletRepoMock;
    private readonly GetWalletHandler _handler;

    public GetWalletHandlerTests()
    {
        _walletRepoMock = new Mock<IWalletRepository>();
        _handler = new GetWalletHandler(_walletRepoMock.Object);
    }

    [Fact]
    public async Task HandleAsync_Should_Return_Null_When_Wallet_Not_Found()
    {
        int walletId = 99;

        _walletRepoMock.Setup(r => r.GetByIdAsync(walletId))
            .ReturnsAsync((WalletEntity?)null);

        Console.WriteLine($"[TEST] Buscando billetera inexistente con ID = {walletId}");

        var result = await _handler.HandleAsync(walletId);

        result.Should().BeNull();
    }

    [Fact]
    public async Task HandleAsync_Should_Return_Wallet_When_Found()
    {
        int walletId = 1;

        var entity = new WalletEntity
        {
            Id = walletId,
            Name = "Test User",
            DocumentId = "123456789",
            Balance = 100,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _walletRepoMock.Setup(r => r.GetByIdAsync(walletId)).ReturnsAsync(entity);

        Console.WriteLine($"[TEST] Buscando billetera válida con ID = {walletId}");

        var result = await _handler.HandleAsync(walletId);

        result.Should().NotBeNull();
        result!.Id.Should().Be(walletId);
        result.Name.Should().Be("Test User");
        result.DocumentId.Should().Be("123456789");
        result.Balance.Should().Be(100);
    }
}
