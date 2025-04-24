﻿namespace Wallet.DomainLayer.Entities;

public class Transaction
{
    public int Id { get; set; }
    public int WalletId { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; } = "Credit"; // o "Debit"
    public DateTime CreatedAt { get; set; }

    public Wallet? Wallet { get; set; }
}
