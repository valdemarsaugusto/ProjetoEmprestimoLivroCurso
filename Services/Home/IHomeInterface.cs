using ProjetoEmprestimoLivroCurso.Dto.Home;
using ProjetoEmprestimoLivroCurso.Models;

namespace ProjetoEmprestimoLivroCurso.Services.Home
{
    public interface IHomeInterface
    {
        Task<RespostaModel<UsuarioModel>> RealizarLogin(LoginDto loginDto);    
    }
}
