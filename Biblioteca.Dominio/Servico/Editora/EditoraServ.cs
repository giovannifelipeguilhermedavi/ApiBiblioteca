using Biblioteca.Dominio.Entidades;
using Biblioteca.Dominio.Interfaces.Editoras;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biblioteca.Dominio.Servico.Editoras
{
    public class EditoraServ
    {
        private readonly IEditoraRepo editoraRepo;

        public EditoraServ(IEditoraRepo editoraRepo)
        {
            this.editoraRepo = editoraRepo;
        }

        public async Task<int> Adicionar(Editora editora) => await this.editoraRepo.Adicionar(editora);

        public async Task Excluir(int id) => await this.editoraRepo.Excluir(id);

        public async Task<Editora> Selecionar(int id) => await this.editoraRepo.Selecionar(id);

        public async Task<List<Editora>> Selecionar() => await this.editoraRepo.Selecionar();

        public async Task<bool> Existe(int id) => await this.editoraRepo.Existe(id);
        
    }
}
