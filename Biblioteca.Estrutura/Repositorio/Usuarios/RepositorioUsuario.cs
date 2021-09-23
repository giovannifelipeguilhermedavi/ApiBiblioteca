using Biblioteca.Dominio.Entidades;
using Biblioteca.Dominio.Interfaces.Usuarios;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Estrutura.Repositorio
{
    public class RepositorioUsuario : IUsuarioRepo
    {
        private readonly string connectionString;

        public RepositorioUsuario(IConfiguration connection)
        {
            this.connectionString = connection.GetConnectionString("BaseBiblioteca");
        }
        public async Task<int> Adicionar(Usuario obj)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                await connection.OpenAsync();
                var parametro = new DynamicParameters();
                parametro.Add("@IdTipo", obj.IdTipo, DbType.Int32);
                parametro.Add("@Login", obj.Login, DbType.String);
                parametro.Add("@Senha", obj.Senha, DbType.String);

                string query = "INSERT INTO USUARIO " +
                               "(IdTipo,Login,Senha) " +
                               "Values " +
                               "(@IdTipo,@Login,@Senha) " +
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

            string query = "DELETE FROM USUARIO " +
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

                string query = "SELECT * FROM USUARIO " +
                               "WHERE Id = @Id";

                existe = connection.QueryFirstOrDefault<bool>(query, parametro);
            }
            return existe;
        }

        public async Task<Usuario> GetUsuario(string login)
        {
            Usuario usuario = null;

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                await connection.OpenAsync();
                var parametro = new DynamicParameters();
                parametro.Add("@Login", login, DbType.String);

                string query = "SELECT * FROM USUARIO " +
                               "WHERE Login = @Login";

                usuario = connection.QueryFirstOrDefault<Usuario>(query, parametro);
            }

            return usuario;
        }

        public async Task<List<Usuario>> Selecionar()
        {
            List<Usuario> usuarios = null;

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM USUARIO ORDER BY LOGIN ";

                usuarios = connection.Query<Usuario>(query).ToList();
            }

            return usuarios;
        }

        public async Task<Usuario> Selecionar(int id)
        {
            Usuario usuario = null;

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                await connection.OpenAsync();
                var parametro = new DynamicParameters();
                parametro.Add("@Id", id, DbType.Int32);

                string query = "SELECT * FROM USUARIO " +
                               "WHERE Id = @Id";

                usuario = connection.QueryFirstOrDefault<Usuario>(query, parametro);
            }

            return usuario;
        }
    }
}
