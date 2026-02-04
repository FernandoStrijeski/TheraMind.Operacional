using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.AnamneseSubGrupos;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Infra.Servicos.MultiTenant;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace API.Servicos.AnamneseSubGrupos
{
    public class AnamneseSubGrupoServico : ServicoBase, IAnamneseSubGrupoServico
    {
        private IConfiguration _configuration;
        private IAnamneseSubGrupoRepo _anamneseSubGrupoRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public AnamneseSubGrupoServico(
            IConfiguration configuration,
            IAnamneseSubGrupoRepo anamneseSubGrupoRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _anamneseSubGrupoRepo = anamneseSubGrupoRepo;
        }

        public async Task<AnamneseSubGrupo>? BuscarPorID(int anamneseSubGrupoID) => await _anamneseSubGrupoRepo.BuscarPorID(anamneseSubGrupoID);

        public async Task<List<AnamneseSubGrupo>> BuscarTodos()
        {
            return await _anamneseSubGrupoRepo.BuscarFiltros();
        }

        public async Task<List<AnamneseSubGrupo>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _anamneseSubGrupoRepo.BuscarFiltros(x => x.Titulo.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task<AnamneseSubGrupo> Adicionar(AnamneseSubGrupo anamneseSubGrupo)
        {
            await _anamneseSubGrupoRepo.Adicionar(anamneseSubGrupo);
            await Comitar();
            return anamneseSubGrupo;
        }

        public async Task<AnamneseSubGrupo> Atualizar(AnamneseSubGrupo anamneseSubGrupo)
        {
            await _anamneseSubGrupoRepo.Atualizar(anamneseSubGrupo);
            await Comitar();
            return anamneseSubGrupo;
        }

        public async Task Deletar(int anamneseSubGrupoID)
        {
            var anamneseSubGrupo = _anamneseSubGrupoRepo.BuscarPorID(anamneseSubGrupoID).Result;

            if (anamneseSubGrupo == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Subgrupo de amanmnese não encontrado, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _anamneseSubGrupoRepo.Deletar(anamneseSubGrupoID);
            await Comitar();

            return;
        }
    }
}
