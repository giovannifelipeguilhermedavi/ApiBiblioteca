using Biblioteca.Dominio.Interfaces.Editoras;
using Biblioteca.Dominio.Interfaces.Livros;
using Biblioteca.Dominio.Interfaces.Usuarios;
using Biblioteca.Dominio.Servico.Editoras;
using Biblioteca.Dominio.Servico.Livros;
using Biblioteca.Dominio.Servico.Usuarios;
using Biblioteca.Estrutura.Repositorio;
using Biblioteca.Estrutura.Repositorio.Editoras;
using Biblioteca.Estrutura.Repositorio.Livros;
using Microsoft.Extensions.DependencyInjection;

namespace Biblioteca.Estrutura.DI
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<ILivroRepo, RepositorioLivro>();
            services.AddScoped<LivroServ>();

            services.AddScoped<IEditoraRepo, RepositorioEditora>();
            services.AddScoped<EditoraServ>();

            services.AddScoped<IUsuarioRepo, RepositorioUsuario>();
            services.AddScoped<UsuarioServ>();


            return services;
        }
    }
}
