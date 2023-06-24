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
    public class TarifeController : ControllerBase
    {
        private readonly PayDBContext _context;

        public TarifeController(PayDBContext context)
        {
            _context = context;
        }

        // GET: api/Tarife
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarife>>> GetTarife()
        {
            return await _context.Tarife.ToListAsync();
        }

        // GET: api/Tarife/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarife>> GetTarife(int id)
        {
            var tarife = await _context.Tarife.FindAsync(id);

            if (tarife == null)
            {
                return NotFound();
            }

            return tarife;
        }

       /* [HttpGet("date/{id}")]
        public async Task<ActionResult<DateTime>> GetLastSubmissionDate(int id)
        {
            var lastSubmission = await _context.Tarife
              .Where(t => t.Id == id)
              .OrderByDescending(t => t.TransmitereData)
              .FirstOrDefaultAsync();

            if (lastSubmission == null)
            {
                return NotFound();
            }

            return Ok(lastSubmission);
        }*/
        [HttpGet("date/{id}")]
        public async Task<ActionResult<Tarife>> GetLastSubmissionDate(int id)
        {
            var lastSubmission = await _context.Tarife
                .Where(t => t.Id == id)
                .OrderByDescending(t => t.TransmitereData)
                .FirstOrDefaultAsync();

            if (lastSubmission == null)
            {
                return NotFound();
            }

            return Ok(lastSubmission);
        }




        // PUT: api/Tarife/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarife(int id, Tarife tarife)
        {
            if (id != tarife.IdTarife)
            {
                return BadRequest();
            }

            _context.Entry(tarife).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TarifeExists(id))
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

        // POST: api/Tarife
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Tarife>> PostTarife(Tarife tarife)
        {
            _context.Tarife.Add(tarife);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTarife", new { id = tarife.IdTarife }, tarife);
        }

        // DELETE: api/Tarife/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Tarife>> DeleteTarife(int id)
        {
            var tarife = await _context.Tarife.FindAsync(id);
            if (tarife == null)
            {
                return NotFound();
            }

            _context.Tarife.Remove(tarife);
            await _context.SaveChangesAsync();

            return tarife;
        }

        private bool TarifeExists(int id)
        {
            return _context.Tarife.Any(e => e.IdTarife == id);
        }
    }
}
