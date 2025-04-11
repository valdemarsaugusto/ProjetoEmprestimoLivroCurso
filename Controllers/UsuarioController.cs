using Microsoft.AspNetCore.Mvc;
using ProjetoEmprestimoLivroCurso.Services.Usuario;
using System.Threading.Tasks;

namespace ProjetoEmprestimoLivroCurso.Controllers
{
    public class UsuarioController : Controller
    {
        public IUsuarioInterface _usuarioInterface;
        public UsuarioController(IUsuarioInterface usuarioInterface)
        {
            _usuarioInterface = usuarioInterface;
        }

        public async Task<IActionResult> Index(int? id)
        {
            var usuarios = await _usuarioInterface.BuscarUsuarios(id);
            return View(usuarios);
        }
    }
}
