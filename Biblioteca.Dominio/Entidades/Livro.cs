namespace Biblioteca.Dominio.Entidades
{
    public class Livro : EntidadeBase
    {
        public int IdEditora { get; set; }
        public string Nome { get; set; }
        public string Autor { get; set; }
        public string Editora { get; set; }
    }
}
