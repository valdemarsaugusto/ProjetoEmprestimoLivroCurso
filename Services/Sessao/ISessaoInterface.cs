using ProjetoEmprestimoLivroCurso.Models;

namespace ProjetoEmprestimoLivroCurso.Services.Sessao
{
    public interface ISessaoInterface
    {
        UsuarioModel BuscarSessao();
        void CriarSessao(UsuarioModel usuario);
        void RemoverSessao();   

    }
}
