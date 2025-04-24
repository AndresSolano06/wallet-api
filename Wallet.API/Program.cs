using Wallet.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Wallet.Application.Interfaces;
using Wallet.Infrastructure.Repositories;
using Wallet.Application.Wallets.Handlers;
using Wallet.Application.Transactions.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WalletDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IWalletRepository, WalletRepository>();
builder.Services.AddScoped<CreateWalletHandler>();
builder.Services.AddScoped<GetWalletHandler>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<TransferHandler>();
builder.Services.AddScoped<GetTransactionsHandler>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
