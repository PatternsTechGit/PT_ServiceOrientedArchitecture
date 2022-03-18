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
        [Route("GetLastThreeYearsBalancesById/{accountId}")]
        public async Task<ActionResult> GetLastThreeYearsBalancesById(string accountId)
        {
            try
            {
                return new OkObjectResult(await _transactionService.GetLastThreeYearsBalancesById(accountId));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex); 
            }
        }

    }
}
