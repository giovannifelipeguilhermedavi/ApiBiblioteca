using Biblioteca.Dominio.Entidades;
using System.Threading.Tasks;

namespace Biblioteca.Dominio.Interfaces.Livros
{
    public interface ILivroRepo : IRepositorioBase<Livro>
    {
        Task<bool> ExisteLivro(string nome, string autor);
    }
}
