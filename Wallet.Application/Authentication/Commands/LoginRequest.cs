namespace Wallet.Application.Authentication.Commands;

public class LoginRequest
{
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
}
