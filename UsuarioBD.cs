using System;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Projeto_Conexao_Banco_de_Dados
{
    public class UsuarioBD
    {
        public String BuscarNome(int idUusarioBusca)
        {
            using (MySqlConnection conexao = ConexaoBD.getInstancia().getConexao())
            {
                try
                {
                    conexao.Open();
                    MySqlCommand comando = conexao.CreateCommand();
                        comando.CommandText = "select Nome_usuario from usuarios where Id_usuario = @id;";
                        comando.Parameters.AddWithValue("id",idUusarioBusca);

                    MySqlDataReader leitura = comando.ExecuteReader();

                    while (leitura.Read())
                    {
                        if (leitura["Nome_usuario"] != null)
                        {
                            return(leitura["Nome_usuario"].ToString());
                        }
                    }

                }
                catch (MySqlException msqle)
                {
                    MessageBox.Show("Erro de acesso ao Banco de Dados - Mysql" + msqle.Message, "Erro");
                }
                finally
                {
                    conexao.Close();
                }

                return string.Empty;
            }
        }


        public void InserirNome(string inserirNome)
        {
            using (MySqlConnection conexao = ConexaoBD.getInstancia().getConexao())
            {

                try
                {
                    conexao.Open();
                    MySqlCommand comando = conexao.CreateCommand();
                        comando.CommandText = "insert into usuarios(Nome_usuario) values(@varNome);";
                        comando.Parameters.AddWithValue("varNome", inserirNome);

                    int valorRetorno = comando.ExecuteNonQuery(); // vai retornar a quantidade de linhas afetadas
                    if (valorRetorno < 1)
                    { // e se este valor de linhas for menor que um, deu erro na inclusão
                        MessageBox.Show("Erro ao inserir");
                    }

                    else
                    {
                        MessageBox.Show("Inserido com sucesso!");
                    }

                }
                catch (MySqlException msqle)
                {
                    MessageBox.Show("Erro de acesso ao Banco de Dados - Mysql" + msqle.Message, "Erro");
                }
                finally
                {
                    conexao.Close();
                }
            }
        }
    }
}
