using System;
using System.Windows.Forms;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace Projeto_Conexao_Banco_de_Dados
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       public void Update()
        {
            //Pega a conexão do App.config
            string conn = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ToString();
            MySqlConnection conexao = new MySqlConnection(conn);

            try
            {
                conexao.Open();
                // MessageBox.Show("Conexão realizada com sucesso!","Conexão Criada");
                MySqlCommand comando = conexao.CreateCommand();
                if (!txtAlteraNome.Text.Trim().Equals(String.Empty)&& !txtId.Text.Trim().Equals(String.Empty))
                {
                    comando.CommandText = "update usuarios " +
                                          "set Nome_usuario = @nome " +
                                          "where Id_usuario = @id; ";
                    comando.Parameters.AddWithValue("id", Convert.ToInt32(txtId.Text.Trim()));
                    comando.Parameters.AddWithValue("nome", txtAlteraNome.Text.Trim());
                }
                else
                {
                    MessageBox.Show("Campo Vazio!");
                    return;
                }

                int valorRetorno = comando.ExecuteNonQuery(); // vai retornar a quantidade de linhas afetadas
                if (valorRetorno < 1)
                { // e se este valor de linhas for menor que um, deu erro na inclusão
                    MessageBox.Show("Erro ao alterar");
                    txtId.Clear();
                    txtAlteraNome.Clear();
                }

                else
                {
                    MessageBox.Show("Alterado com sucesso!");
                    txtId.Clear();
                    txtAlteraNome.Clear();
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

        public void Delete()
        {
            //Pega a conexão do App.config
            MySqlConnection conexao = ConexaoBD.getInstancia().getConexao();

            try
            {
                conexao.Open();
                // MessageBox.Show("Conexão realizada com sucesso!","Conexão Criada");
                MySqlCommand comando = conexao.CreateCommand();
                if (!txtIdDelete.Text.Trim().Equals(String.Empty))
                {
                    comando.CommandText = "delete from usuarios where Id_usuario = @id;";
                    comando.Parameters.AddWithValue("id", Convert.ToInt32(txtIdDelete.Text.Trim()));
                }
                else
                {
                    MessageBox.Show("Campo Vazio!");
                    return;
                }

                int valorRetorno = comando.ExecuteNonQuery(); // vai retornar a quantidade de linhas afetadas
                if (valorRetorno < 1)
                { // e se este valor de linhas for menor que um, deu erro na inclusão
                    MessageBox.Show("Erro ao excluir");
                    txtIdDelete.Clear();
                }

                else
                {
                    MessageBox.Show("Exclusão realizada com sucesso!");
                    txtIdDelete.Clear();
                }

            }
            catch (MySqlException msqle)
            {
                MessageBox.Show("Erro de acesso ao Banco de Dados - Mysql " + msqle.Message, "Erro");
            }
            finally
            {
                conexao.Close();
            }
        }


        // Realizando tudo pelo evento Click
        private void btnConsulta_Click(object sender, EventArgs e)
        {
            MessageBox.Show(new UsuarioBD().BuscarNome(Convert.ToInt32(txtSelect.Text.Trim())));
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            new UsuarioBD().InserirNome(txtNome.Text.Trim());
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Update();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }
    }
}
