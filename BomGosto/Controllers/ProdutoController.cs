using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BomGosto.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BomGosto.Controllers
{
    public class ProdutoController : Controller
    {
        IHostingEnvironment _appEnvironment;
        //Injeta a instância no construtor para poder usar os recursos
        public ProdutoController(IHostingEnvironment env)
        {
            _appEnvironment = env;
        }

        //Retorna a View Index.cshtml que será o formulário para
        //selecionar os arquivos a serem enviados 
        public IActionResult Index()
        {
            return View();
        }

        //método para enviar os arquivos usando a interface IFormFile
        public IActionResult CadastraProduto()
        {
            return View();
        }       

        [HttpPost]
        public async Task<IActionResult> CadastraProduto(Produto p)
        {
            int id = (int)HttpContext.Session.GetInt32("id");
            if(p.imagem is null){
                ProdutoRepository produtoRepo = new ProdutoRepository();                
                produtoRepo.CadastraProduto(p,id);
                return RedirectToAction("ListaProdutos","Produto");
            }
            long tamanhoArquivos = p.imagem.Sum(f => f.Length);
            // caminho completo do arquivo na localização temporária
            var caminhoArquivo = Path.GetTempFileName();

            // processa os arquivo enviados
            //percorre a lista de arquivos selecionados
            foreach (var arquivo in p.imagem)
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
                string caminhoDestinoArquivoOriginal = caminhoDestinoArquivo + "/produtos/" +nomeArquivo;

                //copia o arquivo para o local de destino original
                using (var stream = new FileStream(caminhoDestinoArquivoOriginal, FileMode.Create))
                {
                    await arquivo.CopyToAsync(stream);
                }
                ProdutoRepository produtoRepo = new ProdutoRepository();
                produtoRepo.CadastraProduto(p,id,nomeArquivo);
            }

            //monta a ViewData que será exibida na view como resultado do envio 
            ViewData["Resultado"] = $"{p.imagem.Count} arquivos foram enviados ao servidor, " +
             $"com tamanho total de : {tamanhoArquivos} bytes";

            //retorna a viewdata
            return RedirectToAction("ListaProdutos","Produto");
        }

        public IActionResult ListaProdutos()
        {
            ProdutoRepository produtoRepo = new ProdutoRepository();            
            List<Produto> listaProdutos = produtoRepo.ListaProdutos();
            return View(listaProdutos);
        }
        [Route("/Produto/ListaProdutos/{saborPrincipa}")]
        public IActionResult ListaProdutos(string saborPrincipal)
        {
            ProdutoRepository produtoRepo = new ProdutoRepository();            
            List<Produto> listaProdutos = produtoRepo.ListaProdutos(saborPrincipal);

            return RedirectToAction("Index","Home",listaProdutos);
        }

        public IActionResult ExcluirProduto(int id)
        {
            ProdutoRepository usuarioRepo = new ProdutoRepository();
            usuarioRepo.ExcluirProduto(id);            
            return RedirectToAction("ListaProdutos");
        }
         public IActionResult AlteraProduto(int id)
        {
            ProdutoRepository produtoRepo = new ProdutoRepository();
            Produto produto = produtoRepo.Detalhes(id);
            return View(produto);
        }
        [HttpPost]
        public async Task<IActionResult> AlteraProduto(Produto p)
        {
            if(p.imagem is null){
                ProdutoRepository produtoRepo = new ProdutoRepository();                
                produtoRepo.AlteraProduto(p);
                return RedirectToAction("ListaProdutos","Produto");
            }

            long tamanhoArquivos = p.imagem.Sum(f => f.Length);
            // caminho completo do arquivo na localização temporária
            var caminhoArquivo = Path.GetTempFileName();

            // processa os arquivo enviados
            //percorre a lista de arquivos selecionados
            foreach (var arquivo in p.imagem)
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
                string caminhoDestinoArquivoOriginal = caminhoDestinoArquivo + "/produtos/" +nomeArquivo;

                //copia o arquivo para o local de destino original
                using (var stream = new FileStream(caminhoDestinoArquivoOriginal, FileMode.Create))
                {
                    await arquivo.CopyToAsync(stream);
                }
                ProdutoRepository usuarioRepo = new ProdutoRepository();
                usuarioRepo.AlteraProduto(p,nomeArquivo);
            }                      
           return RedirectToAction("ListaProdutos");               
        }
    }
}