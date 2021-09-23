using Biblioteca.Dominio.Entidades;
using System.Threading.Tasks;

namespace Biblioteca.Dominio.Interfaces.Usuarios
{
    public interface IUsuarioRepo : IRepositorioBase<Biblioteca.Dominio.Entidades.Usuario>
    {
        Task<Usuario> GetUsuario(string login);
    }
}
