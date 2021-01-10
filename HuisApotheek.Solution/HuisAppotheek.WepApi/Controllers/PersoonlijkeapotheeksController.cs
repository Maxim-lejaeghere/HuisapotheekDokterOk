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
	public class PersoonlijkeapotheeksController : ControllerBase
	{
		private readonly huisapotheekContext _context;

		public PersoonlijkeapotheeksController(huisapotheekContext context)
		{
			_context = context;
		}

		// GET: api/Persoonlijkeapotheeks
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Persoonlijkeapotheek>>> GetPersoonlijkeapotheek()
		{
			return await _context.Persoonlijkeapotheek.ToListAsync();
		}

		// GET: api/Persoonlijkeapotheeks/5
		[HttpGet("{id}")]

		public async Task<ActionResult<Persoonlijkeapotheek>> GetPersoonlijkeapotheek(int id)
		{
			var persoonlijkeapotheek = await _context.Persoonlijkeapotheek.FindAsync(id);

			if (persoonlijkeapotheek == null)
			{
				return NotFound();
			}

			return persoonlijkeapotheek;
		}
		//Get all medicijnen form persoonlijke apotheek where Inapotheek is true
		//GET : api/Persoonlijkeapotheeks/AanwezigeMedicijnen

		[HttpGet]
		[Route("AanwezigeMedicijnen/{id}")]
		public async Task<ActionResult<IEnumerable<Persoonlijkeapotheek>>> GetAanwezigeMedicijnen(int id)
		{
			var medicijnen = await _context.Persoonlijkeapotheek.ToListAsync();
			var medicijnenAanwezig = medicijnen.Where(med => med.InApotheek == true).ToArray();
			var medicijnenAanwezigPatientId = medicijnenAanwezig.Where(med => med.Patientid == id).ToArray();
			return medicijnenAanwezigPatientId;
		}
		// Get all medicijnid from patientid where ActiefIngenomen is true
		//GET : api/Persoonlijkeapotheeks/ActiefIngenomen/{id}

		[HttpGet]
		[Route("ActiefIngenomen")]

		public async Task<ActionResult<IEnumerable<Persoonlijkeapotheek>>> GetActiefIngenomenMedicijnen()
		{
		  var medicijnen = await _context.Persoonlijkeapotheek.ToListAsync();
	    var medicijnenActief = medicijnen.Where(med => med.ActiefIngenomen == true).ToArray();

		  return medicijnenActief;
		}




		//GET : api/Persoonlijkeapotheeks/5/

		// PUT: api/Persoonlijkeapotheeks/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		[HttpPut("{id}")]
		public async Task<IActionResult> PutPersoonlijkeapotheek(int id, Persoonlijkeapotheek persoonlijkeapotheek)
		{
			if (id != persoonlijkeapotheek.Apotheekid)
			{
				return BadRequest();
			}

			_context.Entry(persoonlijkeapotheek).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!PersoonlijkeapotheekExists(id))
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

		// POST: api/Persoonlijkeapotheeks
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		[HttpPost]
		public async Task<ActionResult<Persoonlijkeapotheek>> PostPersoonlijkeapotheek(Persoonlijkeapotheek persoonlijkeapotheek)
		{
			_context.Persoonlijkeapotheek.Add(persoonlijkeapotheek);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetPersoonlijkeapotheek", new { id = persoonlijkeapotheek.Apotheekid }, persoonlijkeapotheek);
		}

		// DELETE: api/Persoonlijkeapotheeks/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<Persoonlijkeapotheek>> DeletePersoonlijkeapotheek(int id)
		{
			var persoonlijkeapotheek = await _context.Persoonlijkeapotheek.FindAsync(id);
			if (persoonlijkeapotheek == null)
			{
				return NotFound();
			}

			_context.Persoonlijkeapotheek.Remove(persoonlijkeapotheek);
			await _context.SaveChangesAsync();

			return persoonlijkeapotheek;
		}

		private bool PersoonlijkeapotheekExists(int id)
		{
			return _context.Persoonlijkeapotheek.Any(e => e.Apotheekid == id);
		}
	}
}
