using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Servicos.MultiTenant;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace API.Servicos.Usuarios
{
    public class UsuarioServico : ServicoBase, IUsuarioServico
    {
        private IConfiguration _configuration;
        private IUsuarioRepo _usuarioRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public UsuarioServico(
            IConfiguration configuration,
            IUsuarioRepo usuarioRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _usuarioRepo = usuarioRepo;
        }
        
        public async Task<Usuario>? BuscarPorID(Guid usuarioID) => await _usuarioRepo.BuscarPorID(usuarioID);

        public async Task<List<Usuario>> BuscarTodos()
        {
            return await _usuarioRepo.BuscarFiltros();
        }

        public async Task<List<Usuario>> BuscarPorEmail(BuscarComEmailParametro parametros)
        {
            return await _usuarioRepo.BuscarFiltros(x => x.Email.ToUpper().Contains(parametros.Email.ToUpper()));
        }

        public async Task<Usuario> Adicionar(Usuario usuario)
        {
            usuario.UsuarioId = Guid.NewGuid();
            await _usuarioRepo.Adicionar(usuario);
            await Comitar();
            return usuario;
        }

        public async Task<Usuario> Atualizar(Usuario usuario)
        {
            await _usuarioRepo.Atualizar(usuario);
            await Comitar();
            return usuario;
        }

        public async Task Deletar(Guid usuarioID)
        {
            var usuario = _usuarioRepo.BuscarPorID(usuarioID).Result;

            if (usuario == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Usuário não encontrado, verifique o identificador!");

            //usuario.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _usuarioRepo.Deletar(usuarioID);
            await Comitar();

            return;
        }

        //public async Task Salvar(Usuario usuario)
        //{
        //    await _usuarioRepo.Adicionar(usuario);
        //    await Comitar();
        //}

        //private async Task Atualizar(Usuario usuario)
        //{
        //    await _usuarioRepo.Atualizar(usuario);
        //    await Comitar();
        //}

        //public async Task<(bool criado, Guid usuarioId)> CriarOuAtualizar(CriarUsuarioInputModel usuario, bool atualizaSeExistir)
        //{
        //    var cUsuario = (await _usuarioRepo.Buscar(
        //        x => x.UsuarioId == usuario.UsuarioId
        //    )).FirstOrDefault();
        //    if (cUsuario == null)
        //    {
        //        cUsuario = Usuario.CriarParaImportacao(
        //            empresaID: usuario.EmpresaId,
        //            filialID: usuario.FilialId,
        //            email: usuario.Email,
        //            senhaHash: usuario.SenhaHash,
        //            trocaSenhaProximoAcesso: usuario.TrocaSenhaProximoAcesso,
        //            perfilAcesso: usuario.PerfilAcesso,
        //            ativo: usuario.Ativo
        //            );
        //        await Salvar(cUsuario);
        //        return (true, cUsuario.UsuarioId); // <-- retorno com o novo ID
        //    }
        //    else if (atualizaSeExistir)
        //    {
        //        cUsuario.AtualizarPropriedades(
        //            empresaID: usuario.EmpresaId,
        //            filialID: usuario.FilialId,
        //            email: usuario.Email,
        //            senhaHash: usuario.SenhaHash,
        //            trocaSenhaProximoAcesso: usuario.TrocaSenhaProximoAcesso,
        //            perfilAcesso: usuario.PerfilAcesso,
        //            ativo: usuario.Ativo
        //            );
        //        await _usuarioRepo.Atualizar(cUsuario);
        //        await Atualizar(cUsuario);

        //    }
        //    return (false, usuario.UsuarioId);
        //}

        //public async Task CriarParaImportacao(Guid usuarioID, Guid? empresaID, int? filialID, string email, string senhaHash, bool trocaSenhaProximoAcesso, string perfilAcesso, bool? ativo)
        //{
        //    var cUsuario = (await _usuarioRepo.Buscar(
        //                    x => x.UsuarioId == usuarioID)
        //                    ).FirstOrDefault();
        //    if (cUsuario == null)
        //    {
        //        cUsuario = Usuario.CriarParaImportacao(empresaID, filialID, email, senhaHash, trocaSenhaProximoAcesso, perfilAcesso, ativo);
        //        await Salvar(cUsuario);
        //    }
        //    return;
        //}

        //public async Task Validar(Guid usuarioID)
        //{
        //    var cUsuario = (await _usuarioRepo.Buscar(x => x.UsuarioId == usuarioID)).FirstOrDefault();
        //    if (cUsuario == null)
        //    {
        //        throw new HttpErroDeUsuario(
        //            HttpStatusCode.NotFound,
        //            $"Usuário com ID {usuarioID} não encontrado."
        //        );
        //    }
        //}
    }
}
