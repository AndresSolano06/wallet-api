using Wallet.Application.Interfaces;
using Wallet.Application.Transactions.Responses;

namespace Wallet.Application.Transactions.Handlers;

public class GetTransactionsHandler
{
    private readonly ITransactionRepository _repository;

    public GetTransactionsHandler(ITransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<TransactionResponse>> HandleAsync(int walletId)
    {
        var transactions = await _repository.GetByWalletIdAsync(walletId);

        return transactions.Select(t => new TransactionResponse
        {
            Amount = t.Amount,
            Type = t.Type,
            CreatedAt = t.CreatedAt
        }).ToList();
    }
}
