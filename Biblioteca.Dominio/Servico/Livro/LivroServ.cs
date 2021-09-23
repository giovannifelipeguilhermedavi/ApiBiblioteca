using Biblioteca.Dominio.Entidades;
using Biblioteca.Dominio.Interfaces.Livros;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biblioteca.Dominio.Servico.Livros
{
    public class LivroServ
    {
        private readonly ILivroRepo livroRepo;

        public LivroServ(ILivroRepo livroRepo)
        {
            this.livroRepo = livroRepo;
        }

        public async Task<int> Adicionar(Livro livro) => await this.livroRepo.Adicionar(livro);

        public async Task Excluir(int id) => await this.livroRepo.Excluir(id);

        public async Task<Livro> Selecionar(int id) => await this.livroRepo.Selecionar(id);

        public async Task<List<Livro>> Selecionar() => await this.livroRepo.Selecionar();

        public async Task<bool> Existe(int id) => await this.livroRepo.Existe(id);

        public async Task<bool> ExisteLivro(string nome, string autor) => await this.livroRepo.ExisteLivro(nome, autor);
    }
}
