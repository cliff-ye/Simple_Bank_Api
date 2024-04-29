using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleApiProject.Data;
using SimpleApiProject.Models;
using SimpleApiProject.Services.ServiceInterfaces;
using SimpleApiProject.UnitofWork;
using System.Collections.Generic;

namespace SimpleApiProject.Services.ServiceImplementation
{
    public class TransactionImpl : ControllerBase, ITransaction
    {
        private readonly IUnitofWork _unitofwork;
        protected DbSet<Transaction> _dbSet;
        private AppDbContext _appDbContext;

        public TransactionImpl(IUnitofWork unitofwork, AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _unitofwork = unitofwork;
            _dbSet = _appDbContext.Set<Transaction>();
        }

        public void LogTransaction(string transactionName, decimal transactionAmt, string receiverAccNum, string status, string reason)
        {
            Transaction transaction = new()
            {
                TransactionName = transactionName,
                TransactionAmount = transactionAmt,
                ReceiverAccNumber = receiverAccNum,
                Status = status,
                Reason = reason
            };

            _dbSet.Add(transaction);
        }

        public async Task<ActionResult<IEnumerable<Transaction>>> GetAllTransactions()
        {
            var transactions = await _dbSet.ToListAsync();
            if (transactions == null)
            {
                return NotFound();
            }
            return Ok(transactions);
        }

        public async Task<ActionResult<Transaction>> GetTransaction(Guid transactionId)
        {
           var transaction =  await _dbSet.FindAsync(transactionId);

            if (transaction == null)
            {
                return NotFound();
            }
            return Ok(transaction);
        }

        public async Task<IActionResult> DeleteTransaction(Guid transactionId)
        {
            var transaction = await _dbSet.FindAsync(transactionId);

            if (transaction == null)
            {
                return NotFound();
            }

            _dbSet.Remove(transaction);
            await _unitofwork.SaveChangesAsync();
            return NoContent();
        }
    }
}
