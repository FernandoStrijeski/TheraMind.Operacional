using API.Core.Exceptions;
using API.modelos.InputModels;
using Dominio.Core.Repositorios;
using Dominio.Repositorios;
using Infra.Servicos.MultiTenant;
using System.Net;

namespace API.Servicos.Filiais
{
    public class FilialServico : ServicoBase, IFilialServico
    {
        private IConfiguration _configuration;
        private IFilialRepo _filialRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public FilialServico(
            IConfiguration configuration,
            IFilialRepo filialRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _filialRepo = filialRepo;
        }

        public async Task<Dominio.Entidades.Filial>? BuscarPorID(Guid empresaID, int filialID) => await _filialRepo.BuscarPorID(empresaID, filialID);


        public async Task Salvar(Dominio.Entidades.Filial filial)
        {
            await _filialRepo.Adicionar(filial);
            await Comitar();
        }

        private async Task Atualizar(Dominio.Entidades.Filial filial)
        {
            await _filialRepo.Atualizar(filial);
            await Comitar();
        }

        public async Task<bool> CriarOuAtualizar(CriarFilialInputModel filial, bool atualizaSeExistir)
        {
            var cFilial = (await _filialRepo.Buscar(
                x => x.EmpresaId == filial.EmpresaId && x.FilialId == filial.FilialId
            )).FirstOrDefault();
            if (cFilial == null)
            {
                cFilial = Dominio.Entidades.Filial.CriarParaImportacao(
                        empresaId: filial.EmpresaId,
                        cpf: filial.Cpf,
                        cnpj: filial.Cnpj,
                        inscricaoEstadual: filial.InscricaoEstadual,
                        inscricaoMunicipal: filial.InscricaoMunicipal,
                        nomeFilial: filial.NomeFilial,
                        tipoLogradouroId: filial.TipoLogradouroId,
                        endereco: filial.Endereco,
                        numero: filial.Numero,
                        cep: filial.Cep,
                        complemento: filial.Complemento,
                        bairro: filial.Bairro,
                        cidadeId: filial.CidadeId,
                        telefone: filial.Telefone,
                        ativo: filial.Ativo
                    );
                await Salvar(cFilial);
                return true;
            }
            else if (atualizaSeExistir)
            {
                cFilial.Cpf = filial.Cpf;
                cFilial.Cnpj = filial.Cnpj;
                cFilial.InscricaoEstadual = filial.InscricaoEstadual;
                cFilial.InscricaoEstadual = filial.InscricaoEstadual;
                cFilial.NomeFilial = filial.NomeFilial;
                cFilial.TipoLogradouroId = filial.TipoLogradouroId;
                cFilial.Endereco = filial.Endereco;
                cFilial.Numero = filial.Numero;
                cFilial.Cep = filial.Cep;
                cFilial.Complemento = filial.Complemento;
                cFilial.Bairro = filial.Bairro;
                cFilial.CidadeId = filial.CidadeId;
                cFilial.Telefone = filial.Telefone;
                cFilial.Ativo = filial.Ativo;

                cFilial.AtualizarPropriedades(
                            cpf: filial.Cpf,
                            cnpj: filial.Cnpj,
                            inscricaoEstadual: filial.InscricaoEstadual,
                            inscricaoMunicipal: filial.InscricaoMunicipal,
                            nomeFilial: filial.NomeFilial,
                            tipoLogradouroId: filial.TipoLogradouroId,
                            endereco: filial.Endereco,
                            numero: filial.Numero,
                            cep: filial.Cep,
                            complemento: filial.Complemento,
                            bairro: filial.Bairro,
                            cidadeId: filial.CidadeId,
                            telefone: filial.Telefone,
                            ativo: filial.Ativo
                        );
                await _filialRepo.Atualizar(cFilial);
                await Atualizar(cFilial);

            }
            return false;
        }

        public async Task CriarParaImportacao(int filialId, Guid empresaId, string? cpf, string? cnpj, string? inscricaoEstadual, string? inscricaoMunicipal, string nomeFilial,
                string? tipoLogradouroId, string? endereco, short? numero, string? cep, string? complemento, string? bairro, int cidadeId, string? telefone, bool ativo)
        {
            var cFilial = (await _filialRepo.Buscar(
                            x => x.EmpresaId == empresaId && x.FilialId == filialId)
                            ).FirstOrDefault();
            if (cFilial == null)
            {
                cFilial = Dominio.Entidades.Filial.CriarParaImportacao(empresaId, cpf, cnpj, inscricaoEstadual, inscricaoMunicipal, nomeFilial,
                tipoLogradouroId, endereco, numero, cep, complemento, bairro, cidadeId, telefone, ativo);
                await Salvar(cFilial);
            }
            return;
        }

        public async Task Validar(Guid empresaID, int filialID)
        {
            var cFilial = (await _filialRepo.Buscar(x => x.EmpresaId == empresaID && x.FilialId == filialID)).FirstOrDefault();
            if (cFilial == null)
            {
                throw new HttpErroDeUsuario(
                    HttpStatusCode.NotFound,
                    $"Filial com ID {filialID} não encontrada na empresa."
                );
            }
        }

        //public async Task<List<Empresas>> Buscar(BuscarComCodEmpresaParametro parametros)
        //{
        //    var query = await _empresasRepo.Buscar(x => x.CodEmpresa == parametros.CodEmpresa);
        //    return query.ToList();
        //}

    }
}
