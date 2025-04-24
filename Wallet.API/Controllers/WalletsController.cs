using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wallet.Application.Wallets.Commands;
using Wallet.Application.Wallets.Handlers;
using Wallet.Application.Wallets.Responses;

namespace Wallet.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class WalletsController : ControllerBase
{
    private readonly CreateWalletHandler _createWalletHandler;
    private readonly GetWalletHandler _getWalletHandler;
    private readonly RechargeWalletHandler _rechargeWalletHandler;

    public WalletsController(CreateWalletHandler createWalletHandler, GetWalletHandler getWalletHandler, RechargeWalletHandler rechargeWalletHandler)
    {
        _createWalletHandler = createWalletHandler;
        _getWalletHandler = getWalletHandler;
        _rechargeWalletHandler = rechargeWalletHandler;

    }

    [Authorize(Roles = "Admin")]
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

    [Authorize(Roles = "User,Admin")]
    [HttpGet("{id}")]
    public async Task<ActionResult<WalletResponse>> GetWalletById(int id)
    {
        var wallet = await _getWalletHandler.HandleAsync(id);
        if (wallet is null)
            return NotFound();
        return Ok(wallet);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("{id}/recharge")]
    public async Task<IActionResult> Recharge(int id, [FromBody] RechargeWalletRequest request)
    {
        if (id != request.WalletId)
            return BadRequest("El ID de la URL no coincide con el del cuerpo");

        await _rechargeWalletHandler.HandleAsync(request);
        return Ok(new { message = "Recarga exitosa" });
    }


}
