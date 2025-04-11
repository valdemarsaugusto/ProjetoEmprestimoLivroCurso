using Microsoft.AspNetCore.Mvc;
using ProjetoEmprestimoLivroCurso.Services.Usuario;

namespace ProjetoEmprestimoLivroCurso.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IUsuarioInterface _usuarioInterface;

        public ClienteController(IUsuarioInterface usuarioInterface)
        {
            _usuarioInterface = usuarioInterface;
        }
        public async Task<ActionResult> Index(int? id)
        {

            var clientes = await _usuarioInterface.BuscarUsuarios(id);
            return View(clientes);
        }
    }
}
