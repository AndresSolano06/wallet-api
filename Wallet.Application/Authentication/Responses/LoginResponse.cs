﻿namespace Wallet.Application.Authentication.Responses;

public class LoginResponse
{
    public string Token { get; set; } = "";
    public DateTime Expiration { get; set; }
}
