namespace Wallet.Application.Transactions.Responses;

public class TransactionResponse
{
    public decimal Amount { get; set; }
    public string Type { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
