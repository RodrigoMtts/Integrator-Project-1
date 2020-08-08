using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace BomGosto.Models
{
    public class Produto
    {
        public int id{get;set;}
        public string nome{set;get;}
        public string saborPrincipal{set;get;}
        public string descricao{set;get;}
        public float preco{set;get;}
        public List<IFormFile> imagem{set;get;}
        public string caminhoImagem{get;set;}
        public DateTime dataCadastro{get;set;}
        public string nomeUsuario{get;set;}
    }
}