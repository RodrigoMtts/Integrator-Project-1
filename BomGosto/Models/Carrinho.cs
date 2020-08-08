using System.Collections.Generic;

namespace BomGosto.Models
{
    public class Carrinho
    {
        public List<Produto> produtos{get;set;}

        public Carrinho()
        {
            produtos = new List<Produto>();
        }
        public float ValorTotal()
        {
            float valorTotal = 0;
            foreach(var item in produtos)
            {
                valorTotal += item.preco; 
            }
            return valorTotal;
        }
    }   
}