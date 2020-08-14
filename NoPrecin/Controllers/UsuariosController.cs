using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NoPrecin.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NoPrecin.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly string apiUrl = "https://localhost:44328/api/";

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Usuario model)
        {
            Usuario usuario = new Usuario();

            JObject jObject = new JObject();

            var json = JsonConvert.SerializeObject(model);

            var postRequest = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync(apiUrl + "entrar", postRequest).ConfigureAwait(false))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    jObject = (JObject)JsonConvert.DeserializeObject(apiResponse);
                    
                    if (Convert.ToBoolean(jObject["sucess"]) == false)
                    {
                        ViewData["Error"] = jObject["errors"];
                        return View();
                    }

                    usuario.AcessToken = jObject["data"].Value<string>("acessToken");
                    usuario.Id = Guid.Parse(jObject["data"]["userToken"].Value<string>("id"));
                    usuario.Email = jObject["data"]["userToken"].Value<string>("email");
                }
            }

            HttpContext.Session.Set<Guid>("Id", usuario.Id);
            HttpContext.Session.Set<String>("AcessToken", usuario.AcessToken);
            HttpContext.Session.Set<String>("Email", usuario.Email);

            return RedirectToAction("Index", "Produtos");
        }

        public IActionResult NovaConta()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NovaConta(Usuario model)
        {
            Usuario usuario = new Usuario();
            JObject jObject = new JObject();

            var json = JsonConvert.SerializeObject(model);

            var postRequest = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync(apiUrl + "nova-conta", postRequest).ConfigureAwait(false))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    jObject = (JObject)JsonConvert.DeserializeObject(apiResponse);

                    if (Convert.ToBoolean(jObject["sucess"]) == false)
                    {
                        ViewData["Error"] = jObject["errors"];
                        return View();
                    }

                    usuario.AcessToken = jObject["data"].Value<string>("acessToken");
                    usuario.Id = Guid.Parse(jObject["data"]["userToken"].Value<string>("id"));
                    usuario.Email = jObject["data"]["userToken"].Value<string>("email");
                }
            }

            HttpContext.Session.Set<Guid>("Id", usuario.Id);
            HttpContext.Session.Set<String>("AcessToken", usuario.AcessToken);
            HttpContext.Session.Set<String>("Email", usuario.Email);

            return RedirectToAction("Index", "Produtos");
        }
    }
}
