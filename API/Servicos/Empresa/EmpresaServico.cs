using API.Core.Exceptions;
using API.modelos.InputModels;
using Dominio.Core.Repositorios;
using Dominio.Repositorios;
using Infra.Servicos.MultiTenant;
using System.Net;

namespace API.Servicos.Empresas
{
    public class EmpresaServico : ServicoBase, IEmpresaServico
    {
        private IConfiguration _configuration;
        private IEmpresaRepo _empresaRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public EmpresaServico(
            IConfiguration configuration,
            IEmpresaRepo empresaRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _empresaRepo = empresaRepo;
        }

        public async Task<Dominio.Entidades.Empresa>? BuscarPorID(Guid empresaID) => await _empresaRepo.BuscarPorID(empresaID);


        public async Task Salvar(Dominio.Entidades.Empresa empresa)
        {
            await _empresaRepo.Adicionar(empresa);
            await Comitar();
        }

        private async Task Atualizar(Dominio.Entidades.Empresa empresa)
        {
            await _empresaRepo.Atualizar(empresa);
            await Comitar();
        }

        public async Task<bool> CriarOuAtualizar(CriarEmpresaInputModel empresa, bool atualizaSeExistir)
        {
            var cEmpresa = (await _empresaRepo.Buscar(
                x => x.EmpresaId == empresa.EmpresaID
            )).FirstOrDefault();
            if (cEmpresa == null)
            {
                cEmpresa = Dominio.Entidades.Empresa.CriarParaImportacao(razaoSocial: empresa.RazaoSocial, nomeFantasia: empresa.NomeFantasia, logotipo: empresa.Logotipo, ativo: empresa.Ativo);
                await Salvar(cEmpresa);
                return true;
            }
            else if (atualizaSeExistir)
            {
                cEmpresa.RazaoSocial = empresa.RazaoSocial;
                cEmpresa.NomeFantasia = empresa.NomeFantasia;
                cEmpresa.Logotipo = empresa.Logotipo;
                cEmpresa.AtualizarPropriedades(razaoSocial: empresa.RazaoSocial, nomeFantasia: empresa.NomeFantasia, logotipo: empresa.Logotipo, ativo: empresa.Ativo);
                await _empresaRepo.Atualizar(cEmpresa);
                await Atualizar(cEmpresa);

            }
            return false;
        }

        public async Task CriarParaImportacao(Guid empresaID, string razaoSocial, string nomeFantasia, byte[] logotipo, bool ativo)
        {
            var cEmpresa = (await _empresaRepo.Buscar(
                            x => x.EmpresaId == empresaID)
                            ).FirstOrDefault();
            if (cEmpresa == null)
            {
                cEmpresa = Dominio.Entidades.Empresa.CriarParaImportacao(razaoSocial, nomeFantasia, logotipo, ativo);
                await Salvar(cEmpresa);
            }
            return;
        }

        public async Task Validar(Guid empresaID)
        {
            var cEmpresa = (await _empresaRepo.Buscar(x => x.EmpresaId == empresaID)).FirstOrDefault();
            if (cEmpresa == null)
            {
                throw new HttpErroDeUsuario(
                    HttpStatusCode.NotFound,
                    $"Empresa com ID {empresaID} não encontrada."
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
