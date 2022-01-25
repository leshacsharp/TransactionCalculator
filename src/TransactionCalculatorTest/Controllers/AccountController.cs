using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionCalculatorTest.BusinessLogic.Interfaces;
using TransactionCalculatorTest.Model.Models;

namespace TransactionCalculatorTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountService accountService, ILogger<AccountController> logger)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        [Route("/calculate")]
        [ProducesResponseType(typeof(List<AccountInfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Calculate(CalculationRequest request)
        {
            //The attached input sample has the accounts field as an array. Therefore I decided to modify a bit the output scheme.
            //Added the AccountId property to the output scheme to understand an owner of info.
            //The API response is an array instead of single object as in the task specification.

            try
            {
                var accountsInfo = _accountService.GetAccountsInfo(request);

                return Ok(accountsInfo);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
