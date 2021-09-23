using Biblioteca.Dominio.Entidades;
using Biblioteca.Dominio.Interfaces.Livros;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Estrutura.Repositorio.Livros
{
    class RepositorioLivro : ILivroRepo
    {
        private readonly string connectionString;

        public RepositorioLivro(IConfiguration connection)
        {
            this.connectionString = connection.GetConnectionString("BaseBiblioteca");
        }
        public async Task<int> Adicionar(Livro obj)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                await connection.OpenAsync();
                var parametro = new DynamicParameters();
                parametro.Add("@IdEditora", obj.IdEditora, DbType.Int32);
                parametro.Add("@Nome", obj.Nome, DbType.String);
                parametro.Add("@Autor", obj.Autor, DbType.String);

                string query = "INSERT INTO LIVRO " +
                               "(IdEditora,Nome,Autor) " +
                               "Values " +
                               "(@IdEditora,@Nome,@Autor) " +
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

            string query = "DELETE FROM LIVRO " +
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

                string query = "SELECT * FROM LIVRO " +
                               "WHERE Id = @Id)";

                existe = connection.QueryFirstOrDefault<bool>(query, parametro);
            }
            return existe;
        }

        public async Task<bool> ExisteLivro(string nome, string autor)
        {
            bool existe = false;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                await connection.OpenAsync();
                var parametro = new DynamicParameters();
                parametro.Add("@Nome", nome, DbType.String);
                parametro.Add("@Autor", autor, DbType.String);

                string query = "SELECT * FROM LIVRO " +
                               "WHERE Nome = @Nome AND Autor = @Autor)";

                existe = connection.QueryFirstOrDefault<bool>(query, parametro);
            }
            return existe;
        }

        public async Task<List<Livro>> Selecionar()
        {
            List<Livro> livros = null;

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT L.Id, L.IdEditora, L.Nome, L.Autor, E.Nome as Editora FROM LIVRO AS L " +
                               "INNER JOIN EDITORA AS E ON E.Id = L.IdEditora " +
                               "ORDER BY L.NOME";
                               
                livros = connection.Query<Livro>(query).ToList();
            }

            return livros;
        }

        public async Task<Livro> Selecionar(int id)
        {
            Livro livro = null;

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                await connection.OpenAsync();
                var parametro = new DynamicParameters();
                parametro.Add("@Id", id, DbType.Int32);

                string query = "SELECT L.Id, L.IdEditora, L.Nome, L.Autor, E.Nome as Editora FROM LIVRO AS L " +
                               "INNER JOIN EDITORA AS E ON E.Id = L.IdEditora " +
                               "WHERE Id = @Id";

                livro = connection.QueryFirstOrDefault<Livro>(query, parametro);
            }

            return livro;
        }
    }
}
