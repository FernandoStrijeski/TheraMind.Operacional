using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using BoletoNetCore;
using Dominio.Clientes;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Infra.Servicos.MultiTenant;
using System.Net;
using System.Runtime.ConstrainedExecution;

namespace API.Servicos.Clientes
{
    public class ClienteServico : ServicoBase, IClienteServico
    {
        private IConfiguration _configuration;
        private IClienteRepo _clienteRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public ClienteServico(
            IConfiguration configuration,
            IClienteRepo clienteRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _clienteRepo = clienteRepo;
        }

        public async Task<Cliente>? BuscarPorID(Guid clienteID) => await _clienteRepo.BuscarPorID(clienteID);

        public async Task<List<Cliente>> BuscarTodos()
        {
            return await _clienteRepo.BuscarFiltros();
        }

        public async Task<List<Cliente>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _clienteRepo.BuscarFiltros(x => x.NomeCompleto.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task Salvar(Cliente cliente)
        {
            await _clienteRepo.Adicionar(cliente);
            await Comitar();
        }

        private async Task Atualizar(Cliente cliente)
        {
            await _clienteRepo.Atualizar(cliente);
            await Comitar();
        }

        public async Task<(bool criado, Guid clienteId)> CriarOuAtualizar(CriarClienteInputModel cliente, bool atualizaSeExistir)
        {
            var cCliente = (await _clienteRepo.Buscar(
                x => x.ClienteId == cliente.ClienteId
            )).FirstOrDefault();

            if (cCliente == null)
            {
                cCliente = Cliente.CriarParaImportacao(
                    empresaId: cliente.EmpresaId,
                    filialId: cliente.FilialId,
                    nomeCompleto: cliente.NomeCompleto,
                    nomeSocial: cliente.NomeSocial,
                    dataNascimento: cliente.DataNascimento,
                    cpf: cliente.Cpf,
                    rg: cliente.Rg,
                    email: cliente.Email,
                    celular: cliente.Celular,
                    sexo: cliente.Sexo,
                    identidadeGeneroId: cliente.IdentidadeGeneroId,
                    orientacaoSexualId: cliente.OrientacaoSexualId,
                    estadoCivilId: cliente.EstadoCivilId,
                    escolaridadeId: cliente.EscolaridadeId,
                    tipoEtniaId: cliente.TipoEtniaId,
                    profissao: cliente.Profissao,
                    deficiencia: cliente.Deficiencia,
                    naturalizado: cliente.Naturalizado,
                    dataNaturalizacao: cliente.DataNaturalizacao,
                    nomeResponsavel: cliente.NomeResponsavel,
                    celularResponsavel: cliente.CelularResponsavel,
                    emailResponsavel: cliente.EmailResponsavel,
                    cpfresponsavel: cliente.Cpfresponsavel,
                    rgresponsavel: cliente.Rgresponsavel,
                    dataNascimentoResponsavel: cliente.DataNascimentoResponsavel,
                    parenteNome: cliente.ParenteNome,
                    parenteCelular: cliente.ParenteCelular,
                    parenteGrauParentescoId: cliente.ParenteGrauParentescoId,
                    tipoLogradouroId: cliente.TipoLogradouroId,
                    endereco: cliente.Endereco,
                    numero: cliente.Numero,
                    cep: cliente.Cep,
                    complemento: cliente.Complemento,
                    bairro: cliente.Bairro,
                    cidadeId: cliente.CidadeId,
                    uf: cliente.Uf,
                    paisId: cliente.PaisId,
                    nacionalidadeId: cliente.NacionalidadeId,
                    planoPagamento: cliente.PlanoPagamento,
                    convenioId: cliente.ConvenioId,
                    convenioValorRepasse: cliente.ConvenioValorRepasse,
                    pacoteFechadoId: cliente.PacoteFechadoId,
                    planoMensalInicio: cliente.PlanoMensalInicio,
                    planoMensalDataVencimento: cliente.PlanoMensalDataVencimento,
                    pacoteFechadoNroSessoes: cliente.PacoteFechadoNroSessoes,
                    pacoteFechadoDataInicio: cliente.PacoteFechadoDataInicio,
                    pacoteFechadoDataVencimento: cliente.PacoteFechadoDataVencimento,
                    valorPagamento: cliente.ValorPagamento,
                    formaPagamento: cliente.FormaPagamento,
                    situacao: cliente.Situacao,
                    motivoDesativacao: cliente.MotivoDesativacao,
                    usuarioID: cliente.UsuarioID
                );
                await Salvar(cCliente);
                return (true, cCliente.ClienteId); // <-- retorno com o novo ID
            }
            else if (atualizaSeExistir)
            {
                cCliente.AtualizarPropriedades(
                    empresaId: cliente.EmpresaId,
                    filialId: cliente.FilialId,
                    nomeCompleto: cliente.NomeCompleto,
                    nomeSocial: cliente.NomeSocial,
                    dataNascimento: cliente.DataNascimento,
                    cpf: cliente.Cpf,
                    rg: cliente.Rg,
                    email: cliente.Email,
                    celular: cliente.Celular,
                    sexo: cliente.Sexo,
                    identidadeGeneroId: cliente.IdentidadeGeneroId,
                    orientacaoSexualId: cliente.OrientacaoSexualId,
                    estadoCivilId: cliente.EstadoCivilId,
                    escolaridadeId: cliente.EscolaridadeId,
                    tipoEtniaId: cliente.TipoEtniaId,
                    profissao: cliente.Profissao,
                    deficiencia: cliente.Deficiencia,
                    naturalizado: cliente.Naturalizado,
                    dataNaturalizacao: cliente.DataNaturalizacao,
                    nomeResponsavel: cliente.NomeResponsavel,
                    celularResponsavel: cliente.CelularResponsavel,
                    emailResponsavel: cliente.EmailResponsavel,
                    cpfresponsavel: cliente.Cpfresponsavel,
                    rgresponsavel: cliente.Rgresponsavel,
                    dataNascimentoResponsavel: cliente.DataNascimentoResponsavel,
                    parenteNome: cliente.ParenteNome,
                    parenteCelular: cliente.ParenteCelular,
                    parenteGrauParentescoId: cliente.ParenteGrauParentescoId,
                    tipoLogradouroId: cliente.TipoLogradouroId,
                    endereco: cliente.Endereco,
                    numero: cliente.Numero,
                    cep: cliente.Cep,
                    complemento: cliente.Complemento,
                    bairro: cliente.Bairro,
                    cidadeId: cliente.CidadeId,
                    uf: cliente.Uf,
                    paisId: cliente.PaisId,
                    nacionalidadeId: cliente.NacionalidadeId,
                    planoPagamento: cliente.PlanoPagamento,
                    convenioId: cliente.ConvenioId,
                    convenioValorRepasse: cliente.ConvenioValorRepasse,
                    pacoteFechadoId: cliente.PacoteFechadoId,
                    planoMensalInicio: cliente.PlanoMensalInicio,
                    planoMensalDataVencimento: cliente.PlanoMensalDataVencimento,
                    pacoteFechadoNroSessoes: cliente.PacoteFechadoNroSessoes,
                    pacoteFechadoDataInicio: cliente.PacoteFechadoDataInicio,
                    pacoteFechadoDataVencimento: cliente.PacoteFechadoDataVencimento,
                    valorPagamento: cliente.ValorPagamento,
                    formaPagamento: cliente.FormaPagamento,
                    situacao: cliente.Situacao,
                    motivoDesativacao: cliente.MotivoDesativacao,
                    usuarioID: cliente.UsuarioID
                );
                await _clienteRepo.Atualizar(cCliente);
                await Atualizar(cCliente);
            }

            return (false, cliente.ClienteId);
        }


        public async Task CriarParaImportacao(Guid clienteID, Guid empresaId, int filialId, string nomeCompleto, string? nomeSocial, DateTime dataNascimento,
                                              string cpf, string? rg, string email, string celular, string sexo, int? identidadeGeneroId,
                                              int? orientacaoSexualId, string? estadoCivilId, int? escolaridadeId, int? tipoEtniaId, string? profissao, bool? deficiencia, bool? naturalizado,
                                              DateTime? dataNaturalizacao, string? nomeResponsavel, string? celularResponsavel, string? emailResponsavel, string? cpfresponsavel, string? rgresponsavel,
                                              DateTime? dataNascimentoResponsavel, string? parenteNome, string? parenteCelular, int? parenteGrauParentescoId, string? tipoLogradouroId,
                                              string? endereco, int? numero, string? cep, string? complemento, string? bairro, int? cidadeId, string? uf, int? paisId,
                                              int? nacionalidadeId, short? planoPagamento, int? convenioId, decimal? convenioValorRepasse, int? pacoteFechadoId, short? planoMensalInicio,
                                              DateTime? planoMensalDataVencimento, short? pacoteFechadoNroSessoes, DateTime? pacoteFechadoDataInicio, DateTime? pacoteFechadoDataVencimento,
                                              decimal? valorPagamento, short? formaPagamento, short situacao, string? motivoDesativacao, Guid? usuarioID)
        {
            var cCliente = (await _clienteRepo.Buscar(
                            x => x.ClienteId == clienteID)
                            ).FirstOrDefault();
            if (cCliente == null)
            {
                cCliente = Cliente.CriarParaImportacao(empresaId, filialId, nomeCompleto, nomeSocial, dataNascimento,
                                                        cpf, rg, email, celular, sexo, identidadeGeneroId,
                                                        orientacaoSexualId, estadoCivilId, escolaridadeId, tipoEtniaId, profissao, deficiencia, naturalizado,
                                                        dataNaturalizacao, nomeResponsavel, celularResponsavel, emailResponsavel, cpfresponsavel, rgresponsavel,
                                                        dataNascimentoResponsavel, parenteNome, parenteCelular, parenteGrauParentescoId, tipoLogradouroId,
                                                        endereco, numero, cep, complemento, bairro, cidadeId, uf, paisId,
                                                        nacionalidadeId, planoPagamento, convenioId, convenioValorRepasse, pacoteFechadoId, planoMensalInicio,
                                                        planoMensalDataVencimento, pacoteFechadoNroSessoes, pacoteFechadoDataInicio, pacoteFechadoDataVencimento,
                                                        valorPagamento, formaPagamento, situacao, motivoDesativacao, usuarioID);
                await Salvar(cCliente);
            }
            return;
        }

        public async Task Validar(Guid clienteID)
        {
            var cConvenio = (await _clienteRepo.Buscar(x => x.ClienteId == clienteID)).FirstOrDefault();
            if (cConvenio == null)
            {
                throw new HttpErroDeUsuario(
                    HttpStatusCode.NotFound,
                    $"Cliente com ID {clienteID} não encontrado."
                );
            }
        }
    }
}
