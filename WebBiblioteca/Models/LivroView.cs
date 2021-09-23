using System.ComponentModel.DataAnnotations;

namespace WebBiblioteca.Models
{
    public class LivroView
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Campo Obrigatório.")]
        public int IdEditora { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório.")]
        [MaxLength(70,ErrorMessage ="Máximo 70 caracteres.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório.")]
        [MaxLength(70, ErrorMessage = "Máximo 70 caracteres.")]
        public string Autor { get; set; }

        public string Editora { get; set; }
    }
}
