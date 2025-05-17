using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Servicos.MultiTenant;
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

        public async Task Salvar(Sala sala)
        {
            await _salaRepo.Adicionar(sala);
            await Comitar();
        }

        private async Task Atualizar(Sala sala)
        {
            await _salaRepo.Atualizar(sala);
            await Comitar();
        }

        public async Task<(bool criado, string salaId)> CriarOuAtualizar(CriarSalaInputModel sala, bool atualizaSeExistir)
        {
            var cSala = (await _salaRepo.Buscar(
                x => x.SalaId == sala.SalaId
            )).FirstOrDefault();
            if (cSala == null)
            {
                cSala = Sala.CriarParaImportacao(
                    empresaID: sala.EmpresaId,
                    filialID: sala.FilialId,
                    nome: sala.Nome,
                    ativo: sala.Ativo
                    );
                await Salvar(cSala);
                return (true, cSala.SalaId); // <-- retorno com o novo ID
            }
            else if (atualizaSeExistir)
            {
                cSala.Nome = sala.Nome;
                cSala.AtualizarPropriedades(
                    empresaID: sala.EmpresaId,
                    filialID: sala.FilialId,
                    nome: sala.Nome,
                    ativo: sala.Ativo
                    );
                await _salaRepo.Atualizar(cSala);
                await Atualizar(cSala);
            }
            return (false, cSala.SalaId); // <-- retorno com o novo ID
        }

        public async Task CriarParaImportacao(string salaID, Guid empresaID, int filialID, string nome, bool? ativo)
        {
            var cSala = (await _salaRepo.Buscar(
                            x => x.SalaId == salaID)
                            ).FirstOrDefault();
            if (cSala == null)
            {
                cSala = Sala.CriarParaImportacao(empresaID, filialID, nome, ativo);
                await Salvar(cSala);
            }
            return;
        }

        public async Task Validar(string salaID)
        {
            var cSala = (await _salaRepo.Buscar(x => x.SalaId== salaID)).FirstOrDefault();
            if (cSala == null)
            {
                throw new HttpErroDeUsuario(
                    HttpStatusCode.NotFound,
                    $"Sala com ID {salaID} não encontrado."
                );
            }
        }
    }
}
