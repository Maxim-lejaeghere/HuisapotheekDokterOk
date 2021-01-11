using HuisAppotheek.Domain.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HuisAppotheek.WepApp.Controllers
{
    public class MedicijnController : Controller
    {
        public List<Medicijn> Medicijns { get; set; }
        public Medicijn Medicijn { get; set; }
        private readonly string baseUrl = "https://orp12a-huisapotheek-pietervanop.azurewebsites.net";


        // GET: MedicijnController
        public async Task<ActionResult> Index()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync($"{baseUrl}/api/Medicijns"))
                    {
                        var jsonValue = await response.Content.ReadAsStringAsync();

                        Medicijns = JsonConvert.DeserializeObject<List<Medicijn>>(jsonValue);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


            return View(Medicijns);
        }

        // GET: MedicijnController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync($"{baseUrl}/api/medicijns/{id}"))
                    {
                        var jsonValue = await response.Content.ReadAsStringAsync();

                        Medicijn = JsonConvert.DeserializeObject<Medicijn>(jsonValue);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Fout bij het ophalen van de gegevens";

                throw new Exception(ex.Message);
            }
            return View(Medicijn);
        }

        // GET: MedicijnController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var medicijn = new Medicijn();

            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync($"{baseUrl}/api/medicijns/{id}"))
                    {
                        var jsonValue = await response.Content.ReadAsStringAsync();

                        medicijn = JsonConvert.DeserializeObject<Medicijn>(jsonValue);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Fout bij het ophalen van de gegevens";

                throw new Exception(ex.Message);
            }
            return View(medicijn);
        }

        // GET: MedicijnController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MedicijnController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            Medicijn = new Medicijn();
            try
            {
                var Collection = collection;
                if (ModelState.IsValid)
                {
                    // uitlezen van de formcollection data en opvullen in hun respectievelijke properties
                    {
                    Medicijn.Volledigenaam = collection["Volledigenaam"].ToString();
                    Medicijn.Groep = collection["Groep"].ToString();
                    Medicijn.Vervaldatum = DateTime.Parse(collection["Vervaldatum"]);

                    if (collection["OpVoorschrift"].ToString() == "true,false")
                    {
                        Medicijn.OpVoorschrift = true;
                    }
                    else
                    {
                        Medicijn.OpVoorschrift = false;
                    }

                    //Medicijn.OpVoorschrift = Convert.ToBoolean(collection["OpVoorschrift"].ToString());
                    Medicijn.Postcode = collection["Postcode"].ToString();
                    Medicijn.Bijsluiter = collection["Bijsluiter"].ToString();
                    Medicijn.UrlBijsluiter = collection["UrlBijsluiter"].ToString();

                    //Moeten deze er ook nog bij?
                    if (collection["Dokterid"] != string.Empty)
                    {
                      Medicijn.Dokterid = int.Parse(collection["Dokterid"]);
                    }
                    
                    // Medicijn.Dokter = collection["Dokter"].ToString();
                    // Medicijn.Persoonlijkeapotheek = collection["Persoonlijkeapotheek"].ToString();
                    };

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUrl);

                        HttpResponseMessage response = new HttpResponseMessage();

                        var jsonValue = JsonConvert.SerializeObject(Medicijn);

                        var urlApi = $"{baseUrl}/api/medicijns";

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

        // POST: MedicijnController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
        {
            Medicijn = new Medicijn();
            try
            {
                //var Collection = collection;
                if (ModelState.IsValid)
                {
                    Medicijn.Medicijnid = id;
                    Medicijn.Volledigenaam = collection["Volledigenaam"].ToString();
                    Medicijn.Groep = collection["Groep"].ToString();
                    Medicijn.Vervaldatum = DateTime.Parse(collection["Vervaldatum"]);

                    if (collection["OpVoorschrift"].ToString() == "true,false")
                    {
                        Medicijn.OpVoorschrift = true;
                    }
                    else
                    {
                        Medicijn.OpVoorschrift = false;
                    }

                    //Medicijn.OpVoorschrift = Convert.ToBoolean(collection["OpVoorschrift"].ToString());
                    //Medicijn.Postcode = collection["Postcode"].ToString();
                    Medicijn.Bijsluiter = collection["Bijsluiter"].ToString();
                    Medicijn.UrlBijsluiter = collection["UrlBijsluiter"].ToString();

                    //Moeten deze er ook nog bij?
                    if (collection["Dokterid"] != string.Empty)
                    {
                        Medicijn.Dokterid = int.Parse(collection["Dokterid"]);
                    }
                    // Medicijn.Dokter = collection["Dokter"].ToString();
                    // Medicijn.Persoonlijkeapotheek = collection["Persoonlijkeapotheek"].ToString();


                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUrl);

                        // Initialiseren
                        HttpResponseMessage response = new HttpResponseMessage();

                        // Omzetten van het businessmodel naar json formaat
                        var jsonValue = JsonConvert.SerializeObject(Medicijn);

                        // opbouw van de url va de post api call
                        var urlApi = $"{baseUrl}/api/medicijns";

                        // Het inpakken van de data in de correcte verpakking
                        var postData = new StringContent(jsonValue, System.Text.Encoding.UTF8, "application/json");

                        // HTTP Post -> verstuur het pakje naar het correcte adres
                        response = await client.PutAsync($"{urlApi}/{Medicijn.Medicijnid}", postData);

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

        // GET: MedicijnController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync($"{baseUrl}/api/medicijns/{id}"))
                    {
                        var jsonValue = await response.Content.ReadAsStringAsync();

                        Medicijn = JsonConvert.DeserializeObject<Medicijn>(jsonValue);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Fout bij het ophalen van de gegevens";

                throw new Exception(ex.Message);
            }
            return View(Medicijn);
        }



        // [Authorize(Roles = "Administrator")]
        // POST: MedicijnController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);

                    // Initialiseren
                    HttpResponseMessage response = new HttpResponseMessage();

                    // Omzetten van het businessmodel naar json formaat
                    var jsonValue = JsonConvert.SerializeObject(Medicijn);

                    // opbouw van de url va de post api call
                    var urlApi = $"{baseUrl}/api/medicijns";

                    // Het inpakken van de data in de correcte verpakking
                    var postData = new StringContent(jsonValue, System.Text.Encoding.UTF8, "application/json");

                    // HTTP Post -> verstuur het pakje naar het correcte adres
                    response = await client.DeleteAsync($"{urlApi}/{id}");

                    // controle of het pakje correct is afgeleverd
                    if (!response.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Niet afgeleverd";
                        return View();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
