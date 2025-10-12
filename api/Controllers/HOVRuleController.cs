using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HOVLaneViolation.Data;
using HOVLaneViolation.Entities;


namespace HOVLaneViolation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HOVRuleController : ControllerBase
    {
        private readonly DataContext _context;

        public HOVRuleController(DataContext context)
        {
            _context = context;
        }

        // GET: api/HOVRule
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HOVRule>>> GetRules()
        {
            return await _context.HOVRules.ToListAsync();
        }

        // DELETE: api/HOVRule/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRule(int id)
        {
            var rule = await _context.HOVRules.FindAsync(id);
            if (rule == null)
                return NotFound();

            _context.HOVRules.Remove(rule);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
