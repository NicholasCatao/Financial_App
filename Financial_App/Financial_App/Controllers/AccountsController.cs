using Financial_App.Controllers.Base;
using Financial_App.Domain.Interfaces;
using Financial_App.Domain.Request;
using Financial_App.Domain.Response;
using Microsoft.AspNetCore.Mvc;

namespace Financial_App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : BaseController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<IActionResult> AccountAsync([FromBody] AccountRequest accountRequest, [FromServices] IAccountService accountService)
        {
            var result = await accountService.CreateAccountAsync(accountRequest);
            return result.PossuiErro ? HandleError(result) : Ok(result.Dados);
        }

        [HttpPost("drop-cash")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<IActionResult> AccountDepositAsync([FromQuery] string accountId, [FromQuery] decimal amount, [FromServices] IAccountService accountService)
        {
            var result = await accountService.AccountDropCashAsync(accountId, amount);
            return result.PossuiErro ? HandleError(result) : Ok(result.Dados);
        }

        [HttpPost("move-funds")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<IActionResult> AccountMoveFundstAsync([FromBody] MoveFundsRequest moveFundsRequest, [FromServices] IAccountService accountService)
        {
            var result = await accountService.AccountMoveFundsAsync(moveFundsRequest);
            return result.PossuiErro ? HandleError(result) : Ok(result.Dados);
        }

        [HttpGet("balance")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<IActionResult> AccountBalanceAsync([FromQuery] string accountId, [FromServices] IAccountService accountService)
        {
            var result = await accountService.GetAccountbyIdAsync(accountId);
            return result.PossuiErro ? HandleError(result) : Ok(result.Dados);
        }

        [HttpGet("statement-range")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<IActionResult> AccountStatementbyRangeAsync([FromBody] AccountFilter accountFilter, [FromServices] IAccountService accountService)
        {
            var result = await accountService.AccountStatementbyRangeAsync(accountFilter);
            return result.PossuiErro ? HandleError(result) : Ok(result.Dados);
        }

        [HttpGet("statement-type")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<IActionResult> AccountStatementbyTypeAsync([FromBody] AccountFilter accountFilter, [FromServices] IAccountService accountService)
        {
            var result = await accountService.AccountStatementbyTypeAsync(accountFilter);
            return result.PossuiErro ? HandleError(result) : Ok(result.Dados);
        }
    }
}