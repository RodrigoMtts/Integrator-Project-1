using System.Collections.Generic;
using BomGosto.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BomGosto.Controllers
{
    public class PedidoController : Controller
    {
        public IActionResult FinalizarCompra()
        {
            Carrinho carrinho = new Carrinho();            
            carrinho = JsonConvert.DeserializeObject<Carrinho>(HttpContext.Session.GetString("carrinho"));
            PedidoRepository pedidoRepo = new PedidoRepository();
            int id = (int)HttpContext.Session.GetInt32("id");
            pedidoRepo.FinalizarCompra(carrinho,id);
            carrinho = new Carrinho();
            HttpContext.Session.SetString("carrinho",JsonConvert.SerializeObject(carrinho));
            return RedirectToAction("ListaPedidos");
        }
        public IActionResult ListaPedidos()       
        {
            int id = (int)HttpContext.Session.GetInt32("id");
            int tipo = (int)HttpContext.Session.GetInt32("tipoUsuario");
            PedidoRepository pedidoRepo = new PedidoRepository();
            List<Pedido> listaPedidos;
            if(tipo == 2){
               listaPedidos = pedidoRepo.ListaPedidos(id);
               return View(listaPedidos);
            }else{
               listaPedidos = pedidoRepo.ListaPedidos();
               return View("ListaPedidosAdmin",listaPedidos);
            }            
        }

        [Route("/Pedido/Detalhes/{idPedido}")]
        public IActionResult Detalhes(int idPedido)
        {
            PedidoRepository pedidoRepo = new PedidoRepository();
            List<Produto> detalhesPedido;
            detalhesPedido = pedidoRepo.Detalhes(idPedido);
        
            return View(detalhesPedido);
        }
        public IActionResult ExcluirPedido(int id)
        {
            PedidoRepository pedidoRepo = new PedidoRepository();
            pedidoRepo.ExcluirPedido(id);            
            return RedirectToAction("ListaPedidos");
        }
    }
}