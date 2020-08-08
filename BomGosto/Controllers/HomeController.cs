using System;
using System.Collections.Generic;
using BomGosto.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BomGosto.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {   
            ProdutoRepository produtoRepoView = new ProdutoRepository();
            List<Produto> produtosView = produtoRepoView.ListaSaborPrincipal();
            ViewBag.produtos = produtosView;

            ProdutoRepository produtoRepo = new ProdutoRepository();
            List<Produto> produtos = produtoRepo.ListaProdutos();

            return View(produtos);
        }
        [Route("/Home/Index/{saborPrincipal}")]
        public IActionResult Index(String saborPrincipal)
        {   
            ProdutoRepository produtoRepoView = new ProdutoRepository();
            List<Produto> produtosView = produtoRepoView.ListaSaborPrincipal();
            ViewBag.produtos = produtosView;


            ProdutoRepository produtoRepo = new ProdutoRepository();            
            List<Produto> listaProdutos = produtoRepo.ListaProdutos(saborPrincipal);
            return View(listaProdutos);
        }
        public IActionResult Contato()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contato(Usuario u){
            UsuarioRepository usuarioRepo = new UsuarioRepository();
            usuarioRepo.FazContato(u);
            return RedirectToAction("Index");
        }
        public IActionResult SobreNos()
        {
            return View();
        }
        public IActionResult Cadastrarse()
        {
            return View();
        }
        
        public IActionResult Entrar()
        {
            Carrinho carrinho = new Carrinho();
            HttpContext.Session.SetString("carrinho",JsonConvert.SerializeObject(carrinho));
            if(HttpContext.Session.GetInt32("tipoUsuario") is null)
                return View();
            return RedirectToAction("Detalhes","Usuario");
        }

        public IActionResult ListaContato()
        {   
            UsuarioRepository usuarioRepo = new UsuarioRepository();
            List<Usuario> listaContato = usuarioRepo.ListaContato();
            return View(listaContato);
        }
        public IActionResult ExcluirContato(int id)
        {   
            UsuarioRepository usuarioRepo = new UsuarioRepository();
            usuarioRepo.ExcluirContato(id);
            return RedirectToAction("listaContato");
        }
    }
}