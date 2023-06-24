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
    public class ApartamentController : ControllerBase
    {
        private readonly PayDBContext _context;

        public ApartamentController(PayDBContext context)
        {
            _context = context;
        }

        // GET: api/Apartament
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Apartament>>> GetApartament()
        {
            return await _context.Apartament.ToListAsync();
        }

        // GET: api/Apartament/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Apartament>> GetApartament(int id)
        {
            var apartament = await _context.Apartament.FindAsync(id);

            if (apartament == null)
            {
                return NotFound();
            }

            return apartament;
        }

        // PUT: api/Apartament/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApartament(int id, Apartament apartament)
        {
            if (id != apartament.Idapartament)
            {
                return BadRequest();
            }

            _context.Entry(apartament).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApartamentExists(id))
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

        // POST: api/Apartament
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Apartament>> PostApartament(Apartament apartament)
        {
            _context.Apartament.Add(apartament);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApartament", new { id = apartament.Idapartament }, apartament);
        }

        // DELETE: api/Apartament/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Apartament>> DeleteApartament(int id)
        {
            var apartament = await _context.Apartament.FindAsync(id);
            if (apartament == null)
            {
                return NotFound();
            }

            _context.Apartament.Remove(apartament);
            await _context.SaveChangesAsync();

            return apartament;
        }

        private bool ApartamentExists(int id)
        {
            return _context.Apartament.Any(e => e.Idapartament == id);
        }
    }
}
