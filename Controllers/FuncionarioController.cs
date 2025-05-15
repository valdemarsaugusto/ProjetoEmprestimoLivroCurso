using Microsoft.AspNetCore.Mvc;
using ProjetoEmprestimoLivroCurso.Filtros;
using ProjetoEmprestimoLivroCurso.Services.Usuario;

namespace ProjetoEmprestimoLivroCurso.Controllers
{
    [UsuarioLogado]
    [UsuarioLogadoCliente]
    public class FuncionarioController : Controller
    {
        public IUsuarioInterface _usuarioInterface;
        public FuncionarioController(IUsuarioInterface usuarioInterface)
        {
            _usuarioInterface = usuarioInterface;
        }

       public async Task<ActionResult> Index()
        {
            var funcionarios = await _usuarioInterface.BuscarUsuarios(null);
            return View(funcionarios);
        }
    }
}
