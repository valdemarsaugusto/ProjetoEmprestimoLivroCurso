using ProjetoEmprestimoLivroCurso.Dto.Usuario;
using ProjetoEmprestimoLivroCurso.Models;

namespace ProjetoEmprestimoLivroCurso.Services.Usuario
{
    public interface IUsuarioInterface
    {
        public Task<List<UsuarioModel>> BuscarUsuarios(int? id);
        public Task<bool> VerificaSeExisteUsuarioEEmail(UsuarioCriacaoDto usuarioCriacaoDto);   
        public Task<UsuarioCriacaoDto> Cadastrar(UsuarioCriacaoDto usuarioCriacaoDto);
        public Task<UsuarioModel> BuscarUsuarioPorId(int? id);
        public Task<UsuarioModel> MudarSituacaoUsuario(int id);
        public Task<UsuarioModel> Editar(UsuarioEditarDto usuarioEditarDto);


    }
}
