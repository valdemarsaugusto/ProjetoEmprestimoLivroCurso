using Microsoft.EntityFrameworkCore;
using ProjetoEmprestimoLivroCurso.Data;
using ProjetoEmprestimoLivroCurso.Models;
using ProjetoEmprestimoLivroCurso.Services.Livros;
using ProjetoEmprestimoLivroCurso.Services.Sessao;

namespace ProjetoEmprestimoLivroCurso.Services.Emprestimo
{
    public class EmprestimoService : IEmprestimoInterface
    {
        private readonly AppDbContent _context;
        private readonly ILivroInterface _livroInterface;
        private readonly ISessaoInterface _sessaoInterface;

        public EmprestimoService(AppDbContent context, ILivroInterface livroInterface, ISessaoInterface sessaoInterface)
        {
            _context = context;
            _livroInterface = livroInterface;
            _sessaoInterface = sessaoInterface;
        }
        public async Task<RespostaModel<EmprestimoModel>> Emprestar(int livroId)
        {
            RespostaModel<EmprestimoModel> resposta = new RespostaModel<EmprestimoModel>();

            try
            {
                var sessaoUsuario = _sessaoInterface.BuscarSessao();

                if (sessaoUsuario == null)
                {
                    resposta.Mensagem = "Você precisa estar logado para realizar um empréstimo.";
                    resposta.Status = false;
                    return resposta;
                }

                var livro = await _livroInterface.BuscarLivroPorId(livroId);

                var emprestimo = new EmprestimoModel
                {

                    UsuarioId = sessaoUsuario.Id,
                    LivroId = livroId,
                    //Usuario = sessaoUsuario
                    Livro = livro
                };

                _context.Add(emprestimo);
                await _context.SaveChangesAsync();

                var livroEstoque = await BaixarEstoque(livro);

                resposta.Dados = emprestimo;
                resposta.Mensagem = "Empréstimo realizado com sucesso!";
                resposta.Status = true;

                return resposta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task<RespostaModel<EmprestimoModel>> Devolver(int id)
        {
            RespostaModel<EmprestimoModel> resposta = new RespostaModel<EmprestimoModel>();

            try
            {
                var emprestimo = await _context.Emprestimos.Include(livro => livro.Livro)
                    .FirstOrDefaultAsync(emprestimo => emprestimo.Id == id);

                if (emprestimo == null)
                {
                    resposta.Mensagem = "Empréstimo não encontrado.";
                    resposta.Status = false;
                    return resposta;
                }

                emprestimo.DataDevolucao = DateTime.Now;
                
                _context.Update(emprestimo);
                await _context.SaveChangesAsync();

                var livroEstoque = await RetornarEstoque(emprestimo.Livro);

                resposta.Dados = emprestimo;
                resposta.Mensagem = "Livro devolvido com sucesso.";
                resposta.Status = true;
                return resposta;
            }
            catch  (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<LivroModel> BaixarEstoque(LivroModel livro)
        {
            try
            {
                livro.QuantidadeEmEstoque--;
                _context.Update(livro);
                await _context.SaveChangesAsync();
                return livro;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LivroModel> RetornarEstoque(LivroModel livro)
        {
            try
            {
                livro.QuantidadeEmEstoque++;
                _context.Update(livro);
                await _context.SaveChangesAsync();
                return livro;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<EmprestimoModel>> BuscarEmprestimosFiltro(UsuarioModel usuarioSessao, string pesquisar)
        {
            try
            {
                var emprestimosFiltro = await _context.Emprestimos
                        .Include(usuario => usuario.Usuario)
                        .Include(livro => livro.Livro)
                       .Where(emprestimo => emprestimo.UsuarioId == usuarioSessao.Id &&
                        (emprestimo.Livro.Titulo.Contains(pesquisar) || emprestimo.Livro.Autor.Contains(pesquisar)))
                    .ToListAsync();

                return emprestimosFiltro;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<List<EmprestimoModel>> BuscarEmprestimos(UsuarioModel usuarioSessao)
        {
            try
            {
                var usuarioEmprestimos = _context.Emprestimos
                        .Where(emprestimo => emprestimo.UsuarioId == usuarioSessao.Id)
                        .Include(livro => livro.Livro)
                        .Include(usuario => usuario.Usuario)
                    .ToListAsync();

                return usuarioEmprestimos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
