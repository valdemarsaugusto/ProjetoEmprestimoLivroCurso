﻿using ProjetoEmprestimoLivroCurso.Dto.Livro;
using ProjetoEmprestimoLivroCurso.Models;

namespace ProjetoEmprestimoLivroCurso.Services.Livros
{
    public interface ILivroInterface
    {
        Task<List<LivroModel>> BuscarLivros();
        bool VerifiqueSeJaExisteCadastro(LivroCriacaoDto livroCriacaoDto);

        Task<LivroModel> Cadastrar(LivroCriacaoDto livroCriacaoDto, IFormFile foto);
        Task<LivroModel> BuscarLivroPorId(int? id);
        Task<EmprestimoModel> BuscarLivroPorId(int? id, UsuarioModel usuarioSessao);
        Task<LivroModel> Editar(LivroEdicaoDto livroEdicaoDto, IFormFile? foto);
        Task<List<LivroModel>> BuscarLivrosFiltro(string pesquisar);
    }
}
