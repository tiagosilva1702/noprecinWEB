using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NoPrecin.Context;
using NoPrecin.Models;

namespace NoPrecin.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly string apiUrl = "https://localhost:44328/api/produtos/";
        private readonly string apiUrlv = "https://localhost:44328/api/vendas/";

        //Define uma instância de IHostingEnvironment
        IWebHostEnvironment _appEnvironment;
        private readonly AppDbContext _context;
        //Injeta a instância no construtor para poder usar os recursos
        public ProdutosController(IWebHostEnvironment env, AppDbContext context)
        {
            _appEnvironment = env;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Produtos> listaProdutos = new List<Produtos>();

            Usuario usuario = new Usuario();
            usuario.Id = HttpContext.Session.Get<Guid>("Id");
            usuario.AcessToken = HttpContext.Session.Get<String>("AcessToken");
            usuario.Email = HttpContext.Session.Get<String>("Email");

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(apiUrl))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    listaProdutos = JsonConvert.DeserializeObject<List<Produtos>>(apiResponse);
                }
            }

            return View(listaProdutos.Where(x => x.Ativo == true));
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Criar(Produtos produto, IFormFile arquivo)
        {
            Guid guid = Guid.NewGuid();
            Guid idImagem = Guid.NewGuid();

            Usuario usuario = new Usuario();
            usuario.Id = HttpContext.Session.Get<Guid>("Id");
            usuario.AcessToken = HttpContext.Session.Get<String>("AcessToken");
            usuario.Email = HttpContext.Session.Get<String>("Email");

            // Define um nome para o arquivo enviado incluindo o sufixo obtido de milesegundos
            string nomeArquivo = guid.ToString();

            if (arquivo != null)
            {
                //verifica qual o tipo de arquivo : jpg, gif, png, pdf ou tmp
                if (arquivo.FileName.Contains(".jpg"))
                    nomeArquivo += ".jpg";
                else if (arquivo.FileName.Contains(".gif"))
                    nomeArquivo += ".gif";
                else if (arquivo.FileName.Contains(".png"))
                    nomeArquivo += ".png";
                else if (arquivo.FileName.Contains(".pdf"))
                    nomeArquivo += ".pdf";
                else
                    nomeArquivo += ".tmp";

                //< obtém o caminho físico da pasta wwwroot >
                string caminho_WebRoot = _appEnvironment.WebRootPath;
                // monta o caminho onde vamos salvar o arquivo :  ~\wwwroot\Arquivos\Arquivos_Usuario\Recebidos
                string caminhoDestinoArquivo = caminho_WebRoot + "\\img\\";

                // incluir a pasta Recebidos e o nome do arquiov enviado : ~\wwwroot\Arquivos\Arquivos_Usuario\Recebidos\
                string caminhoDestinoArquivoOriginal = caminhoDestinoArquivo + nomeArquivo;

                //copia o arquivo para o local de destino original
                using (var stream = new FileStream(caminhoDestinoArquivoOriginal, FileMode.Create))
                {
                    await arquivo.CopyToAsync(stream);
                }
            }

            produto.Id = idImagem;
            produto.EmailProprietario = usuario.Email;
            produto.DataCadastro = DateTime.Now;
            produto.Imagem = nomeArquivo;

            //TODO: Acesso direto ao banco de dados
            //_context.Add(produto);
            //await _context.SaveChangesAsync();

            //TODO: Acesso API
            var json = JsonConvert.SerializeObject(produto);
            var postRequest = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + usuario.AcessToken);

                using (var response = await httpClient.PostAsync(apiUrl, postRequest).ConfigureAwait(false))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    produto = JsonConvert.DeserializeObject<Produtos>(apiResponse);
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Editar(Guid id)
        {
            Produtos produtos = new Produtos();
            Usuario usuario = new Usuario();
            usuario.Id = HttpContext.Session.Get<Guid>("Id");
            usuario.AcessToken = HttpContext.Session.Get<String>("AcessToken");
            usuario.Email = HttpContext.Session.Get<String>("Email");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + usuario.AcessToken);

                using (var response = await httpClient.GetAsync(apiUrl + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    produtos = JsonConvert.DeserializeObject<Produtos>(apiResponse);
                }
            }
            return View(produtos);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, Produtos produto)
        {
            Usuario usuario = new Usuario();
            usuario.AcessToken = HttpContext.Session.Get<String>("AcessToken");
            produto.EmailProprietario = HttpContext.Session.Get<String>("Email");

            //TODO: Acesso banco de dados
            //_context.Update(produto);
            //await _context.SaveChangesAsync();

            //TODO: Acesso API
            var json = JsonConvert.SerializeObject(produto);
            var postRequest = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + usuario.AcessToken);

                using (var response = await httpClient.PutAsync(apiUrl + produto.Id, postRequest))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("ListarPorUsuario");
        }

        public async Task<IActionResult> Deletar(Guid id)
        {
            Usuario usuario = new Usuario();
            usuario.AcessToken = HttpContext.Session.Get<String>("AcessToken");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + usuario.AcessToken);

                await httpClient.DeleteAsync(apiUrl + id);
            }

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> ListarPorUsuario()
        {
            List<Produtos> listaProdutos = new List<Produtos>();

            Usuario usuario = new Usuario();
            usuario.Id = HttpContext.Session.Get<Guid>("Id");
            usuario.AcessToken = HttpContext.Session.Get<String>("AcessToken");
            usuario.Email = HttpContext.Session.Get<String>("Email");
            //Retorna para a viu todos os produtos
            if (usuario.AcessToken == null)
            {
                return RedirectToAction("Login", "Usuarios");
            }

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + usuario.AcessToken);

                using (var response = await httpClient.GetAsync(apiUrl + "por-usuario/" + usuario.Id))
                {

                    string apiResponse = await response.Content.ReadAsStringAsync();
                    listaProdutos = JsonConvert.DeserializeObject<List<Produtos>>(apiResponse);
                }
            }

            //Se o usuário estiver logado retorna lista com seus produtos
            return View(listaProdutos);
        }

        public async Task<IActionResult> MinhasCompras()
        {
            List<Vendas> listarVendas = new List<Vendas>();

            Usuario usuario = new Usuario();
            usuario.Id = HttpContext.Session.Get<Guid>("Id");
            usuario.AcessToken = HttpContext.Session.Get<String>("AcessToken");
            usuario.Email = HttpContext.Session.Get<String>("Email");
            //Retorna para a viu todos os produtos
            if (usuario.AcessToken == null)
            {
                return RedirectToAction("Login", "Usuarios");
            }

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + usuario.AcessToken);

                using (var response = await httpClient.GetAsync(apiUrlv))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    listarVendas = JsonConvert.DeserializeObject<List<Vendas>>(apiResponse);
                }
            }
            //Se o usuário estiver logado retorna lista com seus produtos
            return View(listarVendas);
        }
    }
}
