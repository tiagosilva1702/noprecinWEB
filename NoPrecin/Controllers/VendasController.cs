using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NoPrecin.Models;

namespace NoPrecin.Controllers
{
    public class VendasController : Controller
    {
        /*public IActionResult Index()
        {
            return View();
        }*/

        private readonly string apiUrl = "https://localhost:44328/api/produtos";

        public async Task<IActionResult> Index(Guid id)
        {

            Produtos produtos = new Produtos();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(apiUrl + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    produtos = JsonConvert.DeserializeObject<Produtos>(apiResponse);
                }
            }
            return View(produtos);
        }
    }
}
