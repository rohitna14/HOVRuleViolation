using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HOVLaneViolation.Data;
using HOVLaneViolation.Entities;

[ApiController]
[Route("api/[controller]")]
public class HOVTransactionController : ControllerBase
{
    private readonly DataContext _context;

    public HOVTransactionController(DataContext context)
    {
        _context = context; 
    }

    // POST: api/HOVTransaction
    [HttpPost]
    public async Task<ActionResult<HOVTransactionData>> CreateTransaction(HOVTransactionData transaction)
    {
        _context.HOVTransactions.Add(transaction);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTransaction), new { id = transaction.Id }, transaction);
    }

    // GET: api/HOVTransaction/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<HOVTransactionData>> GetTransaction(int id)
    {
        var transaction = await _context.HOVTransactions.FindAsync(id);

        if (transaction == null)
            return NotFound();

        return transaction;
    }
}
