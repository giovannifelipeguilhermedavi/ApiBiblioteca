using System.ComponentModel.DataAnnotations;

namespace WebBiblioteca.Models
{
    public class UsuarioView
    {        
        public int IdTipo { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório.")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório.")]
        public string Senha { get; set; }
    }
}
