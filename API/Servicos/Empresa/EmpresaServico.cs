using API.Core.Exceptions;
using API.Core.Utils;
using API.modelos.InputModels;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Repositorios;
using Infra.Servicos.MultiTenant;
using Operacional.Core.Utils.Class;
using System.Net;

namespace API.Servicos.Empresas
{
    public class EmpresaServico : ServicoBase, IEmpresaServico
    {
        private IConfiguration _configuration;
        private IEmpresaRepo _empresaRepo;
        private IConnectionParamsServico _connectionParamsServico;
        private IAuditoriaRepo _auditoriaRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmpresaServico(
            IConfiguration configuration,
            IEmpresaRepo empresaRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork,
            IAuditoriaRepo auditoriaRepo,
            IHttpContextAccessor httpContextAccessor
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _empresaRepo = empresaRepo;
            _auditoriaRepo = auditoriaRepo;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Dominio.Entidades.Empresa>? BuscarPorID(Guid empresaID) => await _empresaRepo.BuscarPorID(empresaID);

        public async Task<List<Dominio.Entidades.Empresa>> BuscarTodos()
        {
            return await _empresaRepo.BuscarFiltros();
        }

        public async Task<Dominio.Entidades.Empresa> Adicionar(Dominio.Entidades.Empresa empresa)
        {
            await _empresaRepo.Adicionar(empresa);
            await Comitar();
            return empresa;
        }

        public async Task<Dominio.Entidades.Empresa> Atualizar(Dominio.Entidades.Empresa empresa)
        {
            await _empresaRepo.Atualizar(empresa);
            await Comitar();
            return empresa;
        }

        public async Task Deletar(Guid empresaID)
        {
            var empresa = _empresaRepo.BuscarPorID(empresaID).Result;

            if (empresa == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Empresa não encontrada, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _empresaRepo.Deletar(empresaID);
            await Comitar();

            return;
        }
    }
}
