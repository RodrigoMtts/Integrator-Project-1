using MySql.Data.MySqlClient;

namespace BomGosto.Models
{
    public class Repository
    {
        protected const string _sqlConexao = "Database=BomGosto;Data Source=localhost;User Id=root";
        protected MySqlConnection conexao = new MySqlConnection(_sqlConexao);
    }
}
