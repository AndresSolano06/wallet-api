﻿namespace Wallet.Application.Transactions.Commands;

public class TransferRequest
{
    public int FromWalletId { get; set; }
    public int ToWalletId { get; set; }
    public decimal Amount { get; set; }
}
