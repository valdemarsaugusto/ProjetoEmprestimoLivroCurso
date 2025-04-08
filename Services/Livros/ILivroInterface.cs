using ProjetoEmprestimoLivroCurso.Dto;
using ProjetoEmprestimoLivroCurso.Models;

namespace ProjetoEmprestimoLivroCurso.Services.Livros
{
    public interface ILivroInterface
    {
        Task<List<LivroModel>> BuscarLivros();
        bool VerifiqueSeJaExisteCadastro(LivroCriacaoDto livroCriacaoDto);

        Task<LivroModel> Cadastrar(LivroCriacaoDto livroCriacaoDto, IFormFile foto);
        Task<LivroModel> BuscarLivroPorId(int? id);
        Task<LivroModel> Editar(LivroEdicaoDto livroEdicaoDto, IFormFile? foto);
    }
}
