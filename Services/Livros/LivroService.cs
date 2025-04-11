using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProjetoEmprestimoLivroCurso.Data;
using ProjetoEmprestimoLivroCurso.Dto;
using ProjetoEmprestimoLivroCurso.Models;
using System.Threading.Tasks;

namespace ProjetoEmprestimoLivroCurso.Services.Livros
{
    public class LivroService : ILivroInterface
    {
        private readonly AppDbContent _context;
        private string _caminhoServidor;  
        private readonly IMapper _mapper;

        public LivroService(AppDbContent context, IWebHostEnvironment sistema, IMapper mapper)
        {
            _context = context;
            _caminhoServidor = sistema.WebRootPath;
            _mapper = mapper;
        }
        public async Task<List<LivroModel>> BuscarLivros()
        {
            try
            {
                var livros = await  _context.Livros.ToListAsync();
                return livros;    

            }catch (Exception ex)
            {
                throw new Exception(ex.Message); 
            }
        }

        public async Task<LivroModel> Cadastrar(LivroCriacaoDto livroCriacaoDto, IFormFile foto)
        {
            try
            {


                /*                var livro = new LivroModel
                                {
                                    Titulo = livroCriacaoDto.Titulo,
                                    Capa = nomeCaminhoDaImagem,
                                    Autor = livroCriacaoDto.Autor,
                                    Descricao = livroCriacaoDto.Descricao,
                                    QuantidadeEmEstoque = livroCriacaoDto.QuantidadeEmEstoque,
                                    AnoPublicacao = livroCriacaoDto.AnoPublicacao,
                                    ISBN   = livroCriacaoDto.ISBN,
                                    Genero = livroCriacaoDto.Genero
                                };*/

                var nomeCaminhoDaImagem = GeraCaminhoArquivo(foto);
                var livro = _mapper.Map<LivroModel>(livroCriacaoDto);
                livro.Capa = nomeCaminhoDaImagem;

                _context.Add(livro);    
                await _context.SaveChangesAsync();  

                return livro;

            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }



        }

        public bool VerifiqueSeJaExisteCadastro(LivroCriacaoDto livroCriacaoDto)
        {
            try
            {
                var livroBanco = _context.Livros.FirstOrDefault(livro => livro.ISBN == livroCriacaoDto.ISBN);

                if ( livroBanco != null)
                {
                    return false;
                }

                return true;    
            }
            catch (Exception ex) 
            {
                throw new Exception (ex.Message);
            }
        }

        public async Task<LivroModel> BuscarLivroPorId(int? id)
        {
            try
            {
                var livro = await _context.Livros.FirstOrDefaultAsync(livro => livro.Id == id);
                return livro;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GeraCaminhoArquivo(IFormFile foto)
        {
            var codigoUnico = Guid.NewGuid().ToString();
            var nomeCaminhoDaImagem = foto.FileName.Replace(" ", "").ToLower() + codigoUnico + ".png";

            string caminhoParaSalvarImagens = _caminhoServidor + "\\imagem\\";

            if (!Directory.Exists(caminhoParaSalvarImagens))
            {
                Directory.CreateDirectory(caminhoParaSalvarImagens);
            }

            using (var stream = System.IO.File.Create(caminhoParaSalvarImagens + nomeCaminhoDaImagem))
            {
                foto.CopyToAsync(stream).Wait();
            }

            return nomeCaminhoDaImagem;
        }

        public async Task<LivroModel> Editar(LivroEdicaoDto livroEdicaoDto, IFormFile? foto)
        {
            try
            {
                var livro = await _context.Livros.AsNoTracking().FirstOrDefaultAsync(l => l.Id == livroEdicaoDto.Id);
                var nomeCaminhoDaImagem = ""; 

                if (foto != null)
                {
                    string caminhoCapaExistente = _caminhoServidor + "\\imagem\\" +  livro.Capa;

                    if (File.Exists(caminhoCapaExistente))
                    {
                        File.Delete(caminhoCapaExistente);
                    }

                    nomeCaminhoDaImagem = GeraCaminhoArquivo(foto);
                    livro.Capa = nomeCaminhoDaImagem;
                }

                var livroModel = _mapper.Map<LivroModel>(livroEdicaoDto);

                if (nomeCaminhoDaImagem != "")
                {
                    livroModel.Capa = nomeCaminhoDaImagem;
                }
                else
                {
                    livroModel.Capa = livro.Capa;
                }

                    livroModel.DataDeAlteracao = DateTime.Now;

                _context.Update(livroModel);
                await _context.SaveChangesAsync();

                return livroModel;
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
