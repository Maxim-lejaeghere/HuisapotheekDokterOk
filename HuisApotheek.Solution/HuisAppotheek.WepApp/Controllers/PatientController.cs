using HuisAppotheek.Domain.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HuisAppotheek.WepApp.Controllers
{
    public class PatientController : Controller
    {
        public List<Patient> Patients { get; set; }

        //private readonly DAL.Dokter _context;
        private readonly string baseUrl = "https://orp12a-huisapotheek-pietervanop.azurewebsites.net";

        //"https://localhost:5001"
        //"https://orp12a-huisapotheek-pietervanop.azurewebsites.net"
        // GET: PatientController
        public async Task<ActionResult> IndexAsync()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync($"{baseUrl}/api/patients"))
                    {
                        var jsonValue = await response.Content.ReadAsStringAsync();

                        Patients = JsonConvert.DeserializeObject<List<Patient>>(jsonValue);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return View(Patients);
        }

        // GET: PatientController/Details/5
        public async Task<ActionResult> DetailsAsync(int id)
        {
            var patient = new Patient();

            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync($"{baseUrl}/api/patients/{id}"))
                    {
                        var jsonValue = await response.Content.ReadAsStringAsync();

                        patient = JsonConvert.DeserializeObject<Patient>(jsonValue);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Fout bij het ophalen van de gegevens";

                throw new Exception(ex.Message);
            }
            return View(patient);
        }

        // GET: PatientController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PatientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(IFormCollection collection)
        {
            Patient patient = new Patient();
            try
            {
                var Collection = collection;
                if (ModelState.IsValid)
                {
                    // uitlezen van de formcollection data en opvullen in hun respectievelijke properties
                    {
                        patient.Achternaam = collection["Achternaam"].ToString();
                        patient.Voornaam = collection["Voornaam"].ToString();
                        patient.Geboortedatumdatum = DateTime.Parse(collection["Geboortedatumdatum"]);
                        patient.Email = collection["Email"].ToString();
                    };

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUrl);
                        HttpResponseMessage response = new HttpResponseMessage();
                        var jsonValue = JsonConvert.SerializeObject(patient);
                        var urlApi = $"{baseUrl}/api/patients";
                        var postData = new StringContent(jsonValue, System.Text.Encoding.UTF8, "application/json");
                        response = await client.PostAsync(urlApi, postData);
                        if (!response.IsSuccessStatusCode)
                        {
                            ViewBag.Message = "Mislukt";
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

        // GET: PatientController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PatientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: PatientController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PatientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }
    }
}