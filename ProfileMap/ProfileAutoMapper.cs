using AutoMapper;
using ProjetoEmprestimoLivroCurso.Dto.Livro;
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

        
        }
    }
}
