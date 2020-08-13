using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
            UsuarioAutenticado usuario = new UsuarioAutenticado();

            var json = JsonConvert.SerializeObject(model);

            var postRequest = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync(apiUrl + "entrar", postRequest).ConfigureAwait(false))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    usuario = JsonConvert.DeserializeObject<UsuarioAutenticado>(apiResponse);
                }
            }
            return View(usuario);
        }

        public IActionResult NovaConta()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NovaConta(Usuario model)
        {
            Usuario usuario = new Usuario();

            var json = JsonConvert.SerializeObject(model);

            var postRequest = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync(apiUrl + "nova-conta", postRequest).ConfigureAwait(false))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    usuario = JsonConvert.DeserializeObject<Usuario>(apiResponse);
                }
            }
            return View(usuario);
        }
    }
}
