using Microsoft.AspNetCore.Mvc;
using ProjetoEmprestimoLivroCurso.Services.Emprestimo;
using ProjetoEmprestimoLivroCurso.Services.Sessao;
using ProjetoEmprestimoLivroCurso.Services.Usuario;

namespace ProjetoEmprestimoLivroCurso.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IUsuarioInterface _usuarioInterface;
        private readonly ISessaoInterface _sessaoInterface;
        private readonly IEmprestimoInterface _emprestimoInterface;

        public ClienteController(IUsuarioInterface usuarioInterface, ISessaoInterface sessaoInterface, IEmprestimoInterface emprestimoInterface)
        {
            _usuarioInterface = usuarioInterface;
            _sessaoInterface = sessaoInterface;
            _emprestimoInterface = emprestimoInterface;
        }
        public async Task<ActionResult> Index(int? id)
        {

            var clientes = await _usuarioInterface.BuscarUsuarios(id);
            return View(clientes);
        }

        public async Task<ActionResult> Perfil(string pesquisar = null, string filtro = "NDevolvidos")
        {
            var sessaoUsuario = _sessaoInterface.BuscarSessao();    
            if (sessaoUsuario == null) 
            {
                return RedirectToAction("Login", "Home");
            }

            ViewBag.Filtro = filtro;

            if (pesquisar != null)
            {
                var emprestimosFiltrado = await _emprestimoInterface.BuscarEmprestimosFiltro(sessaoUsuario, pesquisar);

                return View(emprestimosFiltrado);
            }

            var emprestimosUsuario = await _emprestimoInterface.BuscarEmprestimos(sessaoUsuario);

            return View(emprestimosUsuario);

        }   
    }
}
