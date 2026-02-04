using API.Core.Exceptions;
using API.modelos;
using Dominio.AnamneseGrupos;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Infra.Servicos.MultiTenant;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace API.Servicos.AnamneseGrupos
{
    public class AnamneseGrupoServico : ServicoBase, IAnamneseGrupoServico
    {
        private IConfiguration _configuration;
        private IAnamneseGrupoRepo _anamneseGrupoRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public AnamneseGrupoServico(
            IConfiguration configuration,
            IAnamneseGrupoRepo anamneseGrupoRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _anamneseGrupoRepo = anamneseGrupoRepo;
        }

        public async Task<AnamneseGrupo>? BuscarPorID(int anamneseGrupoID) => await _anamneseGrupoRepo.BuscarPorID(anamneseGrupoID);

        public async Task<List<AnamneseGrupo>> BuscarTodos()
        {
            return await _anamneseGrupoRepo.BuscarFiltros();
        }

        public async Task<List<AnamneseGrupo>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _anamneseGrupoRepo.BuscarFiltros(x => x.Titulo.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task<AnamneseGrupo> Adicionar(AnamneseGrupo anamneseGrupo)
        {
            await _anamneseGrupoRepo.Adicionar(anamneseGrupo);
            await Comitar();
            return anamneseGrupo;
        }

        public async Task<AnamneseGrupo> Atualizar(AnamneseGrupo anamneseGrupo)
        {
            await _anamneseGrupoRepo.Atualizar(anamneseGrupo);
            await Comitar();
            return anamneseGrupo;
        }

        public async Task Deletar(int anamneseGrupoID)
        {
            var anamneseGrupo = _anamneseGrupoRepo.BuscarPorID(anamneseGrupoID).Result;

            if (anamneseGrupo == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Grupo de amanmese não encontrado, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _anamneseGrupoRepo.Deletar(anamneseGrupoID);
            await Comitar();

            return;
        }
    }
}
