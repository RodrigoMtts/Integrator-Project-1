using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace BomGosto.Models
{
    public class UsuarioRepository : Repository
    {
        public void FazContato(Usuario u){
            string sql = "INSERT INTO Contato VALUES(null,@nome,@email,@telefone,@mensagem,NOW())";
            conexao.Open();
            MySqlCommand command = new MySqlCommand(sql,conexao);            
            command.Parameters.AddWithValue("nome",u.nome);
            command.Parameters.AddWithValue("email",u.email);
            command.Parameters.AddWithValue("telefone",u.telefone);
            command.Parameters.AddWithValue("mensagem",u.mensagem);
            command.ExecuteNonQuery();
            conexao.Clone();
        }
        public List<Usuario> ListaContato()
        {
            string sql = "SELECT * FROM Contato";
            conexao.Open();
            MySqlCommand command = new MySqlCommand(sql,conexao);            
            MySqlDataReader reader = command.ExecuteReader();            
            List<Usuario> listaUsuarios = new List<Usuario>();
            
            while(reader.Read()){
                Usuario usuario = new Usuario();
                usuario.id = reader.GetInt32("idContato");
                if(!reader.IsDBNull(reader.GetOrdinal("nome")))
                    usuario.nome = reader.GetString("nome");                
                if(!reader.IsDBNull(reader.GetOrdinal("email")))
                    usuario.email = reader.GetString("email");
                if(!reader.IsDBNull(reader.GetOrdinal("telefone")))
                    usuario.telefone = reader.GetString("telefone");
                if(!reader.IsDBNull(reader.GetOrdinal("data")))
                    usuario.ultimoAcesso = reader.GetDateTime("data");
                if(!reader.IsDBNull(reader.GetOrdinal("mensagem")))
                    usuario.mensagem = reader.GetString("mensagem");   
                listaUsuarios.Add(usuario);
            }
            reader.Close();
            conexao.Close();
            return listaUsuarios;
        }
        public void ExcluirContato(int id)
        {
            string sql = "DELETE FROM Contato WHERE idContato=@id";        
            conexao.Open();
            MySqlCommand command = new MySqlCommand(sql,conexao);            
            command.Parameters.AddWithValue("id",id);
            command.ExecuteNonQuery();
            conexao.Clone();
        }
        

        public void Cadastrarse(Usuario u,string nomeImagem)
        {
            string sql = "INSERT INTO Usuario VALUES(null,@nome,@nomeUsuario,@cpf,@email,@telefone,@cep,@senha,@tipoUsuario,NOW(),NOW(),@nomeImagem)";        
            conexao.Open();
            MySqlCommand command = new MySqlCommand(sql,conexao);            
            command.Parameters.AddWithValue("nome",u.nome);
            command.Parameters.AddWithValue("nomeUsuario",u.nomeUsuario);
            command.Parameters.AddWithValue("cpf",u.cpf);
            command.Parameters.AddWithValue("email",u.email);
            command.Parameters.AddWithValue("telefone",u.telefone);
            command.Parameters.AddWithValue("cep",u.cep);
            command.Parameters.AddWithValue("senha",u.senha);
            command.Parameters.AddWithValue("tipoUsuario",u.tipoUsuario);
            command.Parameters.AddWithValue("nomeImagem",nomeImagem);
            command.ExecuteNonQuery();
            conexao.Clone();
        }
        public void Cadastrarse(Usuario u)
        {
            string sql = "INSERT INTO Usuario VALUES(null,@nome,@nomeUsuario,@cpf,@email,@telefone,@cep,@senha,@tipoUsuario,NOW(),NOW(),'perfil.jpg')";        
            conexao.Open();
            MySqlCommand command = new MySqlCommand(sql,conexao);            
            command.Parameters.AddWithValue("nome",u.nome);
            command.Parameters.AddWithValue("nomeUsuario",u.nomeUsuario);
            command.Parameters.AddWithValue("cpf",u.cpf);
            command.Parameters.AddWithValue("email",u.email);
            command.Parameters.AddWithValue("telefone",u.telefone);
            command.Parameters.AddWithValue("cep",u.cep);
            command.Parameters.AddWithValue("senha",u.senha);
            command.Parameters.AddWithValue("tipoUsuario",u.tipoUsuario);
            command.ExecuteNonQuery();
            conexao.Clone();
        }

        public Usuario Entrar(string nomeUsuario,string senha)
        {
            string sql = "UPDATE Usuario SET dataUltimoAcesso = NOW() WHERE nomeUsuario=@nomeUsuario;"+
            "SELECT * FROM Usuario WHERE nomeUsuario = @nomeUsuario AND senha = @senha";
            conexao.Open();
            MySqlCommand command = new MySqlCommand(sql,conexao);
            command.Parameters.AddWithValue("nomeUsuario",nomeUsuario);
            command.Parameters.AddWithValue("senha",senha);
            command.ExecuteNonQuery();
            MySqlDataReader reader = command.ExecuteReader();
            Usuario usuario = new Usuario();
            
            if(reader.Read()){
            usuario.id = reader.GetInt32("idUsuario");
            if(!reader.IsDBNull(reader.GetOrdinal("nome")))
                usuario.nome = reader.GetString("nome");
            if(!reader.IsDBNull(reader.GetOrdinal("nomeUsuario")))
                usuario.nomeUsuario = reader.GetString("nomeUsuario");
            if(!reader.IsDBNull(reader.GetOrdinal("cpf")))
                usuario.cpf = reader.GetString("cpf");
            if(!reader.IsDBNull(reader.GetOrdinal("email")))
                usuario.email = reader.GetString("email");
            if(!reader.IsDBNull(reader.GetOrdinal("telefone")))
                usuario.telefone = reader.GetString("telefone");
            if(!reader.IsDBNull(reader.GetOrdinal("cep")))
                usuario.cep = reader.GetString("cep");
            if(!reader.IsDBNull(reader.GetOrdinal("tipoUsuario")))
                usuario.tipoUsuario = reader.GetByte("tipoUsuario");
            if(!reader.IsDBNull(reader.GetOrdinal("dataCriacao")))
                usuario.criacaoConta = reader.GetDateTime("dataCriacao");
            if(!reader.IsDBNull(reader.GetOrdinal("dataUltimoAcesso")))
                usuario.ultimoAcesso = reader.GetDateTime("dataUltimoAcesso");
            if(!reader.IsDBNull(reader.GetOrdinal("imagem")))
                usuario.caminhoImagem= reader.GetString("imagem");
            }
            reader.Close();
            conexao.Close();
            return usuario;
        }

        public Usuario Detalhes(int id)
        {
            string sql = "SELECT * FROM Usuario WHERE idUsuario = @id";
            conexao.Open();
            MySqlCommand command = new MySqlCommand(sql,conexao);
            command.Parameters.AddWithValue("id",id);
            MySqlDataReader reader = command.ExecuteReader();
            Usuario usuario = new Usuario();
            
            if(reader.Read()){
            usuario.id = reader.GetInt32("idUsuario");
            if(!reader.IsDBNull(reader.GetOrdinal("nome")))
                usuario.nome = reader.GetString("nome");
            if(!reader.IsDBNull(reader.GetOrdinal("nomeUsuario")))
                usuario.nomeUsuario = reader.GetString("nomeUsuario");
            if(!reader.IsDBNull(reader.GetOrdinal("cpf")))
                usuario.cpf = reader.GetString("cpf");
            if(!reader.IsDBNull(reader.GetOrdinal("email")))
                usuario.email = reader.GetString("email");
            if(!reader.IsDBNull(reader.GetOrdinal("telefone")))
                usuario.telefone = reader.GetString("telefone");
            if(!reader.IsDBNull(reader.GetOrdinal("cep")))
                usuario.cep = reader.GetString("cep");
            if(!reader.IsDBNull(reader.GetOrdinal("tipoUsuario")))
                usuario.tipoUsuario = reader.GetByte("tipoUsuario");
            if(!reader.IsDBNull(reader.GetOrdinal("dataCriacao")))
                usuario.criacaoConta = reader.GetDateTime("dataCriacao");
            if(!reader.IsDBNull(reader.GetOrdinal("dataUltimoAcesso")))
                usuario.ultimoAcesso = reader.GetDateTime("dataUltimoAcesso");
            if(!reader.IsDBNull(reader.GetOrdinal("imagem")))
                usuario.caminhoImagem= reader.GetString("imagem");
            }
            reader.Close();
            conexao.Close();
            return usuario;
        }
        public Usuario Detalhes(Usuario u)
        {
            string sql = "SELECT * FROM Usuario WHERE nomeUsuario = @nomeUsuario";
            conexao.Open();
            MySqlCommand command = new MySqlCommand(sql,conexao);
            command.Parameters.AddWithValue("nomeUsaurio",u.nomeUsuario);            
            MySqlDataReader reader = command.ExecuteReader();
            Usuario usuario = new Usuario();
                        
            if(reader.Read()){
            usuario.id = reader.GetInt32("idUsuario");
            if(!reader.IsDBNull(reader.GetOrdinal("nome")))
                usuario.nome = reader.GetString("nome");
            if(!reader.IsDBNull(reader.GetOrdinal("nomeUsuario")))
                usuario.nomeUsuario = reader.GetString("nomeUsuario");
            if(!reader.IsDBNull(reader.GetOrdinal("cpf")))
                usuario.cpf = reader.GetString("cpf");
            if(!reader.IsDBNull(reader.GetOrdinal("email")))
                usuario.email = reader.GetString("email");
            if(!reader.IsDBNull(reader.GetOrdinal("telefone")))
                usuario.telefone = reader.GetString("telefone");
            if(!reader.IsDBNull(reader.GetOrdinal("cep")))
                usuario.cep = reader.GetString("cep");
            if(!reader.IsDBNull(reader.GetOrdinal("tipoUsuario")))
                usuario.tipoUsuario = reader.GetByte("tipoUsuario");
            if(!reader.IsDBNull(reader.GetOrdinal("dataCriacao")))
                usuario.criacaoConta = reader.GetDateTime("dataCriacao");
            if(!reader.IsDBNull(reader.GetOrdinal("dataUltimoAcesso")))
                usuario.ultimoAcesso = reader.GetDateTime("dataUltimoAcesso");
            if(!reader.IsDBNull(reader.GetOrdinal("imagem")))
                usuario.caminhoImagem = reader.GetString("imagem");
            }
            reader.Close();
            conexao.Close();
            return usuario;
        }
        public void AlterarSenha(int id,string senha)
        {
            string sql = "UPDATE Usuario SET senha = @senha WHERE idUsuario = @id";
            conexao.Open();
            MySqlCommand command = new MySqlCommand(sql,conexao);
            command.Parameters.AddWithValue("id",id);
            command.Parameters.AddWithValue("senha",senha);
            command.ExecuteNonQuery();
            conexao.Close();
        }
        public void AlterarDados(Usuario u)
        {
            string sql = "UPDATE Usuario SET nome=@nome,nomeUsuario=@nomeUsuario,cpf=@cpf,email=@email,telefone=@telefone,cep=@cep WHERE idUsuario=@id";        
            conexao.Open();
            MySqlCommand command = new MySqlCommand(sql,conexao);            
            command.Parameters.AddWithValue("id",u.id);
            command.Parameters.AddWithValue("nome",u.nome);
            command.Parameters.AddWithValue("nomeUsuario",u.nomeUsuario);
            command.Parameters.AddWithValue("cpf",u.cpf);
            command.Parameters.AddWithValue("email",u.email);
            command.Parameters.AddWithValue("telefone",u.telefone);
            command.Parameters.AddWithValue("cep",u.cep);
            command.Parameters.AddWithValue("senha",u.senha);
            command.ExecuteNonQuery();
            conexao.Clone();
        }
        public void AlterarDados(Usuario u,string nomeImagem)
        {
            string sql = "UPDATE Usuario SET nome=@nome,nomeUsuario=@nomeUsuario,cpf=@cpf,email=@email,telefone=@telefone,cep=@cep,imagem=@nomeImagem WHERE idUsuario=@id";        
            conexao.Open();
            MySqlCommand command = new MySqlCommand(sql,conexao);            
            command.Parameters.AddWithValue("id",u.id);
            command.Parameters.AddWithValue("nome",u.nome);
            command.Parameters.AddWithValue("nomeUsuario",u.nomeUsuario);
            command.Parameters.AddWithValue("cpf",u.cpf);
            command.Parameters.AddWithValue("email",u.email);
            command.Parameters.AddWithValue("telefone",u.telefone);
            command.Parameters.AddWithValue("cep",u.cep);
            command.Parameters.AddWithValue("senha",u.senha);
            command.Parameters.AddWithValue("@nomeImagem",nomeImagem);
            command.ExecuteNonQuery();
            conexao.Clone();
        }
        public void ExcluirConta(int id)
        {
            conexao.Open();
            PedidoRepository pedidoRepo = new PedidoRepository();
            List<Pedido> pedidos = pedidoRepo.ListaPedidos(id);
            string sql = "";
            foreach (var item in pedidos)
            {
                sql= "DELETE FROM ItensPedido WHERE idPedido=@id";
                MySqlCommand command = new MySqlCommand(sql,conexao);
                command.Parameters.AddWithValue("id",item.idPedido);    
                command.ExecuteNonQuery();
                
            }        
            sql = "DELETE FROM Pedido WHERE idUsuario=@id;"+
            "DELETE FROM Usuario WHERE idUsuario=@id";
            MySqlCommand command2 = new MySqlCommand(sql,conexao);
            command2.Parameters.AddWithValue("id",id);    
            command2.ExecuteNonQuery();   
            conexao.Clone();
        }
        public void ExcluirImagem(int id)
        {
            string sql = "UPDATE Usuario SET imagem='perfil.jpg' WHERE idUsuario=@id";        
            conexao.Open();
            MySqlCommand command = new MySqlCommand(sql,conexao);            
            command.Parameters.AddWithValue("id",id);            
            command.ExecuteNonQuery();
            conexao.Clone();
        }
        public List<Usuario> ListaUsuarios()
        {
            string sql = "SELECT * FROM Usuario WHERE tipoUsuario=2";
            conexao.Open();
            MySqlCommand command = new MySqlCommand(sql,conexao);            
            MySqlDataReader reader = command.ExecuteReader();            
            List<Usuario> listaUsuarios = new List<Usuario>();
            
            while(reader.Read()){
                Usuario usuario = new Usuario();
                usuario.id = reader.GetInt32("idUsuario");
                if(!reader.IsDBNull(reader.GetOrdinal("nome")))
                    usuario.nome = reader.GetString("nome");
                if(!reader.IsDBNull(reader.GetOrdinal("nomeUsuario")))
                    usuario.nomeUsuario = reader.GetString("nomeUsuario");
                if(!reader.IsDBNull(reader.GetOrdinal("cpf")))
                    usuario.cpf = reader.GetString("cpf");
                if(!reader.IsDBNull(reader.GetOrdinal("email")))
                    usuario.email = reader.GetString("email");
                if(!reader.IsDBNull(reader.GetOrdinal("telefone")))
                    usuario.telefone = reader.GetString("telefone");
                if(!reader.IsDBNull(reader.GetOrdinal("cep")))
                    usuario.cep = reader.GetString("cep");
                if(!reader.IsDBNull(reader.GetOrdinal("tipoUsuario")))
                    usuario.tipoUsuario = reader.GetByte("tipoUsuario");
                if(!reader.IsDBNull(reader.GetOrdinal("dataCriacao")))
                    usuario.criacaoConta = reader.GetDateTime("dataCriacao");
                if(!reader.IsDBNull(reader.GetOrdinal("dataUltimoAcesso")))
                    usuario.ultimoAcesso = reader.GetDateTime("dataUltimoAcesso");
                listaUsuarios.Add(usuario);
            }
            reader.Close();
            conexao.Close();
            return listaUsuarios;
        }
        public List<Usuario> ListaColaborador()
        {
            string sql = "SELECT * FROM Usuario WHERE tipoUsuario=1";
            conexao.Open();
            MySqlCommand command = new MySqlCommand(sql,conexao);            
            MySqlDataReader reader = command.ExecuteReader();            
            List<Usuario> listaUsuarios = new List<Usuario>();
            
            while(reader.Read()){
                Usuario usuario = new Usuario();
                usuario.id = reader.GetInt32("idUsuario");
                if(!reader.IsDBNull(reader.GetOrdinal("nome")))
                    usuario.nome = reader.GetString("nome");
                if(!reader.IsDBNull(reader.GetOrdinal("nomeUsuario")))
                    usuario.nomeUsuario = reader.GetString("nomeUsuario");
                if(!reader.IsDBNull(reader.GetOrdinal("cpf")))
                    usuario.cpf = reader.GetString("cpf");
                if(!reader.IsDBNull(reader.GetOrdinal("email")))
                    usuario.email = reader.GetString("email");
                if(!reader.IsDBNull(reader.GetOrdinal("telefone")))
                    usuario.telefone = reader.GetString("telefone");
                if(!reader.IsDBNull(reader.GetOrdinal("cep")))
                    usuario.cep = reader.GetString("cep");
                if(!reader.IsDBNull(reader.GetOrdinal("tipoUsuario")))
                    usuario.tipoUsuario = reader.GetByte("tipoUsuario");
                if(!reader.IsDBNull(reader.GetOrdinal("dataCriacao")))
                    usuario.criacaoConta = reader.GetDateTime("dataCriacao");
                if(!reader.IsDBNull(reader.GetOrdinal("dataUltimoAcesso")))
                    usuario.ultimoAcesso = reader.GetDateTime("dataUltimoAcesso");
                listaUsuarios.Add(usuario);
            }
            reader.Close();
            conexao.Close();
            return listaUsuarios;
        }
        public void CadastraColaborador(Usuario u)
        {
            string sql = "INSERT INTO Usuario VALUES(null,@nome,@nomeUsuario,@cpf,@email,@telefone,@cep,@senha,1,NOW(),NOW())";        
            conexao.Open();
            MySqlCommand command = new MySqlCommand(sql,conexao);            
            command.Parameters.AddWithValue("nome",u.nome);
            command.Parameters.AddWithValue("nomeUsuario",u.nomeUsuario);
            command.Parameters.AddWithValue("cpf",u.cpf);
            command.Parameters.AddWithValue("email",u.email);
            command.Parameters.AddWithValue("telefone",u.telefone);
            command.Parameters.AddWithValue("cep",u.cep);
            command.Parameters.AddWithValue("senha",u.senha);
            command.ExecuteNonQuery();
            conexao.Clone();
        }

    }
}