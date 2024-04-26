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
      

        public AccountController(IUnitofWork unitofwork)
        {
            _unitofWork = unitofwork;
            _account = new AccountImpl(unitofwork);
        }

        /// <summary>
        /// This end point creates an account 
        /// </summary>
        /// <param name="createAccDto"></param>
        /// <response code="200">returns the account name, account number and balance</response>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<ActionResult<AccountDetailDto>> CreateAccount(CreateAccDto createAccDto)
        {
            return await _account.CreateAccount(createAccDto);
        }

        /// <summary>
        /// This end point is there for users put money into their account to be able to perform transfers 
        /// </summary>
        /// <param name="depositDto"></param>
        /// <response code="200">returns the account name, account number and balance</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("deposit")]
        public async Task<ActionResult<AccountDetailDto>> Deposit(DepositDto depositDto)
        {
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
        [HttpPost("transfer")]
        public async Task<ActionResult<AccountDetailDto>> MakeTransfer(TransferDto transferDto)
        {
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
        [HttpGet("{accountNum}")]
        public async Task<ActionResult<AccountDetailDto>> GetAccBal(string accountNum)
        {
            return await _account.AccountBalEnquiry(accountNum);
        }
    }
}
