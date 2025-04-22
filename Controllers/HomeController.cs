using Microsoft.AspNetCore.Mvc;
using ProjetoEmprestimoLivroCurso.Dto.Home;
using ProjetoEmprestimoLivroCurso.Services.Home;
using ProjetoEmprestimoLivroCurso.Services.Livros;
using ProjetoEmprestimoLivroCurso.Services.Sessao;
using System.Threading.Tasks;

namespace ProjetoEmprestimoLivroCurso.Controllers;

public class HomeController : Controller
{
    private readonly ISessaoInterface _sessaoInterface;
    private readonly IHomeInterface _homeInterface;
    private readonly ILivroInterface _livroInterface;

    public HomeController(ISessaoInterface sessaoInterface, IHomeInterface homeInterface, ILivroInterface livroInterface)
    {
        _sessaoInterface = sessaoInterface;
        _homeInterface = homeInterface;
        _livroInterface = livroInterface;
    }
    [HttpGet]
    public async Task<ActionResult> Index(string pesquisar = null)
    {
        var usuarioSessao = _sessaoInterface.BuscarSessao();

        if (usuarioSessao != null)
        {
            ViewBag.LayoutPagina = "_Layout";
        }
        else
        {
            ViewBag.LayoutPagina = "_LayoutDeslogada";
        }

        if (pesquisar == null)
        {
            var livrosBanco = await _livroInterface.BuscarLivros();
            return View(livrosBanco);
        }else
        {
            var livrosBanco = await _livroInterface.BuscarLivrosFiltro(pesquisar);
            return View(livrosBanco);   
        }
    }
    [HttpGet]
    public ActionResult Login()
    {
        if(_sessaoInterface.BuscarSessao() != null)
        {
            return RedirectToAction("Index","Home");
        } 
        
        return View();

    }

    [HttpGet]
    public ActionResult Sair()
    {
        _sessaoInterface.RemoverSessao();
        TempData["MensagemSucesso"] = "Logout realizado com sucesso!";
        return RedirectToAction("Login", "Home");
    }

    [HttpPost]
    public async Task<ActionResult> Login(LoginDto loginDto) 
    { 
        if(ModelState.IsValid)
        {
            var login = await _homeInterface.RealizarLogin(loginDto);

            if (login.Status == false)
            {
                TempData["MensagemErro"] = login.Mensagem;
                return View(login.Dados);
            }
            if (login.Dados.Situacao == false) 
            {
                TempData["MensagemErro"] = "Procure o suporte para verificar o status da sua conta.";
                return View("Login");
            }

            _sessaoInterface.CriarSessao(login.Dados);
            TempData["MensagemSucesso"] = "Login realizado com sucesso!";


            return RedirectToAction("Index", "Home");
        }else
        {
            return View(loginDto);
        }

    }

}
