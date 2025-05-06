using Microsoft.AspNetCore.Mvc;
using ProjetoEmprestimoLivroCurso.Services.Emprestimo;
using ProjetoEmprestimoLivroCurso.Services.Livros;
using ProjetoEmprestimoLivroCurso.Services.Sessao;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ProjetoEmprestimoLivroCurso.Controllers
{
    public class EmprestimoController : Controller
    {
        private readonly ISessaoInterface _sessaoInterface;
        private readonly ILivroInterface _livroInterface;
        private readonly IEmprestimoInterface _emprestimoInterface;

        public EmprestimoController(ISessaoInterface sessaoInterface, ILivroInterface livroInterface, IEmprestimoInterface emprestimoInterface)
        {
            _sessaoInterface = sessaoInterface;
            _livroInterface = livroInterface;
            _emprestimoInterface = emprestimoInterface;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Emprestar(int id)
        {
            var usuarioSessao = _sessaoInterface.BuscarSessao();

            if (usuarioSessao == null)
            {
                TempData["MensagemErro"] = "Você precisa estar logado para realizar um empréstimo.";
                return RedirectToAction("Login", "Home");
            }

            var emprestimo = await _emprestimoInterface.Emprestar(id);

            TempData["MensagemSucesso"] = emprestimo.Mensagem;
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<ActionResult> Devolver(int Id)
        {
            var usuarioSessao = _sessaoInterface.BuscarSessao();

            if (usuarioSessao == null)
            {
                TempData["MensagemErro"] = "Você precisa estar logado para realizar um empréstimo.";
                return RedirectToAction("Login", "Home");
            }

            var devolver = await _emprestimoInterface.Devolver(Id);

            if (devolver.Status == false)
            {
                TempData["MensagemErro"] = devolver.Mensagem;
                return RedirectToAction("Index", "Home");
            }else
            {
                TempData["MensagemSucesso"] = devolver.Mensagem;
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
