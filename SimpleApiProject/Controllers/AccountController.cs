using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using SimpleApiProject.Dto;
using SimpleApiProject.Models;
using SimpleApiProject.Services.ServiceImplementation;
using SimpleApiProject.Services.ServiceInterfaces;
using SimpleApiProject.UnitofWork;

namespace SimpleApiProject.Controllers
{
    [ApiController]
    [Route("/api/v1/accounts/")]
    public class AccountController : Controller
    {
        private readonly IUnitofWork _unitofWork;
        private IAccount _account;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IUnitofWork unitofwork, ILogger<AccountController> logger, IAccount account)
        {
            _unitofWork = unitofwork;
            _account = account;
            _logger = logger;
        }

        /// <summary>
        /// This end point creates an account 
        /// </summary>
        /// <param name="createAccDto"></param>
        /// <response code="200">returns the account name, account number and balance</response>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<AccountDetailDto>> CreateAccount(CreateAccDto createAccDto)
        {
            _logger.LogInformation("creating an account");
            return await _account.CreateAccount(createAccDto);
        }

        /// <summary>
        /// This end point is there for users put money into their account to be able to perform transfers 
        /// </summary>
        /// <param name="depositDto"></param>
        /// <response code="200">returns the account name, account number and balance</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("deposit")]
        public async Task<ActionResult<AccountDetailDto>> Deposit(DepositDto depositDto)
        {
            _logger.LogInformation("deposit into account");
            return await _account.Deposit(depositDto);
        }

        /// <summary>
        /// This end point is used to transfer fund from one account to another 
        /// </summary>
        /// <param name="transferDto"></param>
        /// <response code="200">returns the account name, account number and balance</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status402PaymentRequired)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("transfer")]
        public async Task<ActionResult<AccountDetailDto>> MakeTransfer(TransferDto transferDto)
        {
            _logger.LogInformation("make transfer to other account");
            return await _account.Transfer(transferDto);
        }

        /// <summary>
        /// This end point is used to retrieve user's account balance 
        /// </summary>
        /// <param name="accountNum"></param>
        /// <response code="200">returns the account name, account number and balance</response>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{accountNum}")]
        public async Task<ActionResult<AccountDetailDto>> GetAccBal(string accountNum)
        {
            _logger.LogInformation("get account balance");
            return await _account.AccountBalEnquiry(accountNum);
        }
    }
}
