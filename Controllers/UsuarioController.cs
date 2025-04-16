using Microsoft.AspNetCore.Mvc;
using ProjetoEmprestimoLivroCurso.Dto.Usuario;
using ProjetoEmprestimoLivroCurso.Enums;
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

        [HttpGet]
        public ActionResult Cadastrar(int? id)
        {
            ViewBag.Perfil = PerfilEnum.Administrador;

            if (id != null)
            {
                ViewBag.Perfil = PerfilEnum.Cliente;
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Cadastrar(UsuarioCriacaoDto usuarioCriacaoDto)
        {
            if (ModelState.IsValid)
            {
                if (!await _usuarioInterface.VerificaSeExisteUsuarioEEmail(usuarioCriacaoDto))
                {
                    TempData["MensagemErro"] = "Já existe email/usuário cadastrado.";
                    return View(usuarioCriacaoDto);
                }
            }
            else 
            {   
                TempData["MensagemErro"] = "Verifique os dados informados.";
                return View(usuarioCriacaoDto);
            }

            var usuario = await _usuarioInterface.Cadastrar(usuarioCriacaoDto);
            TempData["MensagemSucesso"] = "Cadastro realiazado com sucesso.";

            if(usuario.Perfil != PerfilEnum.Cliente)
            {
                return RedirectToAction("Index", "Funcionario");
            }
            else
            {
                return RedirectToAction("Index", "Cliente", new {Id = 0});
            }
        }
    }
}
