using API.Core.Exceptions;
using API.modelos.InputModels;
using Dominio.Core.Repositorios;
using Dominio.EmpresaFaturas;
using Dominio.Entidades;
using Infra.Servicos.MultiTenant;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace API.Servicos.EmpresaFaturas
{
    public class EmpresaFaturaServico : ServicoBase, IEmpresaFaturaServico
    {
        private IConfiguration _configuration;
        private IEmpresaFaturaRepo _empresaFaturaRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public EmpresaFaturaServico(
            IConfiguration configuration,
            IEmpresaFaturaRepo empresaFaturaRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _empresaFaturaRepo = empresaFaturaRepo;
        }

        public async Task<EmpresaFatura>? BuscarPorID(int empresaFaturaID) => await _empresaFaturaRepo.BuscarPorID(empresaFaturaID);

        public async Task<List<EmpresaFatura>> BuscarTodos()
        {
            return await _empresaFaturaRepo.BuscarFiltros();
        }


        public async Task<EmpresaFatura> Adicionar(EmpresaFatura empresaFatura)
        {
            await _empresaFaturaRepo.Adicionar(empresaFatura);
            await Comitar();
            return empresaFatura;
        }

        public async Task<EmpresaFatura> Atualizar(EmpresaFatura empresaFatura)
        {
            await _empresaFaturaRepo.Atualizar(empresaFatura);
            await Comitar();
            return empresaFatura;
        }

        public async Task Deletar(int empresaFaturaID)
        {
            var empresaFatura = _empresaFaturaRepo.BuscarPorID(empresaFaturaID).Result;

            if (empresaFatura == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Fatura da empresa não encontrada, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _empresaFaturaRepo.Deletar(empresaFaturaID);
            await Comitar();

            return;
        }
    }
}
