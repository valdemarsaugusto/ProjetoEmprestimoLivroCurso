using System.Text.Json.Serialization;

namespace ProjetoEmprestimoLivroCurso.Models
{
    public class EmprestimoModel
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        [JsonIgnore]
        public UsuarioModel Usuario { get; set; }
        public int LivroId { get; set; }
        [JsonIgnore]
        public LivroModel Livro { get; set; }   
        public DateTime DataDevolucao { get; set; } 

    }
}
