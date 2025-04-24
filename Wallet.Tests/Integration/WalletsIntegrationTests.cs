using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using Wallet.Application.Wallets.Commands;
using Wallet.Application.Wallets.Responses;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Net.Http.Headers;


namespace Wallet.Tests.Integration;

public class WalletsIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public WalletsIntegrationTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }
    private string GenerateFakeJwt()
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("2vWV[.e$$^,QI&4<8T<~rX,lRmpy8w=$"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
        new Claim(ClaimTypes.Name, "admin"),
        new Claim(ClaimTypes.Role, "Admin") 
    };

        var token = new JwtSecurityToken(
            issuer: "wallet-api",
            audience: "wallet-api-users",
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    [Fact]
    public async Task CreateWallet_Should_Return_200_And_Create()
    {
        _client.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Bearer", GenerateFakeJwt());
        var payload = new CreateWalletRequest
        {
            Name = "Integrado",
            DocumentId = "999999",
        };

        var response = await _client.PostAsJsonAsync("/api/wallets", payload);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        var body = await response.Content.ReadFromJsonAsync<Wallet.Application.Wallets.Responses.WalletResponse>();
        body.Should().NotBeNull();
        body!.Name.Should().Be("Integrado");
        body.DocumentId.Should().Be("999999");
    }
}
