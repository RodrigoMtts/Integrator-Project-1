using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BomGosto.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BomGosto.Controllers
{
    public class UsuarioController : Controller
    {
        IHostingEnvironment _appEnvironment;
        //Injeta a instância no construtor para poder usar os recursos
        public UsuarioController(IHostingEnvironment env)
        {
            _appEnvironment = env;
        }
        [HttpPost]
        public IActionResult Entrar(string nomeUsuario, string senha)
        {
            UsuarioRepository usuarioRepo = new UsuarioRepository();
            Usuario usuario = usuarioRepo.Entrar(nomeUsuario,senha);
            if(!String.IsNullOrEmpty(usuario.nome)){
                HttpContext.Session.SetInt32("id",usuario.id);
                HttpContext.Session.SetInt32("tipoUsuario",usuario.tipoUsuario);
            return View("Detalhes",usuario);
            }
            return RedirectToAction("Entrar","Home");
        }
        public IActionResult Entrar(Usuario u)
        {
            UsuarioRepository usuarioRepo = new UsuarioRepository();
            Usuario usuario = usuarioRepo.Entrar(u.nomeUsuario,u.senha);
            HttpContext.Session.SetInt32("id",usuario.id);
            HttpContext.Session.SetInt32("tipoUsuario",usuario.tipoUsuario);
            return View("Detalhes",usuario);
        }
        [Route("Usuario/Detalhes")]
        public IActionResult Detalhes()
        {   
            int id = (int)HttpContext.Session.GetInt32("id");
            UsuarioRepository usuarioRepo = new UsuarioRepository();
            Usuario usuario = usuarioRepo.Detalhes(id);
            return View(usuario);
        }
        [Route("Usuario/Detalhes/{id}")]
        public IActionResult Detalhes(int id)
        {          
            UsuarioRepository usuarioRepo = new UsuarioRepository();
            Usuario usuario = usuarioRepo.Detalhes(id);
            return View(usuario);
        }
        [HttpPost]
        public IActionResult Detalhes(Usuario u)
        {          
            UsuarioRepository usuarioRepo = new UsuarioRepository();
            Usuario usuario = usuarioRepo.Detalhes(u);
            return View(usuario);
        }
        public IActionResult Sair()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }
        public IActionResult AlterarSenha()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AlterarSenha(string senha)
        {
            int id = (int)HttpContext.Session.GetInt32("id");
            UsuarioRepository usuarioRepo = new UsuarioRepository();
            usuarioRepo.AlterarSenha(id,senha);
            
            return RedirectToAction("Detalhes");
        }
        public IActionResult AlterarDados(int id)
        {
            UsuarioRepository usuarioRepo = new UsuarioRepository();
            Usuario usuario = usuarioRepo.Detalhes(id);
            return View(usuario);
        }
        [HttpPost]
        public async Task<IActionResult> AlterarDados(Usuario u)
        {
            if(u.imagem is null){
                UsuarioRepository usuarioRepo = new UsuarioRepository();                
                usuarioRepo.AlterarDados(u);

                return RedirectToAction("Detalhes","Usuario",new {@id=u.id});
            }
            long tamanhoArquivos = u.imagem.Sum(f => f.Length);
            // caminho completo do arquivo na localização temporária
            var caminhoArquivo = Path.GetTempFileName();

            // processa os arquivo enviados
            //percorre a lista de arquivos selecionados
            foreach (var arquivo in u.imagem)
            {
                //verifica se existem arquivos 
                if (arquivo == null || arquivo.Length == 0)
                {
                    //retorna a viewdata com erro
                    ViewData["Erro"] = "Error: Arquivo(s) não selecionado(s)";
                    return Content("eerooo");
                }

                // < define a pasta onde vamos salvar os arquivos >
               // string pasta = "Arquivos_Usuario";
                // Define um nome para o arquivo enviado incluindo o sufixo obtido de milesegundos
                string nomeArquivo = "Usuario_arquivo_" + DateTime.Now.Millisecond.ToString();

                //verifica qual o tipo de arquivo : jpg, gif, png, pdf ou tmp
                if (arquivo.FileName.Contains(".jpg"))
                    nomeArquivo += ".jpg";
                else if (arquivo.FileName.Contains(".gif"))
                    nomeArquivo += ".gif";
                else if (arquivo.FileName.Contains(".png"))
                    nomeArquivo += ".png";
                else if (arquivo.FileName.Contains(".pdf"))
                    nomeArquivo += ".pdf";
                else if (arquivo.FileName.Contains(".jpeg"))
                    nomeArquivo += ".jpeg";
                else
                    nomeArquivo += ".tmp";

                //< obtém o caminho físico da pasta wwwroot >
                string caminho_WebRoot = _appEnvironment.WebRootPath;
                // monta o caminho onde vamos salvar o arquivo : 
                // ~\wwwroot\Arquivos\Arquivos_Usuario\Recebidos
                string caminhoDestinoArquivo = caminho_WebRoot + "/img";
                // incluir a pasta Recebidos e o nome do arquivo enviado : 
                // ~\wwwroot\Arquivos\Arquivos_Usuario\Recebidos\
                string caminhoDestinoArquivoOriginal = caminhoDestinoArquivo + "/usuarios/" +nomeArquivo;

                //copia o arquivo para o local de destino original
                using (var stream = new FileStream(caminhoDestinoArquivoOriginal, FileMode.Create))
                {
                    await arquivo.CopyToAsync(stream);
                }
                
                UsuarioRepository usuarioRepo = new UsuarioRepository();
                usuarioRepo.AlterarDados(u,nomeArquivo);
                
            } 
            return RedirectToAction("Detalhes",new {id=u.id});      
        }
        public IActionResult ExcluirConta(int id)
        {
            UsuarioRepository usuarioRepo = new UsuarioRepository();
            usuarioRepo.ExcluirConta(id);
            if(HttpContext.Session.GetInt32("id") == id)
                return RedirectToAction("Sair","Usuario");
            return RedirectToAction("ListaUsuarios");
        }
        [HttpGet]
        public IActionResult ExcluirImagem(int id)
        {            
            UsuarioRepository usuarioRepo = new UsuarioRepository();
            usuarioRepo.ExcluirImagem(id);
            return RedirectToAction("Detalhes",new {id});

        }
        public IActionResult ListaUsuarios()
        {            
            UsuarioRepository usuarioRepo = new UsuarioRepository();
            List<Usuario> listaUsuarios = usuarioRepo.ListaUsuarios();
            return View(listaUsuarios);
        }
        public IActionResult ListaColaborador()
        {            
            UsuarioRepository usuarioRepo = new UsuarioRepository();
            List<Usuario> listaColaboradores = usuarioRepo.ListaColaborador();
            return View("ListaUsuarios",listaColaboradores);
        }
        public IActionResult ExcluirUsuario(int id)
        {            
            UsuarioRepository usuarioRepo = new UsuarioRepository();
            usuarioRepo.ExcluirConta(id);
            return RedirectToAction("ListaUsuarios");
        }
        public IActionResult CadastraColaborador()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Cadastrarse(Usuario u)
        {
            if(u.imagem is null){
                Carrinho carrinho = new Carrinho();
                HttpContext.Session.SetString("carrinho",JsonConvert.SerializeObject(carrinho));
                UsuarioRepository usuarioRepo = new UsuarioRepository();                
                usuarioRepo.Cadastrarse(u);
                return RedirectToAction("Entrar","Usuario",u);
            }
            long tamanhoArquivos = u.imagem.Sum(f => f.Length);
            // caminho completo do arquivo na localização temporária
            var caminhoArquivo = Path.GetTempFileName();

            // processa os arquivo enviados
            //percorre a lista de arquivos selecionados
            foreach (var arquivo in u.imagem)
            {
                //verifica se existem arquivos 
                if (arquivo == null || arquivo.Length == 0)
                {
                    //retorna a viewdata com erro
                    ViewData["Erro"] = "Error: Arquivo(s) não selecionado(s)";
                    return Content("eerooo");
                }

                // < define a pasta onde vamos salvar os arquivos >
               // string pasta = "Arquivos_Usuario";
                // Define um nome para o arquivo enviado incluindo o sufixo obtido de milesegundos
                string nomeArquivo = "Usuario_arquivo_" + DateTime.Now.Millisecond.ToString();

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
                // monta o caminho onde vamos salvar o arquivo : 
                // ~\wwwroot\Arquivos\Arquivos_Usuario\Recebidos
                string caminhoDestinoArquivo = caminho_WebRoot + "/img";
                // incluir a pasta Recebidos e o nome do arquivo enviado : 
                // ~\wwwroot\Arquivos\Arquivos_Usuario\Recebidos\
                string caminhoDestinoArquivoOriginal = caminhoDestinoArquivo + "/usuarios/" +nomeArquivo;

                //copia o arquivo para o local de destino original
                using (var stream = new FileStream(caminhoDestinoArquivoOriginal, FileMode.Create))
                {
                    await arquivo.CopyToAsync(stream);
                }
                Carrinho carrinho = new Carrinho();
                HttpContext.Session.SetString("carrinho",JsonConvert.SerializeObject(carrinho));
                UsuarioRepository usuarioRepo = new UsuarioRepository();
                usuarioRepo.Cadastrarse(u,nomeArquivo);
                
            } 
            return RedirectToAction("Entrar","Usuario",u);
        }
        [HttpPost]
         public async Task<IActionResult> CadastraColaborador(Usuario u)
        {
            if(u.imagem is null){
                UsuarioRepository usuarioRepo = new UsuarioRepository();                
                usuarioRepo.Cadastrarse(u);
                return RedirectToAction("ListaUsuarios","Usuario");
            }
            long tamanhoArquivos = u.imagem.Sum(f => f.Length);
            // caminho completo do arquivo na localização temporária
            var caminhoArquivo = Path.GetTempFileName();

            // processa os arquivo enviados
            //percorre a lista de arquivos selecionados
            foreach (var arquivo in u.imagem)
            {
                //verifica se existem arquivos 
                if (arquivo == null || arquivo.Length == 0)
                {
                    //retorna a viewdata com erro
                    ViewData["Erro"] = "Error: Arquivo(s) não selecionado(s)";
                    return Content("eerooo");
                }

                // < define a pasta onde vamos salvar os arquivos >
               // string pasta = "Arquivos_Usuario";
                // Define um nome para o arquivo enviado incluindo o sufixo obtido de milesegundos
                string nomeArquivo = "Usuario_arquivo_" + DateTime.Now.Millisecond.ToString();

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
                // monta o caminho onde vamos salvar o arquivo : 
                // ~\wwwroot\Arquivos\Arquivos_Usuario\Recebidos
                string caminhoDestinoArquivo = caminho_WebRoot + "/img";
                // incluir a pasta Recebidos e o nome do arquivo enviado : 
                // ~\wwwroot\Arquivos\Arquivos_Usuario\Recebidos\
                string caminhoDestinoArquivoOriginal = caminhoDestinoArquivo + "/usuarios/" +nomeArquivo;

                //copia o arquivo para o local de destino original
                using (var stream = new FileStream(caminhoDestinoArquivoOriginal, FileMode.Create))
                {
                    await arquivo.CopyToAsync(stream);
                }
                UsuarioRepository usuarioRepo = new UsuarioRepository();
                usuarioRepo.Cadastrarse(u,nomeArquivo);
                
            } 
            return RedirectToAction("Lista","Usuario");
        }
    }
}