using AutoMapper;
using ProjetoEmprestimoLivroCurso.Dto.Endereco;
using ProjetoEmprestimoLivroCurso.Dto.Livro;
using ProjetoEmprestimoLivroCurso.Dto.Relatorio;
using ProjetoEmprestimoLivroCurso.Models;

namespace ProjetoEmprestimoLivroCurso.ProfileMap
{
    public class ProfileAutoMapper : Profile
    {
        public ProfileAutoMapper()
        {
            CreateMap<LivroCriacaoDto, LivroModel>();
            CreateMap<LivroModel, LivroEdicaoDto>();
            CreateMap<LivroEdicaoDto, LivroModel>();
            CreateMap<EnderecoModel, EnderecoEditarDto>();
            CreateMap<EnderecoEditarDto, EnderecoModel>();
            CreateMap<LivroModel, LivroRelatorioDto>();
            CreateMap<UsuarioModel, UsuarioRelatorioDto>(); 
            CreateMap<EmprestimoModel, EmprestimoRelatorioDto>();   

        }
    }
}
