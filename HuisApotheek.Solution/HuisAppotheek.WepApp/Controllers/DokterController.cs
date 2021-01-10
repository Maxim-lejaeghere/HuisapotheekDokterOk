using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using HuisAppotheek.Domain.DAL;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

namespace HuisAppotheek.WepApp.Controllers
{
	public class DokterController : Controller
	{
		public List<Dokter> Dokters { get; set; }
		public Dokter Dokter { get; set; }
		private readonly string baseUrl = "https://orp12a-huisapotheek-pietervanop.azurewebsites.net";
		// GET: DokterController
		public async Task<ActionResult> Index()
		{
			try
			{
				using (var httpClient = new HttpClient())
				{
					using (var response = await httpClient.GetAsync($"{baseUrl}/api/Dokters"))
					{
						var jsonValue = await response.Content.ReadAsStringAsync();

						Dokters = JsonConvert.DeserializeObject<List<Dokter>>(jsonValue);
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}


			return View(Dokters);
		}

		// GET: DokterController/Details/5
		public async Task<ActionResult> DetailsAsync(int id)
		{


			try
			{
				using (var httpClient = new HttpClient())
				{
					using (var response = await httpClient.GetAsync($"{baseUrl}/api/dokters/{id}"))
					{
						var jsonValue = await response.Content.ReadAsStringAsync();

						Dokter = JsonConvert.DeserializeObject<Dokter>(jsonValue);
					}
				}
			}
			catch (Exception ex)
			{
				ViewBag.Message = "Fout bij het ophalen van de gegevens";

				throw new Exception(ex.Message);
			}
			return View(Dokter);
		}

		// GET: DokterController/Edit/5
		public async Task<ActionResult> Edit(int id)
		{
			var dokter = new Dokter();

			try
			{
				using (var httpClient = new HttpClient())
				{
					using (var response = await httpClient.GetAsync($"{baseUrl}/api/dokters/{id}"))
					{
						var jsonValue = await response.Content.ReadAsStringAsync();

						dokter = JsonConvert.DeserializeObject<Dokter>(jsonValue);
					}
				}
			}
			catch (Exception ex)
			{
				ViewBag.Message = "Fout bij het ophalen van de gegevens";

				throw new Exception(ex.Message);
			}
			return View(dokter);
		}

		// GET: DokterController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: DokterController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> CreateAsync(IFormCollection collection)
		{
			Dokter = new Dokter();
			try
			{
				var Collection = collection;
				if (ModelState.IsValid)
				{
					// uitlezen van de formcollection data en opvullen in hun respectievelijke properties
					{
						Dokter.Achternaam = collection["Achternaam"].ToString();
						Dokter.Voornaam = collection["Voornaam"].ToString();
						Dokter.Straat = collection["Straat"].ToString();
						Dokter.Huisnummer = collection["Huisnummer"].ToString();
						Dokter.Bus = collection["Bus"].ToString();
						Dokter.Postcode = collection["Postcode"].ToString();
						Dokter.Stad = collection["Stad"].ToString();
						Dokter.Land = collection["Land"].ToString();
						Dokter.Telefoon = collection["Telefoon"].ToString();
						Dokter.Mobiel = collection["Mobiel"].ToString();
						Dokter.Email = collection["Email"].ToString();
						Dokter.Reservatieurl = collection["Reservatieurl"].ToString();
					};

					using (var client = new HttpClient())
					{
						client.BaseAddress = new Uri(baseUrl);

						HttpResponseMessage response = new HttpResponseMessage();

						var jsonValue = JsonConvert.SerializeObject(Dokter);

						var urlApi = $"{baseUrl}/api/dokters";

						var postData = new StringContent(jsonValue, System.Text.Encoding.UTF8, "application/json");

						response = await client.PostAsync(urlApi, postData);

						// controle of het pakje correct is afgeleverd
						if (!response.IsSuccessStatusCode)
						{
							ViewBag.Message = "Niet afgeleverd";
							return View();
						}

					}

				}
				else
				{
					ViewBag.Message = "Er staat een fout in het formulier";
				}

				return RedirectToAction(nameof(Index));

			}
			catch (Exception ex)
			{
				var msg = ex.Message;

				ViewBag.Message = msg;

				return View();
			}
		}

		// POST: DokterController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, IFormCollection collection)
		{
				
			if (ModelState.IsValid)
			{
				Dokter = new Dokter();
				Dokter.Dokterid = id;
				Dokter.Achternaam = collection["Achternaam"].ToString();
				Dokter.Voornaam = collection["Voornaam"].ToString();
				Dokter.Straat = collection["Straat"].ToString();
				Dokter.Huisnummer = collection["Huisnummer"].ToString();
				Dokter.Bus = collection["Bus"].ToString();
				Dokter.Postcode = collection["Postcode"].ToString();
				Dokter.Stad = collection["Stad"].ToString();
				Dokter.Land = collection["Land"].ToString();
				Dokter.Telefoon = collection["Telefoon"].ToString();
				Dokter.Mobiel = collection["Mobiel"].ToString();
				Dokter.Email = collection["Email"].ToString();
				Dokter.Reservatieurl = collection["Reservatieurl"].ToString();
				


				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(baseUrl);

					HttpResponseMessage response = new HttpResponseMessage();

					var jsonValue = JsonConvert.SerializeObject(Dokter);

					var urlApi = $"{baseUrl}/api/Dokters";

					var postData = new StringContent(jsonValue, System.Text.Encoding.UTF8, "application/json");

					response = await client.PutAsync($"{urlApi}/{Dokter.Dokterid}", postData);
					if (!response.IsSuccessStatusCode)
					{
						return View();
					}
				}
			}

			else
			{
				return View();
			}

			return RedirectToAction(nameof(Index));
		}

		// GET: DokterController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: DokterController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
