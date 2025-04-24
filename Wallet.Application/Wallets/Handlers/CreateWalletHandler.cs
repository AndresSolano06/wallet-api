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
        Console.WriteLine($"[Handler] Entrando a HandleAsync");
        Console.WriteLine($"[Handler] Validando campos: Name='{request.Name}', DocumentId='{request.DocumentId}'");

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            Console.WriteLine("[Handler] Error: Name vacío o nulo");
            throw new ArgumentException("Name es obligatorio");
        }

        if (string.IsNullOrWhiteSpace(request.DocumentId))
        {
            Console.WriteLine("[Handler] Error: DocumentId vacío o nulo");
            throw new ArgumentException("DocumentId es obligatorio");
        }

        var wallet = new WalletEntity
        {
            Name = request.Name,
            DocumentId = request.DocumentId,
            Balance = 0,
            CreatedAt = DateTime.UtcNow
        };

        try
        {
            Console.WriteLine("[Handler] Insertando Wallet en repositorio...");
            var created = await _walletRepository.AddAsync(wallet);
            Console.WriteLine("[Handler] Ejecutando SaveChangesAsync...");
            await _walletRepository.SaveChangesAsync();
            Console.WriteLine($"[Handler] Wallet guardada con ID={created.Id}");

            return new WalletResponse
            {
                Id = created.Id,
                Name = created.Name,
                DocumentId = created.DocumentId,
                Balance = created.Balance
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Handler] ERROR inesperado al guardar la wallet: {ex.GetType().Name} - {ex.Message}");
            throw;
        }
    }

}
