using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjetoEmprestimoLivroCurso.Models
{
    public class EnderecoModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Logradouro { get; set; } = string.Empty;
        [Required]
        public string Bairro { get; set; } = string.Empty;
        [Required]
        public string Numero { get; set; } = string.Empty;

        public string? Cidade { get; set; }
        [Required]
        public string Estado { get; set; } = string.Empty;
        [Required]
        public string Cep { get; set; } = string.Empty;
        public string? Complemento { get; set; } = string.Empty;
        public int UsuarioId { get; set; }
        [JsonIgnore]
        public UsuarioModel Usuario { get; set; }
    }
}
