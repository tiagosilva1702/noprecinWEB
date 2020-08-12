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
    public class ProdutosController : Controller
    {
        private readonly string apiUrl = " https://localhost:44328/api/produtos";

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

        public async Task<IActionResult> Editar(Guid id)
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
