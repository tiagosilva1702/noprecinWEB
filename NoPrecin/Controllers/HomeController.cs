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
using Microsoft.AspNetCore.Hosting;

namespace NoPrecin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //Define uma instância de IHostingEnvironment
        IWebHostEnvironment _appEnvironment;

        private readonly string apiUrl = "https://localhost:44328/api/produtos/";
        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _appEnvironment = env;
        }

        public async Task<IActionResult> Index()
        {
            List<Produtos> listaProdutos = new List<Produtos>();
            //< obtém o caminho físico da pasta wwwroot >
            string caminho_WebRoot = _appEnvironment.WebRootPath;
            // monta o caminho onde vamos salvar o arquivo :  ~\wwwroot\Arquivos\Arquivos_Usuario\Recebidos
            string caminhoDestinoArquivo = caminho_WebRoot + "\\img\\";

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(apiUrl))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    listaProdutos = JsonConvert.DeserializeObject<List<Produtos>>(apiResponse);
                }
            }

            return View(listaProdutos.Where(x => x.Vendido == false && x.Ativo == true));
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
