using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApometreController : ControllerBase
    {
        private readonly PayDBContext _context;

        public ApometreController(PayDBContext context)
        {
            _context = context;
        }

        // GET: api/Apometre
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Apometre>>> GetApometre()
        {
            return await _context.Apometre.ToListAsync();
        }

        // GET: api/Apometre/5
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<Apometre>>> GetApometreByUserId(int userId)
        {
            var apometres = await _context.Apometre.Where(a => a.Iduser == userId).ToListAsync();

            if (!apometres.Any())
            {
                return NotFound();
            }

            return apometres;
        }


        [HttpGet("latest/{id}")]
        public async Task<IActionResult> GetLatestApometre(int id)
        {
            var apometre = await _context.Apometre
                .Where(a => a.Iduser == id)
                .OrderByDescending(a => a.TransmitereData)
                .FirstOrDefaultAsync();

            if (apometre == null)
            {
                return Ok(new { message = "No data yet" });
            }

            return Ok(apometre);
        }

        // PUT: api/Apometre/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApometre(int id, Apometre apometre)
        {
            if (id != apometre.IdApometre)
            {
                return BadRequest();
            }

            _context.Entry(apometre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApometreExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Apometre
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Apometre>> PostApometre(Apometre apometre)
        {
            _context.Apometre.Add(apometre);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApometre", new { id = apometre.IdApometre }, apometre);
        }
 

        // DELETE: api/Apometre/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Apometre>> DeleteApometre(int id)
        {
            var apometre = await _context.Apometre.FindAsync(id);
            if (apometre == null)
            {
                return NotFound();
            }

            _context.Apometre.Remove(apometre);
            await _context.SaveChangesAsync();

            return apometre;
        }

        private bool ApometreExists(int id)
        {
            return _context.Apometre.Any(e => e.IdApometre == id);
        }
    }
}
