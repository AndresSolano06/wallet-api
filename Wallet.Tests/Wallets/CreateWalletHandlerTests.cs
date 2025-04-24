using Xunit;
using Moq;
using FluentAssertions;
using Wallet.Application.Interfaces;
using Wallet.Application.Wallets.Handlers;
using Wallet.Application.Wallets.Commands;
using WalletEntity = Wallet.DomainLayer.Entities.Wallet;

namespace Wallet.Tests.Wallets;

public class CreateWalletHandlerTests
{
    private readonly Mock<IWalletRepository> _walletRepoMock;
    private readonly CreateWalletHandler _handler;

    public CreateWalletHandlerTests()
    {
        _walletRepoMock = new Mock<IWalletRepository>();
        _handler = new CreateWalletHandler(_walletRepoMock.Object);
    }

    [Fact]
    public async Task HandleAsync_Should_Throw_When_Name_Is_Empty()
    {
        var command = new CreateWalletRequest
        {
            DocumentId = "123456789",
            Name = ""
        };

        Console.WriteLine("[TEST] Verificando error cuando el nombre está vacío.");

        var act = () => _handler.HandleAsync(command);
        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("*Name*");
    }

    [Fact]
    public async Task HandleAsync_Should_Throw_When_DocumentId_Is_Empty()
    {
        var command = new CreateWalletRequest
        {
            DocumentId = "",
            Name = "Juan"
        };

        Console.WriteLine("[TEST] Verificando error cuando el documento está vacío.");

        var act = () => _handler.HandleAsync(command);
        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("*DocumentId*");
    }

    [Fact]
    public async Task HandleAsync_Should_Create_Wallet_When_Valid()
    {
        var command = new CreateWalletRequest
        {
            DocumentId = "1010101010",
            Name = "Andrés"
        };

        _walletRepoMock
            .Setup(r => r.AddAsync(It.IsAny<WalletEntity>()))
            .ReturnsAsync((WalletEntity w) => { w.Id = 1; return w; });

        var result = await _handler.HandleAsync(command);

        Console.WriteLine($"[TEST] Wallet creada: ID={result.Id}, Name={result.Name}, Balance={result.Balance}");

        result.Should().NotBeNull();
        result.Id.Should().Be(1);
        result.Name.Should().Be("Andrés");
        result.DocumentId.Should().Be("1010101010");
        result.Balance.Should().Be(0);

        _walletRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }
}
