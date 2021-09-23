using ApiBiblioteca.ModelViews;
using AutoMapper;
using Biblioteca.Dominio.Entidades;

namespace ApiBiblioteca.Automapper
{
    /// <summary>
    /// Classe mapear objetos.
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Construtor.
        /// </summary>
        public MappingProfile()
        {
            this.CreateMap<Livro, LivroViewModel>().ReverseMap();
            this.CreateMap<Editora, EditoraViewModel>().ReverseMap();
            this.CreateMap<Usuario, UsuarioViewModel>().ReverseMap();            
        }
    }
}
