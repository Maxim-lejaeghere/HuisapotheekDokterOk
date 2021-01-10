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
	public class PersoonlijkeApotheekController : Controller
	{
		public List<Persoonlijkeapotheek> PersoonlijkeApotheeks { get; set; }
		public Persoonlijkeapotheek Persoonlijkeapotheek = new Persoonlijkeapotheek();

		private readonly string baseUrl = "https://localhost:5001";
		//"https://localhost:5001"
		//"https://orp12a-huisapotheek-pietervanop.azurewebsites.net"
		// GET: PersoonlijkeApotheek
		public async Task<ActionResult> Index()
		{
			try
			{
				using (var httpClient = new HttpClient())
				{
					using (var response = await httpClient.GetAsync($"{baseUrl}/api/Persoonlijkeapotheeks"))
					{
						var jsonValue = await response.Content.ReadAsStringAsync();

						PersoonlijkeApotheeks = JsonConvert.DeserializeObject<List<Persoonlijkeapotheek>>(jsonValue);
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return View(PersoonlijkeApotheeks);
		}

		// GET: PersoonlijkeApotheek/Details/5
		public async Task<ActionResult> Details(int id)
		{
			try
			{
				using (var httpClient = new HttpClient())
				{
					using (var response = await httpClient.GetAsync($"{baseUrl}/api/Persoonlijkeapotheeks/{id}"))
					{
						var jsonValue = await response.Content.ReadAsStringAsync();

						Persoonlijkeapotheek = JsonConvert.DeserializeObject<Persoonlijkeapotheek>(jsonValue);
					}
				}
			}
			catch (Exception ex)
			{
				ViewBag.Message = "Fout bij het ophalen van de gegevens";

				throw new Exception(ex.Message);
			}
			return View(Persoonlijkeapotheek);
		}

		// GET: PersoonlijkeApotheek/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: PersoonlijkeApotheek/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> CreateAsync(IFormCollection collection)
		{

			try
			{
				if (ModelState.IsValid)
				{
					{
						if (collection["ActiefIngenomen"].ToString() == "true,false")
						{
							Persoonlijkeapotheek.ActiefIngenomen = true;
						}
						else
						{
							Persoonlijkeapotheek.ActiefIngenomen = false;
						}

						if (collection["InApotheek"].ToString() == "true,false")
						{
							Persoonlijkeapotheek.InApotheek = true;
						}
						else
						{
							Persoonlijkeapotheek.InApotheek = false;
						}
						Persoonlijkeapotheek.Dosering = collection["Dosering"].ToString();
						Persoonlijkeapotheek.Groep = collection["Groep"].ToString();
						Persoonlijkeapotheek.Opmerkingen = collection["Opmerkingen"].ToString();
						Persoonlijkeapotheek.Medicijnid = Int32.Parse(collection["Medicijnid"]);
						Persoonlijkeapotheek.Patientid = Int32.Parse(collection["Patientid"]);

					};

					using (var client = new HttpClient())
					{
						client.BaseAddress = new Uri(baseUrl);

						HttpResponseMessage response = new HttpResponseMessage();

						var jsonValue = JsonConvert.SerializeObject(Persoonlijkeapotheek);

						var urlApi = $"{baseUrl}/api/Persoonlijkeapotheeks";

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

		// GET: PersoonlijkeApotheek/Edit/5
		public async Task<ActionResult> EditAsync(int id)
		{
			var persoonlijkeApotheek = new Persoonlijkeapotheek();

			try
			{
				using (var httpClient = new HttpClient())
				{
					using (var response = await httpClient.GetAsync($"{baseUrl}/api/Persoonlijkeapotheeks/{id}"))
					{
						var jsonValue = await response.Content.ReadAsStringAsync();

						persoonlijkeApotheek = JsonConvert.DeserializeObject<Persoonlijkeapotheek>(jsonValue);
					}
				}
			}
			catch (Exception ex)
			{
				ViewBag.Message = "Fout bij het ophalen van de gegevens";

				throw new Exception(ex.Message);
			}
			return View(persoonlijkeApotheek);
		}

		// POST: PersoonlijkeApotheek/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> EditAsync(int id, IFormCollection collection)
		{
			if (ModelState.IsValid)
			{
				{
					
					Persoonlijkeapotheek.Apotheekid = id;
					Persoonlijkeapotheek.Dosering = collection["Dosering"].ToString();
					Persoonlijkeapotheek.Groep = collection["Groep"].ToString();
					Persoonlijkeapotheek.Opmerkingen = collection["Opmerkingen"].ToString();
					Persoonlijkeapotheek.Medicijnid = Int32.Parse(collection["Medicijnid"]);
					Persoonlijkeapotheek.Patientid = Int32.Parse(collection["Patientid"]);

					if (collection["ActiefIngenomen"].ToString() == "true,false")
					{
						Persoonlijkeapotheek.ActiefIngenomen = true;
					}
					else
					{
						Persoonlijkeapotheek.ActiefIngenomen = false;
					}

					if (collection["InApotheek"].ToString() == "true,false")
					{
						Persoonlijkeapotheek.InApotheek = true;
					}
					else
					{
						Persoonlijkeapotheek.InApotheek = false;
					}
					using (var client = new HttpClient())
					{
						client.BaseAddress = new Uri(baseUrl);

						HttpResponseMessage response = new HttpResponseMessage();

						var jsonValue = JsonConvert.SerializeObject(Persoonlijkeapotheek);

						var urlApi = $"{baseUrl}/api/Persoonlijkeapotheeks";

						var postData = new StringContent(jsonValue, System.Text.Encoding.UTF8, "application/json");

						response = await client.PutAsync($"{urlApi}/{Persoonlijkeapotheek.Apotheekid}", postData);
						if (!response.IsSuccessStatusCode)
						{
							return View();
						}
					}
				}
			}

			else
			{
				return View();
			}

				return RedirectToAction(nameof(Index));
			}

			// GET: PersoonlijkeApotheek/Delete/5
			public ActionResult Delete(int id)
			{
				return View();
			}

			// POST: PersoonlijkeApotheek/Delete/5
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
