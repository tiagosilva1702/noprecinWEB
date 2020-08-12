using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NoPrecin.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace NoPrecin.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly string apiUrl = " https://localhost:44328/api/reservas";

        HttpClient cliente = new HttpClient();

        public UsuariosController()
        {
            cliente.BaseAddress = new Uri("http://localhost:44328/api/");

            cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<UsuariosCreate>> Index()
        {
            HttpResponseMessage response = await cliente.GetAsync("api/usuarios");
            if (response.IsSuccessStatusCode)
            {
                var dados = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<UsuariosCreate>>(dados);
            }
            return new List<UsuariosCreate>();
        }
    }
}
