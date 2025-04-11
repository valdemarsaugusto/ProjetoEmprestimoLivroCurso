using ProjetoEmprestimoLivroCurso.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProjetoEmprestimoLivroCurso.Models
{
    public class UsuarioModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo Nome Completo é obrigatório.")]
        public string NomeCompleto { get; set; } = string.Empty;
        [Required(ErrorMessage = "O campo Usuário é obrigatório.")]
        public string Usuario { get; set; } = string.Empty;
        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        public string Email { get; set; } = string.Empty;
        [Required]  
        public bool Situacao { get; set; } = true;
        [Required]
        public PerfilEnum Perfil { get; set; }
        [Required]
        public TurnoEnum Turno { get; set; }
        [Required]
        public byte[] SenhaHash { get; set; }
        [Required]
        public byte[] SenhaSalt { get; set; }
        [Required]
        public EnderecoModel Endereco { get; set; }
        public DateTime DataCadastro { get; set; }  = DateTime.Now;
        public DateTime DataAlteracao { get; set; } = DateTime.Now;


    }
}
