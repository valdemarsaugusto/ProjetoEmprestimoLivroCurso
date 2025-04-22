using Microsoft.AspNetCore.Mvc;
using ProjetoEmprestimoLivroCurso.Models;
using ProjetoEmprestimoLivroCurso.Services.Sessao;

namespace ProjetoEmprestimoLivroCurso.ViewComponents
{
    public class Menu : ViewComponent
    {
        private readonly ISessaoInterface _sessaoInterface;

        public Menu(ISessaoInterface sessaoInterface)
        {
            _sessaoInterface = sessaoInterface;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            UsuarioModel sessaoUsuario = _sessaoInterface.BuscarSessao();

            if (sessaoUsuario == null) return View();

            return View(sessaoUsuario);

        }
    }
}
