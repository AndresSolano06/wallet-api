using System.Net.Http.Headers;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Wallet.API;
using Wallet.Application.Wallets.Commands;
using Wallet.Application.Wallets.Responses;
using Xunit;
using System.Net;

namespace Wallet.Tests.Integration;

public class WalletsIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public WalletsIntegrationTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    private string GenerateFakeJwt()
    {
        const string secretKey = "2vVV[.e$$*.Q!&J<8Tc~X,1Rmyp8w=$ABC";

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
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
            signingCredentials: creds
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        Console.WriteLine($"[FakeJWT] Token generado: {jwt}");

        return jwt;
    }

    [Fact]
    public async Task CreateWallet_Should_Return_200_And_Create()
    {
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", GenerateFakeJwt());

        var payload = new CreateWalletRequest
        {
            Name = "Integrado",
            DocumentId = "999999"
        };

        var response = await _client.PostAsJsonAsync("/api/wallets", payload);

        response.StatusCode.Should().Be(HttpStatusCode.Created);



        var body = await response.Content.ReadFromJsonAsync<WalletResponse>();
        body.Should().NotBeNull();
        body!.Name.Should().Be("Integrado");
        body.DocumentId.Should().Be("999999");
    }
}
