using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dominio.Entidades
{
    public partial class Filial
    {
        public Filial()
        {
            AcompanhamentoClinicos = new HashSet<AcompanhamentoClinico>();
            AgendaProfissionals = new HashSet<AgendaProfissional>();
            AgendaSessaoItens = new HashSet<AgendaSessaoItem>();
            AgendaSessoes = new HashSet<AgendaSessao>();
            AnamneseGrupos = new HashSet<AnamneseGrupo>();
            AnamneseRespostaClientes = new HashSet<AnamneseRespostaCliente>();
            AnamneseSubGrupoQuestaoOpcoes = new HashSet<AnamneseSubGrupoQuestaoOpcao>();
            AnamneseSubGrupoQuestaos = new HashSet<AnamneseSubGrupoQuestao>();
            AnamneseSubGrupos = new HashSet<AnamneseSubGrupo>();
            Auditoria = new HashSet<Auditoria>();
            Clientes = new HashSet<Cliente>();
            Convenios = new HashSet<Convenio>();
            DocumentoModeloEmpresaOpcoes = new HashSet<DocumentoModeloEmpresaOpcao>();
            DocumentoModeloEmpresas = new HashSet<DocumentoModeloEmpresa>();
            FormularioSessaoCampos = new HashSet<FormularioSessaoCampo>();
            FormularioSessoes = new HashSet<FormularioSessao>();
            PacoteFechados = new HashSet<PacoteFechado>();
            ProfissionalAcessos = new HashSet<ProfissionalAcesso>();
            Salas = new HashSet<Sala>();
            Servicos = new HashSet<Servico>();
            Usuarios = new HashSet<Usuario>();
        }

        [Key]
        public int FilialId { get; set; }
        public Guid EmpresaId { get; set; }
        public string? Cpf { get; set; }
        public string? Cnpj { get; set; }
        public string? InscricaoEstadual { get; set; }
        public string? InscricaoMunicipal { get; set; }
        public string? NomeFilial { get; set; }
        public string? TipoLogradouroId { get; set; }
        public string? Endereco { get; set; }
        public short? Numero { get; set; }
        public string? Cep { get; set; }
        public string? Complemento { get; set; }
        public string? Bairro { get; set; }
        public int CidadeId { get; set; }
        public string? Telefone { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }

        public virtual Cidade Cidade { get; set; } = null!;
        public virtual Empresa Empresa { get; set; } = null!;
        public virtual TipoLogradouro? TipoLogradouro { get; set; }
        public virtual ICollection<AcompanhamentoClinico> AcompanhamentoClinicos { get; set; }
        public virtual ICollection<AgendaProfissional> AgendaProfissionals { get; set; }
        public virtual ICollection<AgendaSessaoItem> AgendaSessaoItens { get; set; }
        public virtual ICollection<AgendaSessao> AgendaSessoes { get; set; }
        public virtual ICollection<AnamneseGrupo> AnamneseGrupos { get; set; }
        public virtual ICollection<AnamneseRespostaCliente> AnamneseRespostaClientes { get; set; }
        public virtual ICollection<AnamneseSubGrupoQuestaoOpcao> AnamneseSubGrupoQuestaoOpcoes { get; set; }
        public virtual ICollection<AnamneseSubGrupoQuestao> AnamneseSubGrupoQuestaos { get; set; }
        public virtual ICollection<AnamneseSubGrupo> AnamneseSubGrupos { get; set; }
        public virtual ICollection<Auditoria> Auditoria { get; set; }
        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<Convenio> Convenios { get; set; }
        public virtual ICollection<DocumentoModeloEmpresaOpcao> DocumentoModeloEmpresaOpcoes { get; set; }
        public virtual ICollection<DocumentoModeloEmpresa> DocumentoModeloEmpresas { get; set; }
        public virtual ICollection<FormularioSessaoCampo> FormularioSessaoCampos { get; set; }
        public virtual ICollection<FormularioSessao> FormularioSessoes { get; set; }
        public virtual ICollection<PacoteFechado> PacoteFechados { get; set; }
        public virtual ICollection<ProfissionalAcesso> ProfissionalAcessos { get; set; }
        public virtual ICollection<Sala> Salas { get; set; }
        public virtual ICollection<Servico> Servicos { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }

        public static Filial CriarParaImportacao(Guid empresaId, string? cpf, string? cnpj, string? inscricaoEstadual, string? inscricaoMunicipal, string nomeFilial,
                string? tipoLogradouroId, string? endereco, short? numero, string? cep, string? complemento, string? bairro, int cidadeId, string? telefone, bool ativo)
        {
            var filial = new Filial
            {
                EmpresaId = empresaId,
                Cpf = cpf,
                Cnpj = cnpj,
                InscricaoEstadual = inscricaoEstadual,
                InscricaoMunicipal = inscricaoMunicipal,
                NomeFilial = nomeFilial,
                TipoLogradouroId = tipoLogradouroId,
                Endereco = endereco,
                Numero = numero,
                Cep = cep,
                Complemento = complemento,
                Bairro = bairro,
                CidadeId = cidadeId,
                Telefone = telefone,
                Ativo = ativo
            };
            return filial;
        }

        public Filial AtualizarPropriedades(
            string? cpf, string? cnpj, string? inscricaoEstadual, string? inscricaoMunicipal, string nomeFilial,
                string? tipoLogradouroId, string? endereco, short? numero, string? cep, string? complemento, string? bairro, int cidadeId, string? telefone, bool ativo
        )
        {
            if (Cpf != null)
                Cpf = cpf;

            if (Cnpj != null)
                Cnpj = cnpj;

            if (InscricaoEstadual != null)
                InscricaoEstadual = inscricaoEstadual;

            if (InscricaoEstadual != null)
                InscricaoEstadual = inscricaoEstadual;

            if (NomeFilial != null)
                NomeFilial = nomeFilial;

            if (TipoLogradouroId != null)
                TipoLogradouroId = tipoLogradouroId;

            if (Endereco != null)
                Endereco = endereco;

            if (Numero != null)
                Numero = numero;

            if (Cep != null)
                Cep = cep;

            if (Complemento != null)
                Complemento = complemento;

            if (Bairro != null)
                Bairro = bairro;

            CidadeId = cidadeId;

            if (Telefone != null)
                Telefone = telefone;

            Ativo = ativo;
            return this;
        }
    }
}
