using System.ComponentModel.DataAnnotations;

namespace ProjetoEmprestimoLivroCurso.Dto.Endereco
{
    public class EnderecoEditarDto
    {
        public  int  Id { get; set; }   

        [Required(ErrorMessage = "Digite o logradouro.")]
        public string Logradouro { get; set; } = string.Empty;
        [Required(ErrorMessage = "Digite o bairro.")]
        public string Bairro { get; set; } = string.Empty;
        [Required(ErrorMessage = "Digite o número.")]
        public string Numero { get; set; } = string.Empty;
        //[Required(ErrorMessage = "Digite a cidade.")]
        //public string Cidade { get; set; } = string.Empty;
        [Required(ErrorMessage = "Digite o estado.")]
        public string Estado { get; set; } = string.Empty;
        [Required(ErrorMessage = "Digite o Cep.")]
        public string Cep { get; set; } = string.Empty;

        public string? Complemento { get; set; } = string.Empty;
        public  int UsuarioId { get; set; }
    }
}
