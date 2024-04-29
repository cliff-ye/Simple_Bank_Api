using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleApiProject.Dto;
using SimpleApiProject.Models;
using SimpleApiProject.Services.ServiceInterfaces;
using SimpleApiProject.UnitofWork;
using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;

namespace SimpleApiProject.Services.ServiceImplementation
{
    public class AccountImpl : ControllerBase, IAccount
    {
        private readonly IUnitofWork _unitofwork;

        protected DbSet<Account> _dbSet;
        private string msg = "";
        public ITransaction _transaction;

        public AccountImpl(IUnitofWork unitofwork)
        {
            _unitofwork = unitofwork;
            _dbSet = _unitofwork.dbContext.Set<Account>();
            _transaction = new TransactionImpl(unitofwork);
        }
        public async Task<ActionResult<AccountDetailDto>> CreateAccount(CreateAccDto createAccDto)
        {
            if (createAccDto == null)
            {
                return BadRequest();
            }

            //check existence of account by email
            if (AccountExistsByEmail(createAccDto.Email))
            {
                return Conflict("User already exists");

            }

            //map dto account
            var account = CustomMapping.CustomMap.MapDtoToAccount(createAccDto);

            //store account number and balance
            account.AccountNumber = Utils.GenerateAccNum.Gen();
            account.AccountBalance = 0;

            _dbSet.Add(account);
            await _unitofwork.SaveChangesAsync();

            return Ok(CustomMapping.CustomMap.MapAccountToDto(account));
        }

        public async Task<ActionResult<AccountDetailDto>> Deposit(DepositDto depositDto)
        {
            if (!AccountExistsByAccNum(depositDto.AccNum))
            {
                return NotFound("account does not exist");
            }

            //get account to deposit into
            var account = _dbSet.FirstOrDefault(x => x.AccountNumber == depositDto.AccNum);
            account.AccountBalance += depositDto.Amount;

            //update 
            _unitofwork.dbContext.Entry(account).State = EntityState.Modified;

            msg = "Deposited successfully";
            _transaction.LogTransaction("Deposit", depositDto.Amount, depositDto.AccNum, "success", msg);

            try
            {
                await _unitofwork.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok(CustomMapping.CustomMap.MapAccountToDto(account));
        }

        public async Task<ActionResult<AccountDetailDto>> Transfer(TransferDto transferDto)
        {
            if (!AccountExistsByAccNum(transferDto.senderAccNum) || !AccountExistsByAccNum(transferDto.recipientAccNum))
            {
                return NotFound("Account does not exist");
            }

            if (transferDto.amount <= 0)
            {
                msg = "Invalid transfer amount";
                _transaction.LogTransaction("Money Transfer", transferDto.amount, transferDto.recipientAccNum, "failed", msg);
                await _unitofwork.SaveChangesAsync();
                return BadRequest(msg);
            }

            var senderAcc = _dbSet.FirstOrDefault(x => x.AccountNumber == transferDto.senderAccNum);

            if (senderAcc.AccountBalance < transferDto.amount)
            {
                msg = "Insufficient Balance";
                _transaction.LogTransaction("Money Transfer", transferDto.amount, transferDto.recipientAccNum, "failed", msg);
                await _unitofwork.SaveChangesAsync();
                return StatusCode(402, msg);
            }

            var recipientAcc = _dbSet.FirstOrDefault(x => x.AccountNumber == transferDto.recipientAccNum);

            senderAcc.AccountBalance -= transferDto.amount;
            recipientAcc.AccountBalance += transferDto.amount;

            _unitofwork.dbContext.Entry(senderAcc).State = EntityState.Modified;

            msg = "Transferred successfully";
            _transaction.LogTransaction("Money Transfer", transferDto.amount, transferDto.recipientAccNum, "success", msg);

            try
            {
                await _unitofwork.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok(CustomMapping.CustomMap.MapAccountToDto(senderAcc));
        }

        public async Task<ActionResult<AccountDetailDto>> AccountBalEnquiry(string accountNum)
        {
            if (accountNum == null)
            {
                return BadRequest();
            }

            var account = await _dbSet.FirstOrDefaultAsync(acc => acc.AccountNumber == accountNum);

            if (account == null)
            {
                return NotFound("account does not exist");
            }

            return Ok(CustomMapping.CustomMap.MapAccountToDto(account));
        }

        

        private bool AccountExistsByEmail(string email)
        {
            return _dbSet.Any(acc => acc.Email == email);
        }

        private bool AccountExistsByAccNum(string accountNum)
        {
            return _dbSet.Any(acc => acc.AccountNumber == accountNum);
        }

       
    }
}
