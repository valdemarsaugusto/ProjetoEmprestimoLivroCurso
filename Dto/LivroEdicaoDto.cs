using System.ComponentModel.DataAnnotations;

namespace ProjetoEmprestimoLivroCurso.Dto
{
    public class LivroEdicaoDto
    {
        public int Id { get; set; }

        public string? Capa { get; set; }

        [Required(ErrorMessage = "Insira um título.")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Insira a descrição.")]
        public string Descricao { get; set; } = string.Empty;
        //[Required(ErrorMessage = "Insira uma capa.")]
        //public string Capa { get; set; } = string.Empty;
        [Required(ErrorMessage = "Insira um ISBN.")]
        public string ISBN { get; set; } = string.Empty;
        [Required(ErrorMessage = "Insira o autor.")]
        public string Autor { get; set; } = string.Empty;
        [Required(ErrorMessage = "Insira o gênero.")]
        public string Genero { get; set; } = string.Empty;
        [Required(ErrorMessage = "Insira o ano da publicação.")]
        public int AnoPublicacao { get; set; }
        [Required(ErrorMessage = "Insira uma quantidade no estoque.")]
        public int QuantidadeEmEstoque { get; set; }
    }
}
