using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjetoEmprestimoLivroCurso.Dto.Endereco;
using ProjetoEmprestimoLivroCurso.Dto.Usuario;
using ProjetoEmprestimoLivroCurso.Enums;
using ProjetoEmprestimoLivroCurso.Filtros;
using ProjetoEmprestimoLivroCurso.Models;
using ProjetoEmprestimoLivroCurso.Services.Usuario;
using System.Threading.Tasks;

namespace ProjetoEmprestimoLivroCurso.Controllers
{

    public class UsuarioController : Controller
    {
        public IUsuarioInterface _usuarioInterface;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioInterface usuarioInterface, IMapper mapper)
        {
            _usuarioInterface = usuarioInterface;
            _mapper = mapper;
        }
        [UsuarioLogado]
        [UsuarioLogadoCliente]
        public async Task<IActionResult> Index(int? id)
        {
            var usuarios = await _usuarioInterface.BuscarUsuarios(id);
            return View(usuarios);
        }

        [HttpGet]
        public ActionResult Cadastrar(int? id)
        {
            ViewBag.Perfil = PerfilEnum.Administrador;
            ViewBag.Id = id;

            if (id != null)
            {
                ViewBag.Perfil = PerfilEnum.Cliente;
            }

            return View();
        }

        [UsuarioLogado]
        [UsuarioLogadoCliente]
        [HttpGet]
        public async Task<ActionResult> Detalhes(int? id)
        {
            if (id != null)
            {
                var usuario = await _usuarioInterface.BuscarUsuarioPorId(id);
                return View(usuario);
            }

            return RedirectToAction("Index");
        }

        [UsuarioLogado]
        [UsuarioLogadoCliente]
        [HttpGet]
        public async Task<ActionResult> Editar(int? id)
        {
            if (id != null)
            {
                var usuario = await _usuarioInterface.BuscarUsuarioPorId(id);

                var usuarioEditado = new UsuarioEditarDto
                {
                    NomeCompleto = usuario.NomeCompleto,
                    Email = usuario.Email,
                    Perfil = usuario.Perfil,
                    Turno = usuario.Turno,
                    Id = usuario.Id,
                    Usuario = usuario.Usuario,
                    Endereco = _mapper.Map<EnderecoEditarDto>(usuario.Endereco)
                };
                if (usuarioEditado.Perfil == PerfilEnum.Cliente)
                {
                    ViewBag.Perfil = PerfilEnum.Cliente;
                }
                else
                {
                    ViewBag.Perfil = PerfilEnum.Administrador;
                }

                return View(usuarioEditado);
            }

            return RedirectToAction("Index");
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

            if (usuario.Perfil != PerfilEnum.Cliente)
            {
                return RedirectToAction("Index", "Funcionario");
            }
            else
            {
                return RedirectToAction("Index", "Cliente", new { Id = 0 });
            }
        }

        [UsuarioLogado]
        [UsuarioLogadoCliente]
        [HttpPost]
        public async Task<ActionResult> MudarSituacaoUsuario(UsuarioModel usuario)
        {
            if (usuario.Id != 0 && usuario.Id != null)
            {
                var usuarioBanco = await _usuarioInterface.MudarSituacaoUsuario(usuario.Id);

                if (usuarioBanco.Situacao == true)
                {
                    TempData["MensagemSucesso"] = "Usuário ativado com sucesso.";
                }
                else
                {
                    TempData["MensagemSucesso"] = "Usuário desativado com sucesso.";
                }

                if (usuarioBanco.Perfil != PerfilEnum.Cliente)
                {
                    return RedirectToAction("Index", "Funcionario");
                }
                else
                {
                    return RedirectToAction("Index", "Cliente", new { Id = 0 });
                }
            }
            else
            {
                TempData["MensagemErro"] = "Erro ao alterar a situação do usuário.";
                return RedirectToAction("Index");
            }
        }

        [UsuarioLogado]
        [UsuarioLogadoCliente]
        [HttpPost]
        public async Task<ActionResult> Editar(UsuarioEditarDto usuarioEditarDto)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _usuarioInterface.Editar(usuarioEditarDto);
                TempData["MensagemSucesso"] = "Usuário editado com sucesso.";
            }
            else
            {
                TempData["MensagemErro"] = "Verifique os dados informados.";
            }

            if (usuarioEditarDto.Perfil != PerfilEnum.Cliente)
            {
                return RedirectToAction("Index", "Funcionario");
            }
            else
            {
                return RedirectToAction("Index", "Cliente", new { Id = 0 });
            }
        }
    }
}
