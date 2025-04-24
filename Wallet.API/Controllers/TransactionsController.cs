using Microsoft.AspNetCore.Mvc;
using Wallet.Application.Transactions.Commands;
using Wallet.Application.Transactions.Handlers;
using Wallet.Application.Transactions.Responses;

namespace Wallet.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly TransferHandler _transferHandler;
    private readonly GetTransactionsHandler _getTransactionsHandler;

    public TransactionsController(TransferHandler transferHandler, GetTransactionsHandler getTransactionsHandler)
    {
        _transferHandler = transferHandler;
        _getTransactionsHandler = getTransactionsHandler;

    }

    [HttpPost("transfer")]
    public async Task<IActionResult> Transfer([FromBody] TransferRequest request)
    {
        try
        {
            await _transferHandler.HandleAsync(request);
            return Ok(new { message = "Transferencia realizada con éxito" });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return UnprocessableEntity(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Error interno del servidor", detail = ex.Message });
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransactionResponse>>> GetByWalletId([FromQuery] int walletId)
    {
        var result = await _getTransactionsHandler.HandleAsync(walletId);
        return Ok(result);
    }

}
