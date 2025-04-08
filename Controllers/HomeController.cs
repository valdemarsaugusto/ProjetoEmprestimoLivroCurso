using Microsoft.AspNetCore.Mvc;

namespace ProjetoEmprestimoLivroCurso.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
