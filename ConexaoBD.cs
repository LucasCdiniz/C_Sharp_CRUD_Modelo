using MySql.Data.MySqlClient;
using System.Configuration;

namespace Projeto_Conexao_Banco_de_Dados
{
    public class ConexaoBD
    {
        private static readonly ConexaoBD instanciaMySQL = new ConexaoBD();

        public static ConexaoBD getInstancia()
        {
            return instanciaMySQL;
        }

        public MySqlConnection getConexao()
        {
            string conn = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ToString();
            return new MySqlConnection(conn);
        }
    }
}
