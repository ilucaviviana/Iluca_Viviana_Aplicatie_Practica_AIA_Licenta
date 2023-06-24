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
    public class FacturaController : ControllerBase
    {
        private readonly PayDBContext _context;

        public FacturaController(PayDBContext context)
        {
            _context = context;
        }

        // GET: api/Factura
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Factura>>> GetFactura()
        {
            return await _context.Factura.ToListAsync();
        }

        // GET: api/Factura/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Factura>> GetFactura(int id)
        {
            var factura = await _context.Factura.FindAsync(id);

            if (factura == null)
            {
                return NotFound();
            }

            return factura;
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<IEnumerable<Factura>>> GetFacturaByUserId(int id)
        {
            var facturas = await _context.Factura.Where(f => f.IdUtilizator == id).ToListAsync();

            if (facturas == null)
            {
                return NotFound();
            }

            return facturas;
        }


        // PUT: api/Factura/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFactura(int id, Factura factura)
        {
            if (id != factura.IdFactura)
            {
                return BadRequest();
            }

            _context.Entry(factura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacturaExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Factura>> PostFactura(Factura factura)
        {
            // Get the latest tarif
            var latestTarif = await _context.Tarife.OrderByDescending(t => t.TransmitereData).FirstOrDefaultAsync();
            if (latestTarif == null)
            {
                return BadRequest("Tarif not found");
            }
            // You already have all the necessary data in factura object
            _context.Factura.Add(factura);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetFactura", new { id = factura.IdFactura }, factura);
        }

        // DELETE: api/Factura/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Factura>> DeleteFactura(int id)
        {
            var factura = await _context.Factura.FindAsync(id);
            if (factura == null)
            {
                return NotFound();
            }

            _context.Factura.Remove(factura);
            await _context.SaveChangesAsync();

            return factura;
        }

        private bool FacturaExists(int id)
        {
            return _context.Factura.Any(e => e.IdFactura == id);
        }
    }
}
