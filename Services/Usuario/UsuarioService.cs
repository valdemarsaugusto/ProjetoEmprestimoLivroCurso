using Microsoft.EntityFrameworkCore;
using ProjetoEmprestimoLivroCurso.Data;
using ProjetoEmprestimoLivroCurso.Dto.Usuario;
using ProjetoEmprestimoLivroCurso.Models;
using ProjetoEmprestimoLivroCurso.Services.Autenticacao;

namespace ProjetoEmprestimoLivroCurso.Services.Usuario
{
    public class UsuarioService : IUsuarioInterface
    {
        private readonly AppDbContent _context;
        private readonly IAutenticacaoInterface _autenticacaoInterface;

        public UsuarioService(AppDbContent context, IAutenticacaoInterface autenticacaoInterface)
        {
            _context = context;
            _autenticacaoInterface = autenticacaoInterface;
        }
        public async Task<List<UsuarioModel>> BuscarUsuarios(int? id)
        {
            try
            {
                var registros = new List<UsuarioModel>();
                if (id != null) 
                {
                    registros = await _context.Usuarios.Where(cliente => cliente.Perfil == 0).Include(e => e.Endereco).ToListAsync();
                }
                else
                {
                    registros = await _context.Usuarios.Where(funcionario => funcionario.Perfil != 0).Include(e => e.Endereco).ToListAsync();
                }
                return registros;
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<UsuarioCriacaoDto> Cadastrar(UsuarioCriacaoDto usuarioCriacaoDto)
        {
            try
            {
                _autenticacaoInterface.CriarSenhaHash(usuarioCriacaoDto.Senha, out byte[] senhaHash, out byte[] senhaSalt);

                var usuario = new UsuarioModel
                {
                    NomeCompleto = usuarioCriacaoDto.NomeCompleto,
                    Usuario = usuarioCriacaoDto.Usuario,
                    Email = usuarioCriacaoDto.Email,
                    Perfil = usuarioCriacaoDto.Perfil,
                    Turno = usuarioCriacaoDto.Turno,
                    Situacao = usuarioCriacaoDto.Situacao,
                    SenhaHash = senhaHash,
                    SenhaSalt = senhaSalt

                };

                var endereco = new EnderecoModel
                {
                    Logradouro = usuarioCriacaoDto.Logradouro,
                    Bairro = usuarioCriacaoDto.Bairro,
                    Numero = usuarioCriacaoDto.Numero,
                    //Cidade = usuarioCriacaoDto.Cidade,
                    Estado = usuarioCriacaoDto.Estado,
                    Cep = usuarioCriacaoDto.Cep,
                    Complemento = usuarioCriacaoDto.Complemento,
                    Usuario = usuario
                };

                usuario.Endereco = endereco;
                _context.Add(usuario);
                await _context.SaveChangesAsync();  

                return usuarioCriacaoDto;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> VerificaSeExisteUsuarioEEmail(UsuarioCriacaoDto usuarioCriacaoDto)
        {
            try
            {
                var mesmoUsuario = await _context.Usuarios.FirstOrDefaultAsync(usuarioBanco => usuarioBanco.Usuario == usuarioCriacaoDto.Usuario
                                                                                || usuarioBanco.Email == usuarioCriacaoDto.Email);
                if (mesmoUsuario == null)
                {
                    return true;
                }

                return false;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
