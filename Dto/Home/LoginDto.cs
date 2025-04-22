using System.ComponentModel.DataAnnotations;

namespace ProjetoEmprestimoLivroCurso.Dto.Home
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Insira o e-mail.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Insira a senha.")]
        public string Senha { get; set; }
    }
}
