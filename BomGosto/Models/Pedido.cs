using System;
using System.Collections.Generic;

namespace BomGosto.Models
{
    public class Pedido
    {
        public int idPedido{set;get;}
        public int idUsuario{set;get;}
        public string nomeUsuario{get;set;}
        public string cpfUsuario{get;set;}
        public DateTime dataPedido{set;get;}
        public int quantidadeProduto{set;get;}
        public float preco{set;get;}
        public List<Produto> produtos{get;set;}
    }
}