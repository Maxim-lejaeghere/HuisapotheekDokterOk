using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HuisAppotheek.Domain.DAL;

namespace HuisAppotheek.WepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoktersController : ControllerBase
    {
        private readonly huisapotheekContext _context;

        public DoktersController(huisapotheekContext context)
        {
            _context = context;
        }

        // GET: api/Dokters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dokter>>> GetDokter()
        {
            return await _context.Dokter.ToListAsync();
        }

        // GET: api/Dokters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dokter>> GetDokter(int id)
        {
            var dokter = await _context.Dokter.FindAsync(id);

            if (dokter == null)
            {
                return NotFound();
            }

            return dokter;
        }

        // PUT: api/Dokters/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDokter(int id, Dokter dokter)
        {
            if (id != dokter.Dokterid)
            {
                return BadRequest();
            }

            _context.Entry(dokter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DokterExists(id))
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

        // POST: api/Dokters
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Dokter>> PostDokter(Dokter dokter)
        {
            _context.Dokter.Add(dokter);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDokter", new { id = dokter.Dokterid }, dokter);
        }

        // DELETE: api/Dokters/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Dokter>> DeleteDokter(int id)
        {
            var dokter = await _context.Dokter.FindAsync(id);
            if (dokter == null)
            {
                return NotFound();
            }

            _context.Dokter.Remove(dokter);
            await _context.SaveChangesAsync();

            return dokter;
        }

        private bool DokterExists(int id)
        {
            return _context.Dokter.Any(e => e.Dokterid == id);
        }
    }
}
