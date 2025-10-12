using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HOVLaneViolation.Data;
using HOVLaneViolation.Entities;


namespace HOVLaneViolation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HOVMasterController : ControllerBase
    {
        private readonly DataContext _context;

        public HOVMasterController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("byMakeModel")]
        public async Task<ActionResult<HOVMasterData>> GetByMakeAndModel(string make, string model)
        {
            var hovMaster = await _context.HOVMasters
                .FirstOrDefaultAsync(m => m.Make == make && m.Model == model);

            if (hovMaster == null)
            {
                return NotFound();
            }

            return Ok(hovMaster);
        }

        // GET: api/hovmaster
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HOVMasterData>>> GetHOVMasters()
        {
            // Return all car master data from the database asynchronously
            return await _context.HOVMasters.ToListAsync();
        }

        // GET: api/hovmaster/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HOVMasterData>> GetHOVMaster(int id)
        {
            var hovMaster = await _context.HOVMasters.FindAsync(id);

            if (hovMaster == null)
            {
                return NotFound(); // 404 if not found
            }

            return hovMaster;
        }

        [HttpPost]
        public async Task<ActionResult<HOVMasterData>> CreateHOVMaster(HOVMasterData hovMaster)
        {
            _context.HOVMasters.Add(hovMaster);
            await _context.SaveChangesAsync();

            // Returns 201 Created with URI of new resource
            return CreatedAtAction(nameof(GetHOVMaster), new { id = hovMaster.Id }, hovMaster);
        }

        // PUT: api/hovmaster/5
        // [HttpPut("{id}")]
        // public async Task<IActionResult> UpdateHOVMaster(int id, HOVMasterData hovMaster)
        // {
        //     if (id != hovMaster.Id)
        //     {
        //         return BadRequest(); // 400 Bad Request if IDs don't match
        //     }

        //     _context.Entry(hovMaster).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!HOVMasterExists(id))
        //         {
        //             return NotFound(); // 404 if item to update doesn't exist
        //         }
        //         else
        //         {
        //             throw; // Re-throw if other concurrency problem
        //         }
        //     }

        //     return NoContent(); // 204 No Content to signal success
        // }

        // DELETE: api/hovmaster/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHOVMaster(int id)
        {
            var hovMaster = await _context.HOVMasters.FindAsync(id);
            if (hovMaster == null)
            {
                return NotFound();
            }

            _context.HOVMasters.Remove(hovMaster);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HOVMasterExists(int id)
        {
            return _context.HOVMasters.Any(e => e.Id == id);
        }
    }
}
