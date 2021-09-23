using Biblioteca.Dominio.Entidades;
using Biblioteca.Dominio.Interfaces.Editoras;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Estrutura.Repositorio.Editoras
{
    public class RepositorioEditora : IEditoraRepo
    {
        private readonly string connectionString;

        public RepositorioEditora(IConfiguration connection)
        {
            this.connectionString = connection.GetConnectionString("BaseBiblioteca");
        }
        public async Task<int> Adicionar(Editora obj)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                await connection.OpenAsync();
                var parametro = new DynamicParameters();               
                parametro.Add("@Nome", obj.Nome, DbType.String);                

                string query = "INSERT INTO EDITORA " +
                               "(Nome) " +
                               "Values " +
                               "(@Nome) " +
                               "SELECT CAST(SCOPE_IDENTITY() AS INT)";

                id = connection.QueryFirstOrDefault<int>(query, parametro);
            }
            return id;
        }

        public async Task Excluir(int id)
        {
            using SqlConnection connection = new SqlConnection(this.connectionString);
            await connection.OpenAsync();
            var parametro = new DynamicParameters();
            parametro.Add("@Id", id, DbType.Int32);

            string query = "DELETE FROM EDITORA " +
                           "WHERE Id = @Id";

            connection.QueryFirstOrDefault<bool>(query, parametro);
        }

        public async Task<bool> Existe(int id)
        {
            bool existe = false;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                await connection.OpenAsync();
                var parametro = new DynamicParameters();
                parametro.Add("@Id", id, DbType.Int32);

                string query = "SELECT * FROM EDITORA " +
                               "WHERE Id = @Id)";

                existe = connection.QueryFirstOrDefault<bool>(query, parametro);
            }
            return existe;
        }

        public async Task<List<Editora>> Selecionar()
        {
            List<Editora> Editoras = null;

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM EDITORA ORDER BY NOME ";

                Editoras = connection.Query<Editora>(query).ToList();
            }

            return Editoras;
        }

        public async Task<Editora> Selecionar(int id)
        {
            Editora editora = null;

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                await connection.OpenAsync();
                var parametro = new DynamicParameters();
                parametro.Add("@Id", id, DbType.Int32);

                string query = "SELECT * FROM EDITORA " +
                               "WHERE Id = @Id";

                editora = connection.QueryFirstOrDefault<Editora>(query, parametro);
            }

            return editora;
        }
    }
}
