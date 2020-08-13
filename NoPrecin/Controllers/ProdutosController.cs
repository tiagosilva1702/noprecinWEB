using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NoPrecin.Models;

namespace NoPrecin.Controllers
{
    public class ProdutosController : Controller
    {
        //Define uma instância de IHostingEnvironment
        IHostingEnvironment _appEnvironment;
        //Injeta a instância no construtor para poder usar os recursos
        public ProdutosController(IHostingEnvironment env)
        {
            _appEnvironment = env;
        }

        private readonly string apiUrl = "https://localhost:44328/api/produtos";

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

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Criar(Produtos produto, IFormFile arquivo)
        {
            Guid guid = Guid.NewGuid();

            // Define um nome para o arquivo enviado incluindo o sufixo obtido de milesegundos
            string nomeArquivo = guid.ToString();
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

            produto.Imagem = nomeArquivo;
            var json = JsonConvert.SerializeObject(produto);
            var postRequest = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync(apiUrl + "novo-produto", postRequest).ConfigureAwait(false))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    produto = JsonConvert.DeserializeObject<Produtos>(apiResponse);
                }
            }
            return View(produto);
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
