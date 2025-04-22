using ProjetoEmprestimoLivroCurso.Dto.Endereco;
using ProjetoEmprestimoLivroCurso.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProjetoEmprestimoLivroCurso.Dto.Usuario
{
    public class UsuarioEditarDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o nome completo.")]
        public string NomeCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "Digite o usuário.")]
        public string Usuario { get; set; } = string.Empty;
        [Required(ErrorMessage = "Digite o E-mail.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Selecione um perfil.")]
        public PerfilEnum Perfil { get; set; }
        [Required(ErrorMessage = "Selecione o turno.")]
        public TurnoEnum Turno { get; set; }

        public EnderecoEditarDto Endereco { get; set; }
    }
}

