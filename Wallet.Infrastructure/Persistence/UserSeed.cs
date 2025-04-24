using Wallet.DomainLayer.Entities;

public static class UserSeed
{
    public static List<User> Users = new()
    {
        new() { Id = 1, Username = "admin", Password = "admin123", Role = "Admin" },
        new() { Id = 2, Username = "user", Password = "user123", Role = "User" }
    };
}
