using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebBiblioteca.Models;

namespace WebBiblioteca.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        static HttpClient httpClient = new HttpClient();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var livros = await Livros();
            return View(livros);
        }

        public IActionResult Administracao()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Acesso(UsuarioView usuarioView)
        {
            if (ModelState.IsValid)
            {
                string urlUsuario = "http://localhost:9323/v1/Usuario/GetUsuario/" + usuarioView.Login;
                var response = await httpClient.GetStringAsync(urlUsuario);
                var usuario = JsonConvert.DeserializeObject<UsuarioView>(response);
                if (usuario != null)
                {
                    if (usuario != null && usuario.IdTipo == 1)
                    {
                        var livros = await Livros();
                        return View(livros);
                    }
                    else
                    {
                        ViewBag.Mensagem = "Acesso somente para Administradores.";
                        return View("Mensagem");
                    }
                }
                else
                {
                    ViewBag.Usuario = "Usuário não localizado.";
                    return View("Administracao");
                }
            }
            else
            {
                return View("Administracao");
            }
        }

        public async Task<IActionResult> Cadastro(LivroView Livro)
        {
            var editoras = await this.Editoras();
            ViewBag.Editoras = editoras;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Incluir(LivroView Livro)
        {
            if (ModelState.IsValid)
            {
                string urlAdicionarLivro = "http://localhost:9323/v1/Livro/Adicionar";
                var myContent = JsonConvert.SerializeObject(Livro);
                var response = await httpClient.PostAsync(urlAdicionarLivro, new StringContent(myContent, Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var livros = await Livros();
                    return View("Acesso", livros);
                }

                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
                
        public async Task<IActionResult> Delete(int id)
        {
            
            string urlExcluir = "http://localhost:9323/v1/Livro/Excluir/" + id;
            var response = await httpClient.DeleteAsync(urlExcluir);

            if (response.IsSuccessStatusCode)
            {
                var livros = await Livros();
                return View("Acesso",livros);
            }
            else
            {
                ViewBag.Mensagem = "Não foi possível a exclusão do registro.";
                return View("Mensagem");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<List<LivroView>> Livros()
        {
            string urlLivros = "http://localhost:9323/v1/Livro/Listar";
            var response = await httpClient.GetStringAsync(urlLivros);
            var livros = JsonConvert.DeserializeObject<List<LivroView>>(response);
            return livros;
        }
        private async Task<List<EditoraView>> Editoras()
        {
            string urlEditoras = "http://localhost:9323/v1/Editora/Listar";
            var response = await httpClient.GetStringAsync(urlEditoras);
            var editoras = JsonConvert.DeserializeObject<List<EditoraView>>(response);
            return editoras;
        }
    }
}
