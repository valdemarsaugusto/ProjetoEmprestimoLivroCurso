using AutoMapper;
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
        private readonly IMapper _mapper;

        public UsuarioService(AppDbContent context, IAutenticacaoInterface autenticacaoInterface, IMapper mapper)
        {
            _context = context;
            _autenticacaoInterface = autenticacaoInterface;
            _mapper = mapper;
        }

        public Task<UsuarioModel> BuscarUsuarioPorId(int? id)
        {
            try
            {
                var usuario = _context.Usuarios.Include(e => e.Endereco).FirstOrDefaultAsync(usuarioBanco => usuarioBanco.Id == id);
                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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

        public async Task<UsuarioModel> Editar(UsuarioEditarDto usuarioEditarDto)
        {
            try
            {
                var usuarioEditarBanco = await _context.Usuarios.Include(e => e.Endereco).FirstOrDefaultAsync(usuarioBanco => usuarioBanco.Id == usuarioEditarDto.Id);
                if (usuarioEditarBanco != null)
                {
                    usuarioEditarBanco.Turno = usuarioEditarDto.Turno;
                    usuarioEditarBanco.Perfil = usuarioEditarDto.Perfil;
                    usuarioEditarBanco.NomeCompleto = usuarioEditarDto.NomeCompleto;
                    usuarioEditarBanco.Usuario = usuarioEditarDto.Usuario;
                    usuarioEditarBanco.Email = usuarioEditarDto.Email;
                    usuarioEditarBanco.DataAlteracao = DateTime.Now;
                    usuarioEditarBanco.Endereco = _mapper.Map<EnderecoModel>(usuarioEditarDto.Endereco);

                    _context.Update(usuarioEditarBanco);

                    await _context.SaveChangesAsync();

                    return usuarioEditarBanco;
                }

                return usuarioEditarBanco;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UsuarioModel> MudarSituacaoUsuario(int id)
        {
            try
            {
                var usuarioMudarSituacao = await _context.Usuarios.FirstOrDefaultAsync(usuario => usuario.Id == id);

                if (usuarioMudarSituacao.Situacao == true)
                {
                    usuarioMudarSituacao.Situacao = false;
                    usuarioMudarSituacao.DataAlteracao = DateTime.Now;  
                }else
                {
                    usuarioMudarSituacao.Situacao = true;
                    usuarioMudarSituacao.DataAlteracao = DateTime.Now;  
                }

                _context.Update(usuarioMudarSituacao);
                await _context.SaveChangesAsync();

                return usuarioMudarSituacao;
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
