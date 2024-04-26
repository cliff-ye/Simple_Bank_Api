using SimpleApiProject.Dto;
using SimpleApiProject.Models;

namespace SimpleApiProject.CustomMapping
{
    public class CustomMap
    {
       
        public static Account MapDtoToAccount(CreateAccDto createAccDto)
        {
            return new Account()
            {
                AccountName = createAccDto.FirstName + " " + createAccDto.LastName,
                Address = createAccDto.address,
                Email = createAccDto.Email,
                PhoneNumber = createAccDto.phoneNumber
            };
        }

        public static AccountDetailDto MapAccountToDto(Account account)
        {
            return new AccountDetailDto()
            {
                accountName = account.AccountName,
                accountNumber = account.AccountNumber,
                accountBalance = account.AccountBalance
            };
        }

        //public static Transaction MapToTransaction(LogTransactionDto logTransaction)
        //{
        //    return new Transaction()
        //    {
        //        TransactionName = logTransaction.TransactionName,
        //        TransactionAmount = logTransaction.TransactionAmount,
        //        ReceiverAccNumber = logTransaction.ReceiverAccNum,
        //        status = logTransaction.status
        //    };
        //}
    }
}
