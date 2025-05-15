using ProjetoEmprestimoLivroCurso.Models;

namespace ProjetoEmprestimoLivroCurso.Services.Emprestimo
{
    public interface IEmprestimoInterface
    {
        Task<RespostaModel<EmprestimoModel>> Emprestar(int livroId);

        Task<List<EmprestimoModel>> BuscarEmprestimosFiltro(UsuarioModel usuarioSessao, string pesquisar);
        Task<List<EmprestimoModel>> BuscarEmprestimos(UsuarioModel usuarioSessao);
        Task<List<EmprestimoModel>> BuscarEmprestimosGeral(string tipo  = null);

        Task<RespostaModel<EmprestimoModel>> Devolver(int id);
    }
}
