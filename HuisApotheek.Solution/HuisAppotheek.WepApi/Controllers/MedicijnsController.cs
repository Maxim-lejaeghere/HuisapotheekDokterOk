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
    public class MedicijnsController : ControllerBase
    {
        private readonly huisapotheekContext _context;

        public MedicijnsController(huisapotheekContext context)
        {
            _context = context;
        }

        // GET: api/Medicijns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medicijn>>> GetMedicijn()
        {
            return await _context.Medicijn.ToListAsync();
        }

        // GET: api/Medicijns/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Medicijn>> GetMedicijn(int id)
        {
            var medicijn = await _context.Medicijn.FindAsync(id);

            if (medicijn == null)
            {
                return NotFound();
            }

            return medicijn;
        }

        // PUT: api/Medicijns/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicijn(int id, Medicijn medicijn)
        {
            if (id != medicijn.Medicijnid)
            {
                return BadRequest();
            }

            _context.Entry(medicijn).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicijnExists(id))
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

        // POST: api/Medicijns
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Medicijn>> PostMedicijn(Medicijn medicijn)
        {
            _context.Medicijn.Add(medicijn);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMedicijn", new { id = medicijn.Medicijnid }, medicijn);
        }

        // DELETE: api/Medicijns/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Medicijn>> DeleteMedicijn(int id)
        {
            var medicijn = await _context.Medicijn.FindAsync(id);
            if (medicijn == null)
            {
                return NotFound();
            }

            _context.Medicijn.Remove(medicijn);
            await _context.SaveChangesAsync();

            return medicijn;
        }

        private bool MedicijnExists(int id)
        {
            return _context.Medicijn.Any(e => e.Medicijnid == id);
        }
    }
}
