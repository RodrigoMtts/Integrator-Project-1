using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BomGosto.Models
{
    public class Usuario
    {
        public int id {set; get;}
        public string nome {set; get;}
        public string nomeUsuario{get; set;}
        public string cpf {set; get;}
        public string email {set; get;}
        public string telefone {set; get;}
        public string cep {set; get;}
        public byte tipoUsuario {get;set;}
        public string mensagem {get;set;}
                
        [DataType(DataType.Password)]
        public string senha {set; get;}
        public DateTime criacaoConta {set;get;}
        public DateTime ultimoAcesso {set; get;}     
        public List<IFormFile> imagem{set;get;}
        public string caminhoImagem{get;set;}  
    }
}