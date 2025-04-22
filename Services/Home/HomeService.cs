using ProjetoEmprestimoLivroCurso.Data;
using ProjetoEmprestimoLivroCurso.Dto.Home;
using ProjetoEmprestimoLivroCurso.Models;
using ProjetoEmprestimoLivroCurso.Services.Autenticacao;

namespace ProjetoEmprestimoLivroCurso.Services.Home
{
    public class HomeService : IHomeInterface
    {
        private readonly AppDbContent _context;
        private readonly IAutenticacaoInterface _autenticacaoInterface;

        public HomeService(AppDbContent context, IAutenticacaoInterface autenticacaoInterface)
        {
            _context = context;
            _autenticacaoInterface = autenticacaoInterface;
        }
        public async Task<RespostaModel<UsuarioModel>> RealizarLogin(LoginDto loginDto)
        {
            RespostaModel<UsuarioModel> resposta = new RespostaModel<UsuarioModel>();

            try
            {
                var usuario = _context.Usuarios.FirstOrDefault(usuarioBanco => usuarioBanco.Email == loginDto.Email);

                if (usuario == null)
                {
                    resposta.Dados = null;
                    resposta.Mensagem = "Credenciais inválidas.";
                    resposta.Status = false;

                    return resposta;
                }

                if (!_autenticacaoInterface.VerificaLogin(loginDto.Senha, usuario.SenhaHash, usuario.SenhaSalt))
                {
                    resposta.Dados = null;
                    resposta.Mensagem = "Credenciais inválidas.";
                    resposta.Status = false;

                    return resposta;

                }

                resposta.Dados = usuario;
                resposta.Mensagem = "Login efetuado com sucesso.";
                resposta.Status = true;

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Dados = null;
                resposta.Mensagem = ex.Message;
                resposta.Status = false;

                return resposta;
            }
        }
    }
}
