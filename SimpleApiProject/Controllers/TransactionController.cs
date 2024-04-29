using Microsoft.AspNetCore.Mvc;
using SimpleApiProject.Models;
using SimpleApiProject.Services.ServiceImplementation;
using SimpleApiProject.Services.ServiceInterfaces;
using SimpleApiProject.UnitofWork;

namespace SimpleApiProject.Controllers
{
    [ApiController]
    [Route("/api/v1/transactions/")]
    public class TransactionController : Controller
    {
        private readonly IUnitofWork _unitofWork;
        private ITransaction _transaction;
        private readonly ILogger<TransactionController> _logger;
        public TransactionController(IUnitofWork unitofWork, ILogger<TransactionController> logger)
        {
            _unitofWork = unitofWork;
            _transaction = new TransactionImpl(unitofWork);
            _logger = logger;

        }

        /// <summary>
        /// This end point returns all the transactions
        /// </summary>
        /// <response code="200"></response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetAllTransactions()
        {
            _logger.LogInformation("gets all transactions");
            return await _transaction.GetAllTransactions();
        }

        /// <summary>
        /// This end point returns all the transactions
        /// </summary>
        /// <response code="200"></response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(Guid id)
        {
            _logger.LogInformation("gets a transaction");

            return await _transaction.GetTransaction(id);
        }

        /// <summary>
        /// This end point returns all the transactions
        /// </summary>
        /// <response code="204"></response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(Guid id)
        {
            _logger.LogInformation("deletes a transaction");

            return await _transaction.DeleteTransaction(id);
        }
    }
}
