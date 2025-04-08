using Microsoft.EntityFrameworkCore;
using ProjetoEmprestimoLivroCurso.Models;

namespace ProjetoEmprestimoLivroCurso.Data
{
    public class AppDbContent : DbContext
    {
        public AppDbContent(DbContextOptions<AppDbContent> options) : base(options) 
        {
        
        }

        public DbSet<LivroModel> Livros { get; set; }
    }
}
