using ProjetoEmprestimoLivroCurso.Enums;
using ProjetoEmprestimoLivroCurso.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjetoEmprestimoLivroCurso.Dto.Usuario
{
    public class UsuarioCriacaoDto
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o nome completo.")]
        public string NomeCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "Digite o usuário.")]
        public string Usuario { get; set; } = string.Empty;
        [Required(ErrorMessage = "Digite o E-mail.")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Digite a situação.")]
        public bool Situacao { get; set; } = true;
        [Required(ErrorMessage = "Selecione um perfil.")]
        public PerfilEnum Perfil { get; set; }
        [Required(ErrorMessage = "Selecione o turno.")]
        public TurnoEnum Turno { get; set; }
        [Required(ErrorMessage = "Digite a senha."), MinLength(6, ErrorMessage = "A senha deve conter no mínimo 6 caracteres.")]
        public string Senha { get; set; } = string.Empty;
        [Required(ErrorMessage = "Digite a confirmação de senha."), Compare("Senha", ErrorMessage = "As senha não coincidem.")]
        public string ConfirmarSenha { get; set; } = string.Empty;

        [Required(ErrorMessage = "Digite o logradouro.")]
        public string Logradouro { get; set; } = string.Empty;
        [Required(ErrorMessage = "Digite o bairro.")]
        public string Bairro { get; set; } = string.Empty;
        [Required(ErrorMessage = "Digite o número.")]
        public string Numero { get; set; } = string.Empty;
        [Required(ErrorMessage = "Digite a cidade.")]
        public string Cidade { get; set; } = string.Empty;
        [Required(ErrorMessage = "Digite o estado.")]
        public string Estado { get; set; } = string.Empty;
        [Required(ErrorMessage = "Digite o Cep.")]
        public string Cep { get; set; } = string.Empty;

        public string? Complemento { get; set; } = string.Empty;
    }
}
