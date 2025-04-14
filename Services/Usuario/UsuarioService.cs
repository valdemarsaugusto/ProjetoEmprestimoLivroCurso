using Microsoft.EntityFrameworkCore;
using ProjetoEmprestimoLivroCurso.Data;
using ProjetoEmprestimoLivroCurso.Dto.Usuario;
using ProjetoEmprestimoLivroCurso.Models;

namespace ProjetoEmprestimoLivroCurso.Services.Usuario
{
    public class UsuarioService : IUsuarioInterface
    {
        private readonly AppDbContent _context;

        public UsuarioService(AppDbContent context)
        {
            _context = context;
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
