using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Repositorios;
using Infra.Servicos.MultiTenant;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace API.Servicos.Salas
{
    public class SalaServico : ServicoBase, ISalaServico
    {
        private IConfiguration _configuration;
        private ISalaRepo _salaRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public SalaServico(
            IConfiguration configuration,
            ISalaRepo salaRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _salaRepo = salaRepo;
        }
        
        public async Task<Sala>? BuscarPorID(string salaID) => await _salaRepo.BuscarPorID(salaID);

        public async Task<List<Sala>> BuscarTodos()
        {
            return await _salaRepo.BuscarFiltros();
        }

        public async Task<List<Sala>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _salaRepo.BuscarFiltros(x => x.Nome.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task<Sala> Adicionar(Sala sala)
        {
            await _salaRepo.Adicionar(sala);
            await Comitar();
            return sala;
        }

        public async Task<Sala> Atualizar(Sala sala)
        {
            await _salaRepo.Atualizar(sala);
            await Comitar();
            return sala;
        }

        public async Task Deletar(string salaID)
        {
            var sala = _salaRepo.BuscarPorID(salaID).Result;

            if (sala == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Sala não encontrada, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _salaRepo.Deletar(salaID);
            await Comitar();

            return;
        }
    }
}
