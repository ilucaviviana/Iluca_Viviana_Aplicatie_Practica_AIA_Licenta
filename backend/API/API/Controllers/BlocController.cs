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
    public class BlocController : ControllerBase
    {
        private readonly PayDBContext _context;

        public BlocController(PayDBContext context)
        {
            _context = context;
        }

        // GET: api/Bloc
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bloc>>> GetBloc()
        {
            return await _context.Bloc.ToListAsync();
        }

        // GET: api/Bloc/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bloc>> GetBloc(int id)
        {
            var bloc = await _context.Bloc.FindAsync(id);

            if (bloc == null)
            {
                return NotFound();
            }

            return bloc;
        }

        // PUT: api/Bloc/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBloc(int id, Bloc bloc)
        {
            if (id != bloc.Idbloc)
            {
                return BadRequest();
            }

            _context.Entry(bloc).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlocExists(id))
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

        // POST: api/Bloc
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Bloc>> PostBloc(Bloc bloc)
        {
            _context.Bloc.Add(bloc);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBloc", new { id = bloc.Idbloc }, bloc);
        }

        // DELETE: api/Bloc/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Bloc>> DeleteBloc(int id)
        {
            var bloc = await _context.Bloc.FindAsync(id);
            if (bloc == null)
            {
                return NotFound();
            }

            _context.Bloc.Remove(bloc);
            await _context.SaveChangesAsync();

            return bloc;
        }

        private bool BlocExists(int id)
        {
            return _context.Bloc.Any(e => e.Idbloc == id);
        }
    }
}
