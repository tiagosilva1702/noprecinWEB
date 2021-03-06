﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
using System.Security.Claims;
using System.Security.Principal;
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
            UsuarioLogado usuario = new UsuarioLogado();

            var json = JsonConvert.SerializeObject(model);

            var postRequest = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync(apiUrl + "entrar", postRequest).ConfigureAwait(false))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    usuario = JsonConvert.DeserializeObject<UsuarioLogado>(apiResponse);

                    if (usuario.sucess == false)
                    {
                        ViewData["Error"] = usuario.errors[0].ToString();
                        return View();
                    }
                }
            }

            this.Autenticar();

            HttpContext.Session.Set<Guid>("Id", usuario.data.userToken.id);
            HttpContext.Session.Set<String>("AcessToken", usuario.data.acessToken);
            HttpContext.Session.Set<String>("Email", usuario.data.userToken.email);

            return RedirectToAction("ListarPorUsuario", "Produtos");
        }

        public IActionResult NovaConta()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NovaConta(Usuario model)
        {
            UsuarioLogado usuario = new UsuarioLogado();

            var json = JsonConvert.SerializeObject(model);

            var postRequest = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync(apiUrl + "nova-conta", postRequest).ConfigureAwait(false))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    usuario = JsonConvert.DeserializeObject<UsuarioLogado>(apiResponse);

                    if (usuario.sucess == false)
                    {
                        ViewData["Error"] = usuario.errors[0].ToString();
                        return View();
                    }
                }
            }

            this.Autenticar();

            HttpContext.Session.Set<Guid>("Id", usuario.data.userToken.id);
            HttpContext.Session.Set<String>("AcessToken", usuario.data.acessToken);
            HttpContext.Session.Set<String>("Email", usuario.data.userToken.email);

            return RedirectToAction("ListarPorUsuario", "Produtos");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }

        private async void Autenticar()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, "Usuario")
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {

                //AllowRefresh = true,
                // Refreshing the authentication session should be allowed.

                //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                //IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                //IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.

                //RedirectUri = <string>
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
    }
}
