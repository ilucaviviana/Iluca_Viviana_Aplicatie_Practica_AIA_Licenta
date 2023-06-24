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
    public class StradaController : ControllerBase
    {
        private readonly PayDBContext _context;

        public StradaController(PayDBContext context)
        {
            _context = context;
        }

        // GET: api/Strada
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Strada>>> GetStrada()
        {
            return await _context.Strada.ToListAsync();
        }

        // GET: api/Strada/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Strada>> GetStrada(int id)
        {
            var strada = await _context.Strada.FindAsync(id);

            if (strada == null)
            {
                return NotFound();
            }

            return strada;
        }

        // PUT: api/Strada/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStrada(int id, Strada strada)
        {
            if (id != strada.Idstrada)
            {
                return BadRequest();
            }

            _context.Entry(strada).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StradaExists(id))
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

        // POST: api/Strada
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Strada>> PostStrada(Strada strada)
        {
            _context.Strada.Add(strada);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStrada", new { id = strada.Idstrada }, strada);
        }

        // DELETE: api/Strada/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Strada>> DeleteStrada(int id)
        {
            var strada = await _context.Strada.FindAsync(id);
            if (strada == null)
            {
                return NotFound();
            }

            _context.Strada.Remove(strada);
            await _context.SaveChangesAsync();

            return strada;
        }

        private bool StradaExists(int id)
        {
            return _context.Strada.Any(e => e.Idstrada == id);
        }
    }
}
