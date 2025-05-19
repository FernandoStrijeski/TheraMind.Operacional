using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Profissionais;
using Dominio.Repositorios;
using Infra.Servicos.MultiTenant;
using System.Net;

namespace API.Servicos.Profissionais
{
    public class ProfissionalServico : ServicoBase, IProfissionalServico
    {
        private IConfiguration _configuration;
        private IProfissionalRepo _profissionalRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public ProfissionalServico(
            IConfiguration configuration,
            IProfissionalRepo profissionalRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _profissionalRepo = profissionalRepo;
        }

        public async Task<Profissional>? BuscarPorID(Guid profissionalID) => await _profissionalRepo.BuscarPorID(profissionalID);

        public async Task<List<Profissional>> BuscarTodos()
        {
            return await _profissionalRepo.BuscarFiltros();
        }

        public async Task<List<Profissional>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _profissionalRepo.BuscarFiltros(x => x.NomeCompleto.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task Salvar(Profissional profissional)
        {
            await _profissionalRepo.Adicionar(profissional);
            await Comitar();
        }

        private async Task Atualizar(Profissional profissional)
        {
            await _profissionalRepo.Atualizar(profissional);
            await Comitar();
        }

        public async Task<(bool criado, Guid profissionalId)> CriarOuAtualizar(CriarProfissionalInputModel profissional, bool atualizaSeExistir)
        {
            var cProfissional = (await _profissionalRepo.Buscar(
                x => x.ProfissionalId == profissional.ProfissionalId
            )).FirstOrDefault();

            if (cProfissional == null)
            {
                cProfissional = Profissional.CriarParaImportacao(
                    tipoProfissional: profissional.TipoProfissional,
                    tipoPessoa: profissional.TipoPessoa,
                    nomeCompleto: profissional.NomeCompleto,
                    areaAtuacao: profissional.AreaAtuacao,
                    cpf: profissional.Cpf,
                    cnpj: profissional.Cnpj,
                    crp: profissional.Crp,
                    crfa: profissional.Crfa,
                    crefito: profissional.Crefito,
                    crm: profissional.Crm,
                    crn: profissional.Crn,
                    coffito: profissional.Coffito,
                    sexo: profissional.Sexo,
                    email: profissional.Email,
                    celular: profissional.Celular,
                    usuarioID: profissional.UsuarioID,
                    ativo: profissional.Ativo
                );
                await Salvar(cProfissional);
                return (true, cProfissional.ProfissionalId); // <-- retorno com o novo ID
            }
            else if (atualizaSeExistir)
            {
                cProfissional.AtualizarPropriedades(
                    tipoProfissional: profissional.TipoProfissional,
                    tipoPessoa: profissional.TipoPessoa,
                    nomeCompleto: profissional.NomeCompleto,
                    areaAtuacao: profissional.AreaAtuacao,
                    cpf: profissional.Cpf,
                    cnpj: profissional.Cnpj,
                    crp: profissional.Crp,
                    crfa: profissional.Crfa,
                    crefito: profissional.Crefito,
                    crm: profissional.Crm,
                    crn: profissional.Crn,
                    coffito: profissional.Coffito,
                    sexo: profissional.Sexo,
                    email: profissional.Email,
                    celular: profissional.Celular,
                    usuarioID: profissional.UsuarioID,
                    ativo: profissional.Ativo
                );
                await _profissionalRepo.Atualizar(cProfissional);
                await Atualizar(cProfissional);
            }

            return (false, profissional.ProfissionalId);
        }


        public async Task CriarParaImportacao(Guid profissionalID, string tipoProfissional, string tipoPessoa, string nomeCompleto, string? areaAtuacao, string? cpf, string? cnpj,
                            string? crp, string? crfa, string? crefito, string? crm, string? crn, string? coffito, string sexo, string email, string celular, Guid? usuarioID, bool? ativo)
        {
            var cProfissional = (await _profissionalRepo.Buscar(
                            x => x.ProfissionalId == profissionalID)
                            ).FirstOrDefault();
            if (cProfissional == null)
            {
                cProfissional = Profissional.CriarParaImportacao(tipoProfissional, tipoPessoa, nomeCompleto, areaAtuacao, cpf, cnpj, crp, crfa, crefito, crm, crn, coffito, sexo, email, celular, usuarioID, ativo);
                await Salvar(cProfissional);
            }
            return;
        }

        public async Task Validar(Guid profissionalID)
        {
            var cProfissional = (await _profissionalRepo.Buscar(x => x.ProfissionalId == profissionalID)).FirstOrDefault();
            if (cProfissional == null)
            {
                throw new HttpErroDeUsuario(
                    HttpStatusCode.NotFound,
                    $"Profissional com ID {profissionalID} não encontrado."
                );
            }
        }
    }
}
