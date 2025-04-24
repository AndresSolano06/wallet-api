namespace Wallet.Application.Wallets.Commands;

public class RechargeWalletRequest
{
    public int WalletId { get; set; }
    public decimal Amount { get; set; }
}
