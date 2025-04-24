using Microsoft.AspNetCore.Mvc;
using Wallet.Application.Wallets.Commands;
using Wallet.Application.Wallets.Handlers;
using Wallet.Application.Wallets.Responses;

namespace Wallet.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WalletsController : ControllerBase
{
    private readonly CreateWalletHandler _createWalletHandler;
    private readonly GetWalletHandler _getWalletHandler;

    public WalletsController(CreateWalletHandler createWalletHandler, GetWalletHandler getWalletHandler)
    {
        _createWalletHandler = createWalletHandler;
        _getWalletHandler = getWalletHandler;

    }

    [HttpPost]
    public async Task<ActionResult<WalletResponse>> CreateWallet([FromBody] CreateWalletRequest request)
    {
        try
        {
            var result = await _createWalletHandler.HandleAsync(request);
            return CreatedAtAction(nameof(GetWalletById), new { id = result.Id }, result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WalletResponse>> GetWalletById(int id)
    {
        var wallet = await _getWalletHandler.HandleAsync(id);
        if (wallet is null)
            return NotFound();
        return Ok(wallet);
    }

}
