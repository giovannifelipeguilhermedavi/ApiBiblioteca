using Biblioteca.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biblioteca.Dominio.Interfaces
{
    public interface IRepositorioBase<T> where T : EntidadeBase
    {
        Task<int> Adicionar(T obj);

        Task Excluir(int id);

        Task<List<T>> Selecionar();

        Task<T> Selecionar(int id);

        Task<bool> Existe(int id);
    }
}
