using Biblioteca.Dominio.Entidades;
using Biblioteca.Dominio.Interfaces.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Dominio.Servico.Usuarios
{
    public class UsuarioServ
    {
        private readonly IUsuarioRepo usuarioRepo;

        public UsuarioServ(IUsuarioRepo usuarioRepo)
        {
            this.usuarioRepo = usuarioRepo;
        }

        public async Task<int> Adicionar(Usuario usuario) => await this.usuarioRepo.Adicionar(usuario);

        public async Task Excluir(int id) => await this.usuarioRepo.Excluir(id);

        public async Task<Usuario> Selecionar(int id) => await this.usuarioRepo.Selecionar(id);

        public async Task<List<Usuario>> Selecionar() => await this.usuarioRepo.Selecionar();

        public async Task<bool> Existe(int id) => await this.usuarioRepo.Existe(id);

        public async Task<Usuario> GetUsuario(string login) => await this.usuarioRepo.GetUsuario(login);
    }
}
