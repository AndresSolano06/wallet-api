namespace Wallet.Application.Wallets.Commands;

public class CreateWalletRequest
{
    public string DocumentId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}
