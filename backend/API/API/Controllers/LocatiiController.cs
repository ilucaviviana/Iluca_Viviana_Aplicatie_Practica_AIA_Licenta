using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocatiiController : ControllerBase
    {
        private readonly PayDBContext _context;
        private readonly IConfiguration _config;
        private readonly ILogger<LocatiiController> _logger;

        public LocatiiController(IConfiguration config, PayDBContext context, ILogger<LocatiiController> logger)
        {
            _config = config;
            _context = context;
            _logger = logger;
        }


        // GET: api/Locatii
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Locatii>>> GetLocatii()
        {
            var locatie = await _context.Locatii.ToListAsync();
            return Ok(locatie);
        }

        // GET: api/Locatii/5
     
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserLocation(int id)
        {
            var location = await _context.Locatii.FirstOrDefaultAsync(l => l.IdUser == id);
            if (location == null)
            {
                return Ok();
            }
            return Ok(location);
        }


        // PUT: api/Locatii/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocatii(int id, Locatii locatii)
        {
            if (id != locatii.IdLocatii)
            {
                return BadRequest();
            }

            _context.Entry(locatii).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocatiiExists(id))
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

        // POST: api/Locatii
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Locatii locatiiDto)
        {

            try
            {
                var existingLocation = await _context.Locatii.FirstOrDefaultAsync(l => l.IdUser == locatiiDto.IdUser);
                if (existingLocation != null)
                {
                    return BadRequest("User already has a location");
                }
                var utilizator = await _context.Utilizator.FindAsync(locatiiDto.IdUser);
                if (utilizator == null)
                {
                    return BadRequest("User not found");
                }

                var locatii = new Locatii()
                {
                    IdUser = locatiiDto.IdUser,
                    Judet = locatiiDto.Judet,
                    Oras = locatiiDto.Oras,
                    Strada = locatiiDto.Strada,
                    Bloc = locatiiDto.Bloc,
                    Apartament = locatiiDto.Apartament,
                    Nrlocatari = locatiiDto.Nrlocatari,
                    Nrapometre = locatiiDto.Nrapometre
                };

                await _context.Locatii.AddAsync(locatii);
                await _context.SaveChangesAsync();


                return Ok(); // Return the location ID in the response
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
 

        // DELETE: api/Locatii/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Locatii>> DeleteLocatii(int id)
        {
            var locatii = await _context.Locatii.FindAsync(id);
            if (locatii == null)
            {
                return NotFound();
            }

            _context.Locatii.Remove(locatii);
            await _context.SaveChangesAsync();

            return locatii;
        }

        private bool LocatiiExists(int id)
        {
            return _context.Locatii.Any(e => e.IdLocatii == id);
        }
    }
}

