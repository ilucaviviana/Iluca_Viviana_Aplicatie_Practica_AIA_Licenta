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
    public class JudetController : ControllerBase
    {
        private readonly PayDBContext _context;

        public JudetController(PayDBContext context)
        {
            _context = context;
        }

        // GET: api/Judet
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Judet>>> GetJudet()
        {
            return await _context.Judet.ToListAsync();
        }

        // GET: api/Judet/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Judet>> GetJudet(int id)
        {
            var judet = await _context.Judet.FindAsync(id);

            if (judet == null)
            {
                return NotFound();
            }

            return judet;
        }

        // PUT: api/Judet/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJudet(int id, Judet judet)
        {
            if (id != judet.Idjudet)
            {
                return BadRequest();
            }

            _context.Entry(judet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JudetExists(id))
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

        // POST: api/Judet
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Judet>> PostJudet(Judet judet)
        {
            _context.Judet.Add(judet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJudet", new { id = judet.Idjudet }, judet);
        }

        // DELETE: api/Judet/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Judet>> DeleteJudet(int id)
        {
            var judet = await _context.Judet.FindAsync(id);
            if (judet == null)
            {
                return NotFound();
            }

            _context.Judet.Remove(judet);
            await _context.SaveChangesAsync();

            return judet;
        }

        private bool JudetExists(int id)
        {
            return _context.Judet.Any(e => e.Idjudet == id);
        }
    }
}
