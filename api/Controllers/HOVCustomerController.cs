using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HOVLaneViolation.Data;
using HOVLaneViolation.Entities;

namespace HOVLaneViolation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HOVCustomerController : ControllerBase
    {
        private readonly DataContext _context;

        public HOVCustomerController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HOVCustomerData>>> GetCustomers()
        {
            return await _context.HOVCustomers.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HOVCustomerData>> GetCustomer(int id)
        {
            var customer = await _context.HOVCustomers.FindAsync(id);
            if (customer == null) return NotFound();
            return customer;
        }



        [HttpPost]
        public async Task<ActionResult<HOVCustomerData>> AddCustomer(HOVCustomerData customer)
        {
            _context.HOVCustomers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }


        // ✅ NEW: Get customer by license plate
        [HttpGet("byLicense/{licensePlate}")]
public async Task<ActionResult<HOVCustomerData>> GetCustomerByLicense(string licensePlate)
{
    var customer = await _context.HOVCustomers
        .FirstOrDefaultAsync(c => c.LicensePlate == licensePlate);

    if (customer == null)
        return NotFound();  // This triggers your 404
    return Ok(customer);
}
    }
}
