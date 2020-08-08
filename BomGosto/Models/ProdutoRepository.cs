using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace BomGosto.Models
{
    public class ProdutoRepository : Repository
    {
        public void CadastraProduto(Produto p, int id,string nomeImagem)
        {
            string sql = "INSERT INTO Produto VALUES(null,@nome,@saborPrincipal,@descricao,@preco,@imagem,NOW(),@id)";        
            conexao.Open();
            MySqlCommand command = new MySqlCommand(sql,conexao);            
            command.Parameters.AddWithValue("id",id);
            command.Parameters.AddWithValue("nome",p.nome);
            command.Parameters.AddWithValue("saborPrincipal",p.saborPrincipal);
            command.Parameters.AddWithValue("descricao",p.descricao);
            command.Parameters.AddWithValue("preco",p.preco);
            command.Parameters.AddWithValue("imagem",nomeImagem);           
            command.ExecuteNonQuery();
            conexao.Clone();

           
        }
        public void CadastraProduto(Produto p, int id)
        {
            string sql = "INSERT INTO Produto VALUES(null,@nome,@saborPrincipal,@descricao,@preco,null,NOW(),@id)";        
            conexao.Open();
            MySqlCommand command = new MySqlCommand(sql,conexao);            
            command.Parameters.AddWithValue("id",id);
            command.Parameters.AddWithValue("nome",p.nome);
            command.Parameters.AddWithValue("saborPrincipal",p.saborPrincipal);
            command.Parameters.AddWithValue("descricao",p.descricao);
            command.Parameters.AddWithValue("preco",p.preco);
            command.ExecuteNonQuery();
            conexao.Clone();

           
        }
        public List<Produto> ListaProdutos()
        {
            string sql = "SELECT Produto.idProduto,Produto.nome,Produto.saborPrincipal,Produto.descricao,Produto.preco,Produto.imagem,Produto.dataCadastro,Usuario.nome as nomeUsuario FROM Produto INNER JOIN Usuario ON Produto.idUsuario=Usuario.idUsuario";
            conexao.Open();
            MySqlCommand command = new MySqlCommand(sql,conexao);            
            MySqlDataReader reader = command.ExecuteReader();            
            List<Produto> listaProdutos = new List<Produto>();
            
            while(reader.Read()){
                Produto produto = new Produto();
                produto.id = reader.GetInt32("idProduto");
                if(!reader.IsDBNull(reader.GetOrdinal("nome")))
                    produto.nome = reader.GetString("nome");
                if(!reader.IsDBNull(reader.GetOrdinal("saborPrincipal")))
                    produto.saborPrincipal = reader.GetString("saborPrincipal");
                if(!reader.IsDBNull(reader.GetOrdinal("descricao")))
                    produto.descricao = reader.GetString("descricao");
                if(!reader.IsDBNull(reader.GetOrdinal("preco")))
                    produto.preco = reader.GetFloat("preco");
                if(!reader.IsDBNull(reader.GetOrdinal("imagem")))
                    produto.caminhoImagem = reader.GetString("imagem");
                if(!reader.IsDBNull(reader.GetOrdinal("dataCadastro")))
                    produto.dataCadastro = reader.GetDateTime("dataCadastro");
                if(!reader.IsDBNull(reader.GetOrdinal("nomeUsuario")))
                    produto.nomeUsuario = reader.GetString("nomeUsuario");
                listaProdutos.Add(produto);
            }
            reader.Close();
            conexao.Close();
            return listaProdutos;
        }
        public List<Produto> ListaProdutos(string saborPrincipal)
        {
            string sql = "SELECT * FROM Produto WHERE saborPrincipal=@saborPrincipal";
            conexao.Open();
            MySqlCommand command = new MySqlCommand(sql,conexao);
            command.Parameters.AddWithValue("saborPrincipal",saborPrincipal);
            MySqlDataReader reader = command.ExecuteReader();            
            List<Produto> listaProdutos = new List<Produto>();
            
            while(reader.Read()){
                Produto produto = new Produto();
                produto.id = reader.GetInt32("idProduto");
                if(!reader.IsDBNull(reader.GetOrdinal("nome")))
                    produto.nome = reader.GetString("nome");
                if(!reader.IsDBNull(reader.GetOrdinal("saborPrincipal")))
                    produto.saborPrincipal = reader.GetString("saborPrincipal");
                if(!reader.IsDBNull(reader.GetOrdinal("descricao")))
                    produto.descricao = reader.GetString("descricao");
                if(!reader.IsDBNull(reader.GetOrdinal("preco")))
                    produto.preco = reader.GetFloat("preco");
                if(!reader.IsDBNull(reader.GetOrdinal("imagem")))
                    produto.caminhoImagem = reader.GetString("imagem");
                if(!reader.IsDBNull(reader.GetOrdinal("dataCadastro")))
                    produto.dataCadastro = reader.GetDateTime("dataCadastro");
                listaProdutos.Add(produto);
            }
            reader.Close();
            conexao.Close();
            return listaProdutos;
        }

        public List<Produto> ListaSaborPrincipal()
        {
            string sql = "SELECT Distinct saborPrincipal FROM Produto";
            conexao.Open();
            MySqlCommand command = new MySqlCommand(sql,conexao);
            MySqlDataReader reader = command.ExecuteReader();            
            List<Produto> listaProdutos = new List<Produto>();
            
            while(reader.Read()){
                Produto produto = new Produto();                
                if(!reader.IsDBNull(reader.GetOrdinal("saborPrincipal")))
                    produto.saborPrincipal = reader.GetString("saborPrincipal");
                listaProdutos.Add(produto);
            }
            reader.Close();
            conexao.Close();
            return listaProdutos;
        }
        public void ExcluirProduto(int id)
        {
            string sql = "DELETE FROM Produto WHERE idProduto=@id";        
            conexao.Open();
            MySqlCommand command = new MySqlCommand(sql,conexao);            
            command.Parameters.AddWithValue("id",id);            
            command.ExecuteNonQuery();
            conexao.Clone();
        }

        public void AlteraProduto(Produto p,string nomeImagem)
        {       
            string sql = "UPDATE Produto SET nome=@nome,saborPrincipal=@saborPrincipal,descricao=@descricao,preco=@preco,imagem=@imagem WHERE idProduto=@id";        
            conexao.Open();
            MySqlCommand command = new MySqlCommand(sql,conexao);            
            command.Parameters.AddWithValue("id",p.id);
            command.Parameters.AddWithValue("nome",p.nome);
            command.Parameters.AddWithValue("saborPrincipal",p.saborPrincipal);
            command.Parameters.AddWithValue("descricao",p.descricao);
            command.Parameters.AddWithValue("preco",p.preco);
            command.Parameters.AddWithValue("imagem",nomeImagem); 
            command.ExecuteNonQuery();
            conexao.Clone();        
        }
        public void AlteraProduto(Produto p)
        {       
            string sql = "UPDATE Produto SET nome=@nome,saborPrincipal=@saborPrincipal,descricao=@descricao,preco=@preco WHERE idProduto=@id";        
            conexao.Open();
            MySqlCommand command = new MySqlCommand(sql,conexao);            
            command.Parameters.AddWithValue("id",p.id);
            command.Parameters.AddWithValue("nome",p.nome);
            command.Parameters.AddWithValue("saborPrincipal",p.saborPrincipal);
            command.Parameters.AddWithValue("descricao",p.descricao);
            command.Parameters.AddWithValue("preco",p.preco);
            command.ExecuteNonQuery();
            conexao.Clone();        
        }

        public Produto Detalhes(int id)
        {
            string sql = "SELECT * FROM Produto WHERE idProduto = @id";
            conexao.Open();
            MySqlCommand command = new MySqlCommand(sql,conexao);
            command.Parameters.AddWithValue("id",id);
            MySqlDataReader reader = command.ExecuteReader();
            Produto produto = new Produto();

            reader.Read();
            produto.id = reader.GetInt32("idProduto");
            if(!reader.IsDBNull(reader.GetOrdinal("nome")))
                produto.nome = reader.GetString("nome");
            if(!reader.IsDBNull(reader.GetOrdinal("saborPrincipal")))
                produto.saborPrincipal = reader.GetString("saborPrincipal");
            if(!reader.IsDBNull(reader.GetOrdinal("descricao")))
                produto.descricao = reader.GetString("descricao");
            if(!reader.IsDBNull(reader.GetOrdinal("preco")))
                produto.preco = reader.GetFloat("preco");
            if(!reader.IsDBNull(reader.GetOrdinal("dataCadastro")))
                produto.dataCadastro = reader.GetDateTime("dataCadastro");            
            reader.Close();
            conexao.Close();
            return produto;
        }
    }
}