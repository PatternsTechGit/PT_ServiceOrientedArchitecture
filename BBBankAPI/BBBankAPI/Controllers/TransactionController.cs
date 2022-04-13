using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace BBBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        [HttpGet]
        [Route("GetLast3MonthBalances")]
        public async Task<ActionResult> GetLast3MonthBalances()
        {
            try
            {
                return new OkObjectResult(await _transactionService.GetLast3MonthBalances(null));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }
        [HttpGet]
        [Route("GetLast3MonthBalances/{accountId}")]
        public async Task<ActionResult> GetLast3MonthBalances(string accountId)
        {
            try
            {
                return new OkObjectResult(await _transactionService.GetLast3MonthBalances(accountId));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }
    }
}
