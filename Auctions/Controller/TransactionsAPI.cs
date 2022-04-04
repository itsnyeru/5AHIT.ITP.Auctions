using Domain.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Auctions.Controller;
[Route("api/transaction")]
[ApiController]
public class TransactionsAPI : ControllerBase {

    ITransactionRepository _transactionRepository;

    public TransactionsAPI(ITransactionRepository transactionRepository) { _transactionRepository = transactionRepository; }

    [HttpPost("approve/{userId}/{token}")]
    public async Task Post(int userId, Guid token) => 
        await _transactionRepository.Approve(userId, token);
}
