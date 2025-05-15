using AutoMapper;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Newtonsoft.Json;
using ProjetoEmprestimoLivroCurso.Dto.Relatorio;
using ProjetoEmprestimoLivroCurso.Enums;
using System.Data;
using System.Runtime.ConstrainedExecution;

namespace ProjetoEmprestimoLivroCurso.Services.Relatorio
{
    public class RelatorioService : IRelatorioInterface
    {
        private readonly IMapper _mapper;

        public RelatorioService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public DataTable ColetarDados<T>(List<T> dados, int idRelatorio)
        {
            DataTable dataTable = new DataTable();
            dataTable.TableName = Enum.GetName(typeof(RelatorioEnum), idRelatorio);

            var colunas = dados.First().GetType().GetProperties();

            foreach (var coluna in colunas)
            {
                dataTable.Columns.Add(coluna.Name, coluna.PropertyType);
            }

            switch (idRelatorio)
            {
                case 1:
                    var dadosLivro = JsonConvert.SerializeObject(dados);
                    var dadosLivroModel = JsonConvert.DeserializeObject<List<LivroRelatorioDto>>(dadosLivro);

                    if (dadosLivroModel != null)
                    {
                        return ExportarLivro(dataTable, dadosLivroModel);
                    }

                    break;
                case 2:
                    var dadosCliente = JsonConvert.SerializeObject(dados);
                    var dadosClienteModel = JsonConvert.DeserializeObject<List<UsuarioRelatorioDto>>(dadosCliente);

                    if (dadosClienteModel != null)
                    {
                        return ExportarUsuario(dataTable, dadosClienteModel);
                    }

                    break;
                case 3:
                    var dadosFuncionario = JsonConvert.SerializeObject(dados);
                    var dadosFuncionarioModel = JsonConvert.DeserializeObject<List<UsuarioRelatorioDto>>(dadosFuncionario);

                    if (dadosFuncionarioModel != null)
                    {
                        return ExportarUsuario(dataTable, dadosFuncionarioModel);
                    }

                    break;
                case 4:
                    var dadosEmprestimo = JsonConvert.SerializeObject(dados);
                    var dadosEmprestimoModel = JsonConvert.DeserializeObject<List<EmprestimoRelatorioDto>>(dadosEmprestimo);

                    if (dadosEmprestimoModel != null)
                    {
                        return ExportarEmprestimo(dataTable, dadosEmprestimoModel);
                    }
                    break;
                case 5:
                    var dadosEmprestimoPendente = JsonConvert.SerializeObject(dados);
                    var dadosEmprestimoPendenteModel = JsonConvert.DeserializeObject<List<EmprestimoRelatorioDto>>(dadosEmprestimoPendente);

                    if (dadosEmprestimoPendenteModel != null)
                    {
                        return ExportarEmprestimoPendentes(dataTable, dadosEmprestimoPendenteModel);
                    }
                    break;
            }

            return dataTable;
        }

        public DataTable ExportarLivro(DataTable data, List<LivroRelatorioDto> dados)
        {
            foreach (var dado in dados)
            {
                data.Rows.Add(dado.Id, dado.Titulo, dado.Descricao, dado.Capa, dado.ISBN, dado.Autor, dado.Genero, dado.AnoPublicacao, dado.QuantidadeEmEstoque, dado.DataDeCadastro, dado.DataDeAlteracao);

            }
            return data;
        }

        public DataTable ExportarUsuario(DataTable data, List<UsuarioRelatorioDto> dados)
        {
            foreach (var dado in dados)
            {
                data.Rows.Add(dado.Id, dado.NomeCompleto, dado.Usuario, dado.Email, dado.Situacao, dado.Perfil,
                    dado.Turno, dado.Logradouro, dado.Bairro, dado.Numero, dado.Cidade,
                    dado.Estado, dado.Cep, dado.Complemento, dado.DataCadastro, dado.DataAlteracao);
            }
            return data;
        }

        public DataTable ExportarEmprestimo(DataTable data, List<EmprestimoRelatorioDto> dados)
        {
            foreach (var dado in dados)
            {
                data.Rows.Add(dado.Id, dado.UsuarioId, dado.NomeCompleto, dado.Usuario, dado.LivroId, dado.Titulo, 
                    dado.ISBN, dado.DataEmprestimo, dado.DataDevolucao);
            }
            return data;

        }
        public DataTable ExportarEmprestimoPendentes(DataTable data, List<EmprestimoRelatorioDto> dados)
        {
            foreach (var dado in dados)
            {
                data.Rows.Add(dado.Id, dado.UsuarioId, dado.NomeCompleto, dado.Usuario, dado.LivroId, dado.Titulo,
                    dado.ISBN, dado.DataEmprestimo);
            }
            return data;

        }
    }
}
