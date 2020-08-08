using BomGosto.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BomGosto.Controllers
{
    public class CarrinhoController : Controller
    {
        public IActionResult Adicionar(int id)
        {        
            if(HttpContext.Session.GetInt32("tipoUsuario") is null)
                return RedirectToAction("Entrar","Home");
            ProdutoRepository produtoRepo = new ProdutoRepository();
            Produto produto = produtoRepo.Detalhes(id);
            Carrinho carrinho = new Carrinho();
            
            carrinho = JsonConvert.DeserializeObject<Carrinho>(HttpContext.Session.GetString("carrinho"));
            carrinho.produtos.Add(produto);
            
            HttpContext.Session.SetString("carrinho",JsonConvert.SerializeObject(carrinho));
            return RedirectToAction("ListaCarrinho");
        }
        public IActionResult ListaCarrinho()
        {
            Carrinho carrinho = new Carrinho();            
            carrinho = JsonConvert.DeserializeObject<Carrinho>(HttpContext.Session.GetString("carrinho"));
            return View(carrinho);
        }
        public IActionResult Retirar(int id)
        {
            Carrinho carrinho = new Carrinho();
            
            carrinho = JsonConvert.DeserializeObject<Carrinho>(HttpContext.Session.GetString("carrinho"));
            carrinho.produtos.RemoveAt(id);
            
            HttpContext.Session.SetString("carrinho",JsonConvert.SerializeObject(carrinho));
            return RedirectToAction("ListaCarrinho");
        }
    }
}