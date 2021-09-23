namespace Biblioteca.Dominio.Entidades
{
    public class Usuario : EntidadeBase
    {
        public int IdTipo { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}
