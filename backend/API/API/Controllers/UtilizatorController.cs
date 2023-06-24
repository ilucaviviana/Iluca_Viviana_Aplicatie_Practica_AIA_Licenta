using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using API.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Cors;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class UtilizatorController : ControllerBase
    {
        private readonly PayDBContext _context;
        private readonly IConfiguration _config;
        private readonly AppSettings _appSettings;


        public UtilizatorController(IConfiguration config, PayDBContext context, IOptions<AppSettings> appSettings)
        {
            _config = config;
            _context = context;
            _appSettings = appSettings.Value;

        }

        // GET: api/Utilizator
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Utilizator>>> GetUtilizator()
        {
            var utilizator = await _context.Utilizator.ToListAsync();
            return Ok(utilizator);
        }

        // GET: api/Utilizator/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Utilizator>> GetUtilizator(int id)
        {
            var utilizator = await _context.Utilizator.FindAsync(id);

            if (utilizator == null)
            {
                return NotFound();
            }

            return utilizator;
        }

        // PUT: api/Utilizator/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUtilizator(int id, Utilizator utilizator)
        {
            if (id != utilizator.Id)
            {
                return BadRequest();
            }

            _context.Entry(utilizator).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtilizatorExists(id))
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

        // POST: api/Utilizator
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<string>> PostLogin([FromBody] Utilizator utilizator)
        {

            var userAvailable = _context.Utilizator.Where(u => u.Email == utilizator.Email && u.Parola == utilizator.Parola && u.Tip == utilizator.Tip).FirstOrDefault();
            if (userAvailable != null)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtConfig:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userAvailable.Id.ToString()),
                new Claim("Tip", userAvailable.Tip.ToString())
                };

                var tokenOptions = new JwtSecurityToken(
                    issuer: _config["JwtConfig:Issuer"],
                    audience: _config["JwtConfig:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddDays(7),
                    signingCredentials: credentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return Ok(tokenString);
            }

            return Ok("Failure");
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<Utilizator>> PostUtilizator(Utilizator utilizator)
        {
            if (utilizator == null)
                return BadRequest();

            await _context.Utilizator.AddAsync(utilizator);
            await _context.SaveChangesAsync();

            // utilizator.Token = CreateJwt(utilizator);
            return Ok(new
            {
                // Token = utilizator.Token,
                Message = "User Registered!"
            });
        }


        // DELETE: api/Utilizator/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Utilizator>> DeleteUtilizator(int id)
        {
            var utilizator = await _context.Utilizator.FindAsync(id);
            if (utilizator == null)
            {
                return NotFound();
            }

            _context.Utilizator.Remove(utilizator);
            await _context.SaveChangesAsync();

            return utilizator;
        }

        private bool UtilizatorExists(int id)
        {
            return _context.Utilizator.Any(e => e.Id == id);
        }

        private string CreateJwt(Utilizator utilizator)
        {
            // authentication successful so generate jwt token
            var jwtTokenHandler = new JwtSecurityTokenHandler();
           // var key = Encoding.ASCII.GetBytes(_appSettings.Secret); // Use the secret key from app settings
            var key = Encoding.ASCII.GetBytes(_config.GetValue<string>("AppSettings:Secret"));
            var identity = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Role, utilizator.Tip.ToString()),
            new Claim(ClaimTypes.Name, $"{utilizator.Nume}{utilizator.Prenume}"),
            new Claim(ClaimTypes.NameIdentifier, utilizator.Id.ToString()) // Add the user ID as a claim
                });
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            return jwtTokenHandler.WriteToken(token);
        }
    }
}
