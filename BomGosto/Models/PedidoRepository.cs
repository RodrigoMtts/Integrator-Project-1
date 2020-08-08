using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace BomGosto.Models
{
    public class PedidoRepository : Repository
    {
        public void FinalizarCompra(Carrinho carrinho,int id)
        {
            int idPedido;
            int i=0;
            {
                string sql = "INSERT INTO Pedido VALUES(null,@idUsuario,NOW(),@quantidade,@preco)";        
                conexao.Open();
                MySqlCommand command = new MySqlCommand(sql,conexao);            
                command.Parameters.AddWithValue("idUsuario",id);
                command.Parameters.AddWithValue("quantidade",carrinho.produtos.Count);
                command.Parameters.AddWithValue("preco",carrinho.ValorTotal());
                command.ExecuteNonQuery();
                conexao.Close();
            }
            {
                string sql = "SELECT MAX(idPedido) as idPedido FROM Pedido";
                conexao.Open();
                MySqlCommand command = new MySqlCommand(sql,conexao);            
                MySqlDataReader reader = command.ExecuteReader();            
                reader.Read();                
                idPedido = reader.GetInt32("idPedido");            
                reader.Close();
                conexao.Close();
            }
            {
                string sql = "INSERT INTO ItensPedido VALUES(@idPedido,@idProduto)"; 
                foreach (var item in carrinho.produtos)
                {
                    conexao.Open();
                    MySqlCommand command = new MySqlCommand(sql,conexao);            
                    command.Parameters.AddWithValue("idPedido",idPedido);
                    command.Parameters.AddWithValue("idProduto",item.id);
                    command.ExecuteNonQuery();
                    conexao.Close();
                    i++;
                }
            }
        }

        public List<Pedido> ListaPedidos()
        {
            string sql = "select Pedido.idPedido,Usuario.idUsuario,Usuario.nome,Usuario.cpf,Pedido.dataPedido,Pedido.quantidadeProduto,Pedido.preco from Pedido inner join Usuario ON Pedido.idUsuario=Usuario.idUsuario";
            conexao.Open();
            MySqlCommand command = new MySqlCommand(sql,conexao);            
            MySqlDataReader reader = command.ExecuteReader();            
            List<Pedido> listaPedidos = new List<Pedido>();
            
            while(reader.Read()){
                Pedido pedido = new Pedido();
                pedido.idPedido = reader.GetInt32("idPedido");
                if(!reader.IsDBNull(reader.GetOrdinal("idUsuario")))
                    pedido.idUsuario = reader.GetInt32("idUsuario");
                if(!reader.IsDBNull(reader.GetOrdinal("nome")))
                    pedido.nomeUsuario = reader.GetString("nome");
                if(!reader.IsDBNull(reader.GetOrdinal("cpf")))
                    pedido.cpfUsuario = reader.GetString("cpf");
                if(!reader.IsDBNull(reader.GetOrdinal("dataPedido")))
                    pedido.dataPedido = reader.GetDateTime("dataPedido");
                if(!reader.IsDBNull(reader.GetOrdinal("quantidadeProduto")))
                    pedido.quantidadeProduto = reader.GetInt32("quantidadeProduto");
                if(!reader.IsDBNull(reader.GetOrdinal("preco")))
                    pedido.preco = reader.GetFloat("preco");
                listaPedidos.Add(pedido);
            }
            reader.Close();
            conexao.Close();
            return listaPedidos;
        }

        public List<Pedido> ListaPedidos(int id)
        {
            string sql = "SELECT * FROM Pedido WHERE idUsuario=@id";
            conexao.Open();
            MySqlCommand command = new MySqlCommand(sql,conexao); 
            command.Parameters.AddWithValue("id",id);
            MySqlDataReader reader = command.ExecuteReader();            
            List<Pedido> listaPedidos = new List<Pedido>();
            
            while(reader.Read()){
                Pedido pedido = new Pedido();
                pedido.idPedido = reader.GetInt32("idPedido");
                if(!reader.IsDBNull(reader.GetOrdinal("idUsuario")))
                    pedido.idUsuario = reader.GetInt32("idUsuario");
                if(!reader.IsDBNull(reader.GetOrdinal("dataPedido")))
                    pedido.dataPedido = reader.GetDateTime("dataPedido");
                if(!reader.IsDBNull(reader.GetOrdinal("quantidadeProduto")))
                    pedido.quantidadeProduto = reader.GetInt32("quantidadeProduto");
                if(!reader.IsDBNull(reader.GetOrdinal("preco")))
                    pedido.preco = reader.GetFloat("preco");
                listaPedidos.Add(pedido);
            }
            reader.Close();
            conexao.Close();
            return listaPedidos;
        }
        public List<Produto> Detalhes(int idPedido)
        {         
            string sql = "select Produto.nome,Produto.preco from Produto inner join ItensPedido ON Produto.idProduto=ItensPedido.idProduto WHERE ItensPedido.idPedido=@idPedido";
            conexao.Open();
            MySqlCommand command = new MySqlCommand(sql,conexao); 
            command.Parameters.AddWithValue("idPedido",idPedido);
            MySqlDataReader reader = command.ExecuteReader();            
            List<Produto> listaProdutos = new List<Produto>();
            int i = 0;
            while(reader.Read()){
                Produto pedido = new Produto();
                if(!reader.IsDBNull(reader.GetOrdinal("nome")))
                    pedido.nome = reader.GetString("nome");
                if(!reader.IsDBNull(reader.GetOrdinal("preco")))
                    pedido.preco = reader.GetFloat("preco");
                listaProdutos.Add(pedido);
                i++;
            }
            reader.Close();
            conexao.Close();

            return listaProdutos;
        }
        public void ExcluirPedido(int id)
        {
            string sql = "DELETE FROM ItensPedido WHERE idPedido=@id;"+
            "DELETE FROM Pedido WHERE idPedido=@id";        
            conexao.Open();
            MySqlCommand command = new MySqlCommand(sql,conexao);            
            command.Parameters.AddWithValue("id",id);            
            command.ExecuteNonQuery();
            conexao.Clone();
        }
    }
}