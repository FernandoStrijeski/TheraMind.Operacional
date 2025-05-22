using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.Convenios;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Profissionais;
using Dominio.Repositorios;
using Infra.Servicos.MultiTenant;
using System.Net;

namespace API.Servicos.Convenios
{
    public class ConvenioServico : ServicoBase, IConvenioServico
    {
        private IConfiguration _configuration;
        private IConvenioRepo _convenioRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public ConvenioServico(
            IConfiguration configuration,
            IConvenioRepo convenioRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _convenioRepo = convenioRepo;
        }

        public async Task<Convenio>? BuscarPorID(int convenioID) => await _convenioRepo.BuscarPorID(convenioID);

        public async Task<List<Convenio>> BuscarTodos()
        {
            return await _convenioRepo.BuscarFiltros();
        }

        public async Task<List<Convenio>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _convenioRepo.BuscarFiltros(x => x.Nome.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task Salvar(Convenio convenio)
        {
            await _convenioRepo.Adicionar(convenio);
            await Comitar();
        }

        private async Task Atualizar(Convenio convenio)
        {
            await _convenioRepo.Atualizar(convenio);
            await Comitar();
        }

        public async Task<(bool criado, int convenioId)> CriarOuAtualizar(CriarConvenioInputModel convenio, bool atualizaSeExistir)
        {
            var cConvenio = (await _convenioRepo.Buscar(
                x => x.ConvenioId == convenio.ConvenioId
            )).FirstOrDefault();

            if (cConvenio == null)
            {
                cConvenio = Convenio.CriarParaImportacao(
                    empresaID: convenio.EmpresaId,
                    filialID: convenio.FilialId,
                    nome: convenio.Nome,
                    tipoRepasse: convenio.TipoRepasse,
                    valorRepasse: convenio.ValorRepasse,
                    ativo: convenio.Ativo
                );
                await Salvar(cConvenio);
                return (true, cConvenio.ConvenioId); // <-- retorno com o novo ID
            }
            else if (atualizaSeExistir)
            {
                cConvenio.AtualizarPropriedades(
                    empresaID: convenio.EmpresaId,
                    filialID: convenio.FilialId,
                    nome: convenio.Nome,
                    tipoRepasse: convenio.TipoRepasse,
                    valorRepasse: convenio.ValorRepasse,
                    ativo: convenio.Ativo
                );
                await _convenioRepo.Atualizar(cConvenio);
                await Atualizar(cConvenio);
            }

            return (false, convenio.ConvenioId);
        }


        public async Task CriarParaImportacao(int convenioID, Guid empresaID, int filialID, string nome, short tipoRepasse, decimal valorRepasse, bool? ativo)
        {
            var cConvenio = (await _convenioRepo.Buscar(
                            x => x.ConvenioId == convenioID)
                            ).FirstOrDefault();
            if (cConvenio == null)
            {
                cConvenio = Convenio.CriarParaImportacao(empresaID, filialID, nome, tipoRepasse, valorRepasse, ativo);
                await Salvar(cConvenio);
            }
            return;
        }

        public async Task Validar(int convenioID)
        {
            var cConvenio = (await _convenioRepo.Buscar(x => x.ConvenioId == convenioID)).FirstOrDefault();
            if (cConvenio == null)
            {
                throw new HttpErroDeUsuario(
                    HttpStatusCode.NotFound,
                    $"Convênio com ID {convenioID} não encontrado."
                );
            }
        }
    }
}
