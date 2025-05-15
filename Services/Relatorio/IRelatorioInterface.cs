using System.Data;

namespace ProjetoEmprestimoLivroCurso.Services.Relatorio
{
    public interface IRelatorioInterface
    {
        DataTable ColetarDados<T>(List<T> dados, int idRelatorio);
    }
}
