using Microsoft.AspNetCore.Mvc;
using SimpleApiProject.Models;

namespace SimpleApiProject.Services.ServiceInterfaces
{
    public interface ITransaction
    {
        public void LogTransaction(string transactionName, decimal transactionAmt,string receiverAccNum, string status, string reason);

        public Task<ActionResult<IEnumerable<Transaction>>> GetAllTransactions();
        public Task<ActionResult<Transaction>> GetTransaction(Guid transactionId);
        public Task<IActionResult> DeleteTransaction(Guid transactionId);
    }
}
