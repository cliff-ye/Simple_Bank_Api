using Microsoft.AspNetCore.Mvc;
using SimpleApiProject.Dto;
using SimpleApiProject.Models;

namespace SimpleApiProject.Services.ServiceInterfaces
{
    public interface IAccount
    {
        public Task<ActionResult<AccountDetailDto>> CreateAccount(CreateAccDto createAccDto);
        public Task<ActionResult<AccountDetailDto>> Deposit(DepositDto depositDto);
        public Task<ActionResult<AccountDetailDto>> Transfer(TransferDto transferDto);
        public Task<ActionResult<AccountDetailDto>> AccountBalEnquiry(string accountNum);

    }
}
