using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HuisAppotheek.Domain.DAL;
using HuisAppotheek.WepApp.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static HuisAppotheek.Domain.DAL.WeatherForecast;

namespace HuisAppotheek.WepApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

        //public IActionResult Index()
        //{
        //	return View();
        //}
        
        public List<Medicijn> Medicijns { get; set; }
        public Medicijn Medicijn { get; set; }
        private readonly string baseUrl = "https://orp12a-huisapotheek-pietervanop.azurewebsites.net";



        // GET: MedicijnController
        public async Task<ActionResult> Index()
        {
            DateTime today = DateTime.Today;
            DateTime todayPlus30Days = today.AddDays(30);
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync($"{baseUrl}/api/Medicijns"))
                    {
                        var jsonValue = await response.Content.ReadAsStringAsync();

                        Medicijns = JsonConvert.DeserializeObject<List<Medicijn>>(jsonValue);

                        Medicijns = Medicijns.Where(x => today <= x.Vervaldatum && x.Vervaldatum < todayPlus30Days).ToList();

                        //Medicijnen = Medicijnen.Where(x => dateTime <= x.Vervaldatum && x.Vervaldatum <= plus30).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


            return View(Medicijns);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string city)
        {
            var weather = new WeatherForecast();
            string weatherUrl = "https://millenniumfalcon.azurewebsites.net/api/weather";
            if (city != null)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        using (var httpClient = new HttpClient())
                        {
                            using (var response = await httpClient.GetAsync($"{weatherUrl}/{city}"))
                            {
                                var apiValues = await response.Content.ReadAsStringAsync();
                                weather = JsonConvert.DeserializeObject<WeatherForecast>(apiValues);
                                if (weather.cod == 404)
                                {
                                    throw new Exception("Fout bij het ophalen van gegevens");
                                }
                                else
                                {
                                    ViewBag.city = weather.name;
                                    ViewBag.timezone = weather.timezone;
                                    ViewBag.mintemp = Math.Round(weather.main.temp_min);
                                    ViewBag.maxtemp = Math.Round(weather.main.temp_max);
                                    ViewBag.maintemp = Math.Round(weather.main.temp);

                                    string UnixTimeToTime(double timestamp)
                                    {
                                        System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
                                        dateTime = dateTime.AddSeconds(timestamp);
                                        string time = /*dateTime.ToShortDateString() + " " + */dateTime.ToShortTimeString();
                                        return time;
                                    }


                                    ViewBag.sunrise = UnixTimeToTime(weather.sys.sunrise);
                                    ViewBag.sunset = UnixTimeToTime(weather.sys.sunset);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "Fout bij het ophalen van de gegevens";
                        throw new Exception(ex.Message);
                    }
                }
            }
            //return View();
            DateTime today = DateTime.Today;
            DateTime todayPlus30Days = today.AddDays(30);
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync($"{baseUrl}/api/Medicijns"))
                    {
                        var jsonValue = await response.Content.ReadAsStringAsync();

                        Medicijns = JsonConvert.DeserializeObject<List<Medicijn>>(jsonValue);

                        Medicijns = Medicijns.Where(x => today <= x.Vervaldatum && x.Vervaldatum < todayPlus30Days).ToList();

                        //Medicijnen = Medicijnen.Where(x => dateTime <= x.Vervaldatum && x.Vervaldatum <= plus30).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


            return View(Medicijns);
        }

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
