using API.Core.Exceptions;
using API.modelos.InputModels;
using Dominio.Core.Repositorios;
using Dominio.EmpresasAssinaturas;
using Dominio.Entidades;
using Infra.Servicos.MultiTenant;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace API.Servicos.EmpresasAssinaturas
{
    public class EmpresaAssinaturaServico : ServicoBase, IEmpresaAssinaturaServico
    {
        private IConfiguration _configuration;
        private IEmpresaAssinaturaRepo _empresaAssinaturaRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public EmpresaAssinaturaServico(
            IConfiguration configuration,
            IEmpresaAssinaturaRepo empresaAssinaturaRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _empresaAssinaturaRepo = empresaAssinaturaRepo;
        }

        public async Task<EmpresaAssinatura>? BuscarPorID(Guid empresaAssinaturaID) => await _empresaAssinaturaRepo.BuscarPorID(empresaAssinaturaID);

        public async Task<List<EmpresaAssinatura>> BuscarTodos()
        {
            return await _empresaAssinaturaRepo.BuscarFiltros();
        }

        public async Task<List<EmpresaAssinatura>> BuscarPorIdEmpresa(Guid empresaID) => await _empresaAssinaturaRepo.BuscarPorIdEmpresa(empresaID);


        public async Task<EmpresaAssinatura> Adicionar(EmpresaAssinatura empresaAssinatura)
        {
            await _empresaAssinaturaRepo.Adicionar(empresaAssinatura);
            await Comitar();
            return empresaAssinatura;
        }

        public async Task<EmpresaAssinatura> Atualizar(EmpresaAssinatura empresaAssinatura)
        {
            await _empresaAssinaturaRepo.Atualizar(empresaAssinatura);
            await Comitar();
            return empresaAssinatura;
        }

        public async Task Deletar(Guid empresaAssinaturaID)
        {
            var empresaAssinatura = _empresaAssinaturaRepo.BuscarPorID(empresaAssinaturaID).Result;

            if (empresaAssinatura == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Assinatura da empresa não encontrada, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _empresaAssinaturaRepo.Deletar(empresaAssinaturaID);
            await Comitar();

            return;
        }
    }
}
