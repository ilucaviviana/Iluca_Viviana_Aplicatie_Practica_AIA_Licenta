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
    public class OrasController : ControllerBase
    {
        private readonly PayDBContext _context;

        public OrasController(PayDBContext context)
        {
            _context = context;
        }

        // GET: api/Oras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Oras>>> GetOras()
        {
            return await _context.Oras.ToListAsync();
        }

        // GET: api/Oras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Oras>> GetOras(int id)
        {
            var oras = await _context.Oras.FindAsync(id);

            if (oras == null)
            {
                return NotFound();
            }

            return oras;
        }

        // PUT: api/Oras/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOras(int id, Oras oras)
        {
            if (id != oras.Idoras)
            {
                return BadRequest();
            }

            _context.Entry(oras).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrasExists(id))
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

        // POST: api/Oras
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Oras>> PostOras(Oras oras)
        {
            _context.Oras.Add(oras);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOras", new { id = oras.Idoras }, oras);
        }

        // DELETE: api/Oras/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Oras>> DeleteOras(int id)
        {
            var oras = await _context.Oras.FindAsync(id);
            if (oras == null)
            {
                return NotFound();
            }

            _context.Oras.Remove(oras);
            await _context.SaveChangesAsync();

            return oras;
        }

        private bool OrasExists(int id)
        {
            return _context.Oras.Any(e => e.Idoras == id);
        }
    }
}
