using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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

        private readonly string apiUrlv = "https://localhost:44328/api/vendas";
        private readonly string apiUrl = "https://localhost:44328/api/produtos";

        public async Task<IActionResult> Index(Guid id)
        {
            Usuario usuario = new Usuario();
            usuario.AcessToken = HttpContext.Session.Get<String>("AcessToken");

            if (usuario.AcessToken == null)
            {
                ViewData["Error"] = "Para realizar uma compra é necessário logar no sistema!";
                return RedirectToAction("Login", "Usuarios");
            }

            Produtos produtos = new Produtos();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + usuario.AcessToken);

                using (var response = await httpClient.GetAsync(apiUrl + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    produtos = JsonConvert.DeserializeObject<Produtos>(apiResponse);
                }
            }
            return View(produtos);
        }

        public async Task<IActionResult> Venda(Guid Id, Produtos produto)
        {
            Usuario usuario = new Usuario();
            usuario.Id = HttpContext.Session.Get<Guid>("Id");
            usuario.AcessToken = HttpContext.Session.Get<String>("AcessToken");

            Vendas venda = new Vendas();
            venda.ProdutoId = Id;
            venda.Data = DateTime.Now;
            venda.EmailComprador = HttpContext.Session.Get<String>("Email");

            produto.Ativo = false;


            //TODO: Acesso direto ao banco de dados
            //_context.Add(venda);
            //await _context.SaveChangesAsync();

            //TODO: Acesso API
            var json = JsonConvert.SerializeObject(venda);
            var jsonP = JsonConvert.SerializeObject(produto);

            var postRequest = new StringContent(json, Encoding.UTF8, "application/json");
            var postRequestP = new StringContent(jsonP, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + usuario.AcessToken);


                using (var response = await httpClient.PutAsync(apiUrl + "/" + produto.Id, postRequestP))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }

                using (var response = await httpClient.PostAsync(apiUrlv, postRequest).ConfigureAwait(true))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
