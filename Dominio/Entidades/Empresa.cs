using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Threading;

namespace Dominio.Entidades
{
    public partial class Empresa
    {        
        public Empresa()
        {
            AcompanhamentoClinicos = new HashSet<AcompanhamentoClinico>();
            AgendaProfissionals = new HashSet<AgendaProfissional>();
            AgendaSessaoItems = new HashSet<AgendaSessaoItem>();
            AgendaSessaos = new HashSet<AgendaSessao>();
            AnamneseGrupos = new HashSet<AnamneseGrupo>();
            AnamneseRespostaClientes = new HashSet<AnamneseRespostaCliente>();
            AnamneseSubGrupoQuestaoOpcaos = new HashSet<AnamneseSubGrupoQuestaoOpcao>();
            AnamneseSubGrupoQuestaos = new HashSet<AnamneseSubGrupoQuestao>();
            AnamneseSubGrupos = new HashSet<AnamneseSubGrupo>();
            Auditoria = new HashSet<Auditoria>();
            Clientes = new HashSet<Cliente>();
            Convenios = new HashSet<Convenio>();
            DocumentoModeloEmpresaOpcaos = new HashSet<DocumentoModeloEmpresaOpcao>();
            DocumentoModeloEmpresas = new HashSet<DocumentoModeloEmpresa>();
            EmpresaAssinaturas = new HashSet<EmpresaAssinatura>();
            EmpresaFaturas = new HashSet<EmpresaFatura>();
            Filials = new HashSet<Filial>();
            FormularioSessaoCampos = new HashSet<FormularioSessaoCampo>();
            FormularioSessaos = new HashSet<FormularioSessao>();
            PacoteFechados = new HashSet<PacoteFechado>();
            ProfissionalAcessos = new HashSet<ProfissionalAcesso>();
            Salas = new HashSet<Sala>();
            Servicos = new HashSet<Servico>();
            Usuarios = new HashSet<Usuario>();
        }

        [Key]  // Define a chave primária
        public Guid EmpresaId { get; set; }
        public string RazaoSocial { get; set; } = null!;
        public string? NomeFantasia { get; set; }
        public byte[]? Logotipo { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }

        public virtual ICollection<AcompanhamentoClinico> AcompanhamentoClinicos { get; set; }
        public virtual ICollection<AgendaProfissional> AgendaProfissionals { get; set; }
        public virtual ICollection<AgendaSessaoItem> AgendaSessaoItems { get; set; }
        public virtual ICollection<AgendaSessao> AgendaSessaos { get; set; }
        public virtual ICollection<AnamneseGrupo> AnamneseGrupos { get; set; }
        public virtual ICollection<AnamneseRespostaCliente> AnamneseRespostaClientes { get; set; }
        public virtual ICollection<AnamneseSubGrupoQuestaoOpcao> AnamneseSubGrupoQuestaoOpcaos { get; set; }
        public virtual ICollection<AnamneseSubGrupoQuestao> AnamneseSubGrupoQuestaos { get; set; }
        public virtual ICollection<AnamneseSubGrupo> AnamneseSubGrupos { get; set; }
        public virtual ICollection<Auditoria> Auditoria { get; set; }
        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<Convenio> Convenios { get; set; }
        public virtual ICollection<DocumentoModeloEmpresaOpcao> DocumentoModeloEmpresaOpcaos { get; set; }
        public virtual ICollection<DocumentoModeloEmpresa> DocumentoModeloEmpresas { get; set; }
        public virtual ICollection<EmpresaAssinatura> EmpresaAssinaturas { get; set; }
        public virtual ICollection<EmpresaFatura> EmpresaFaturas { get; set; }
        public virtual ICollection<Filial> Filials { get; set; }
        public virtual ICollection<FormularioSessaoCampo> FormularioSessaoCampos { get; set; }
        public virtual ICollection<FormularioSessao> FormularioSessaos { get; set; }
        public virtual ICollection<PacoteFechado> PacoteFechados { get; set; }
        public virtual ICollection<ProfissionalAcesso> ProfissionalAcessos { get; set; }
        public virtual ICollection<Sala> Salas { get; set; }
        public virtual ICollection<Servico> Servicos { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }

        public static Empresa CriarParaImportacao(string razaoSocial, string nomeFantasia, byte[] logotipo, bool ativo)
        {
            var empresa = new Empresa
            {
                RazaoSocial = razaoSocial,
                NomeFantasia = nomeFantasia,
                Logotipo = logotipo,
                Ativo = ativo
            };
            return empresa;
        }

        public Empresa AtualizarPropriedades(
            string? razaoSocial=null,
            string? nomeFantasia = null,
            byte[]? logotipo = null,
            bool? ativo = true
        )
        {
            if (razaoSocial != null)
                RazaoSocial = razaoSocial;
            if (nomeFantasia != null)
                NomeFantasia = nomeFantasia;
            if (logotipo != null)
                Logotipo = logotipo;
            if (ativo != null)
                Ativo = ativo;           
            return this;
        }
    }
}
