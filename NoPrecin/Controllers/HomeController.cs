using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NoPrecin.Models;
using Microsoft.AspNetCore.Http;

namespace NoPrecin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private static string _urlBase;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<Produtos> listaProdutos = new List<Produtos>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(apiUrl))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    listaProdutos = JsonConvert.DeserializeObject<List<Produtos>>(apiResponse);
                }
            }
            return View(listaProdutos);
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
