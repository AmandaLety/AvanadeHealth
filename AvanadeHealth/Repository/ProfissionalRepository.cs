using AvanadeHealth.Entidade;
using System.Data;
using System.Data.SqlClient;

namespace AvanadeHealth.Repository
{
    public class ProfissionalRepository
    {
        public IList<Profissional> Listar()

        {
            IList<Profissional> lista = new List<Profissional>();
            var connectionString = new ConfigurationBuilder()
                                            .SetBasePath(Directory.GetCurrentDirectory())
                                            .AddJsonFile("appsettings.json")
                                            .Build().GetConnectionString("SqlHealth");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                String query =
                    "SELECT IDPROFISSIONAL, NOME, TELEFONE, ENDERECO, ATIVO FROM PROFISSIONAL  ";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();


                while (dataReader.Read())
                {
                    // Recupera os dados
                    Profissional profissional = new Profissional();
                    profissional.IdProfissional = Convert.ToInt32(dataReader["IDPROFISSIONAL"]);
                    profissional.Nome = dataReader["NOME"].ToString();
                    profissional.Telefone = dataReader["TELEFONE"].ToString();
                    profissional.Endereco = dataReader["ENDERECO"].ToString();
                    profissional.Ativo = dataReader["ATIVO"].Equals("1");


                    // Adiciona o modelo da lista
                    lista.Add(profissional);
                }
                connection.Close();
            } // Finaliza o objeto connection
            // Retorna a lista
            return lista;
        }
        public Profissional Consultar(int id)
        {
            Profissional profissional = new Profissional();

            //a conexão não muda
            var connectionString = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json")
                                        .Build().GetConnectionString("SqlHealth");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                String query =
                    "SELECT IDPROFISSIONAL, NOME, TELEFONE, ENDERECO, ATIVO FROM PROFISSIONAL WHERE IDPROFISSIONAL = @IDPROFISSIONAL ";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@IDPROFISSIONAL", SqlDbType.Int);
                command.Parameters["@IDPROFISSIONAL"].Value = id;
                connection.Open();//ABRIR CONEXÃO


                SqlDataReader dataReader = command.ExecuteReader();//EXECUTA COMANDO


                while (dataReader.Read())
                {
                    // Recupera os dados

                    profissional.IdProfissional = Convert.ToInt32(dataReader["IDPROFISSIONAL"]);
                    profissional.Nome = dataReader["NOME"].ToString();
                    profissional.Telefone = dataReader["TELEFONE"].ToString();
                    profissional.Endereco = dataReader["ENDERECO"].ToString();
                    profissional.Ativo = dataReader["ATIVO"].Equals("1");

                }
                connection.Close();
            } // Finaliza o objeto connection

            // Retorna a lista para view
            return profissional;
        }
        public void Inserir(Profissional profissional)
        {
            var connectionString = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json")
                                        .Build().GetConnectionString("SqlHealth");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                String query =
                    "INSERT INTO PROFISSIONAL ( NOME, TELEFONE, ENDERECO, ATIVO ) VALUES ( @nome, @telefone, @endereco, @ativo ) ";

                SqlCommand command = new SqlCommand(query, connection);

                // Adicionando o valor ao comando
                command.Parameters.Add("@nome", SqlDbType.Text);
                command.Parameters.Add("@telefone", SqlDbType.Text);
                command.Parameters.Add("@endereco", SqlDbType.Text);
                command.Parameters.Add("@ativo", SqlDbType.Int);
                command.Parameters["@nome"].Value = profissional.Nome;
                command.Parameters["@telefone"].Value = profissional.Telefone;
                command.Parameters["@endereco"].Value = profissional.Endereco;
                command.Parameters["@ativo"].Value = profissional.Ativo.Equals("1");


                // Abrindo a conexão com  o Banco
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void Alterar(Profissional profissional)
        {
            var connectionString = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json")
                                        .Build().GetConnectionString("SqlHealth");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                String query =
                    "UPDATE PROFISSIONAL SET NOME = @nome, TELEFONE = @telefone, ENDERECO = @endereco, ATIVO = @ativo WHERE IDPROFISSIONAL = @IdProfissional  ";

                SqlCommand command = new SqlCommand(query, connection);

                // Adicionando o valor ao comando

                command.Parameters.Add("@idprofissional", SqlDbType.Int);
                command.Parameters.Add("@nome", SqlDbType.Text);
                command.Parameters.Add("@telefone", SqlDbType.Text);
                command.Parameters.Add("@endereco", SqlDbType.Text);
                command.Parameters.Add("@ativo", SqlDbType.Int);
                command.Parameters["@idprofissional"].Value = profissional.IdProfissional;
                command.Parameters["@nome"].Value = profissional.Nome;
                command.Parameters["@telefone"].Value = profissional.Telefone;
                command.Parameters["@endereco"].Value = profissional.Endereco;
                command.Parameters["@ativo"].Value = profissional.Ativo.Equals("1");


                // Abrindo a conexão com  o Banco
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void Excluir(int id)
        {
            var connectionString = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json")
                                        .Build().GetConnectionString("SqlHealth");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                String query =
                    "DELETE PROFISSIONAL WHERE IDPROFISSIONAL = @idprofissional  ";

                SqlCommand command = new SqlCommand(query, connection);

                // Adicionando o valor ao comando
                command.Parameters.Add("@idprofissional", SqlDbType.Int);
                command.Parameters["@idprofissional"].Value = id;

                // Abrindo a conexão com  o Banco
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
