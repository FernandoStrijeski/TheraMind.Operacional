using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    public partial class Profissional
    {
        public Profissional()
        {
            AcompanhamentoClinicos = new HashSet<AcompanhamentoClinico>();
            AgendaProfissionals = new HashSet<AgendaProfissional>();
            AgendaSessaoItens = new HashSet<AgendaSessaoItem>();
            AgendaSessoes = new HashSet<AgendaSessao>();
            AnamneseRespostaClientes = new HashSet<AnamneseRespostaCliente>();
            AnamneseSubGrupoQuestaoOpcoes = new HashSet<AnamneseSubGrupoQuestaoOpcao>();
            AnamneseSubGrupoQuestoes = new HashSet<AnamneseSubGrupoQuestao>();
            AnamneseSubGrupos = new HashSet<AnamneseSubGrupo>();
            ProfissionalAcessos = new HashSet<ProfissionalAcesso>();
        }

        [Key]
        public Guid ProfissionalId { get; set; }
        public string TipoProfissional { get; set; } = null!;
        public string TipoPessoa { get; set; } = null!;
        public string NomeCompleto { get; set; } = null!;
        public string? AreaAtuacao { get; set; }
        public string? Cpf { get; set; }
        public string? Cnpj { get; set; }
        public string? Crp { get; set; }
        public string? Crfa { get; set; }
        public string? Crefito { get; set; }
        public string? Crm { get; set; }
        public string? Crn { get; set; }
        public string? Coffito { get; set; }
        public string Sexo { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Celular { get; set; } = null!;
        public Guid? UsuarioID { get; set; } = null!;
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }

        [ForeignKey(nameof(UsuarioID))]
        public virtual Usuario? Usuario { get; set; }

        public virtual ICollection<AcompanhamentoClinico> AcompanhamentoClinicos { get; set; }
        public virtual ICollection<AgendaProfissional> AgendaProfissionals { get; set; }
        public virtual ICollection<AgendaSessaoItem> AgendaSessaoItens { get; set; }
        public virtual ICollection<AgendaSessao> AgendaSessoes { get; set; }
        public virtual ICollection<AnamneseRespostaCliente> AnamneseRespostaClientes { get; set; }
        public virtual ICollection<AnamneseSubGrupoQuestaoOpcao> AnamneseSubGrupoQuestaoOpcoes { get; set; }
        public virtual ICollection<AnamneseSubGrupoQuestao> AnamneseSubGrupoQuestoes { get; set; }
        public virtual ICollection<AnamneseSubGrupo> AnamneseSubGrupos { get; set; }
        public virtual ICollection<ProfissionalAcesso> ProfissionalAcessos { get; set; }

        public static Profissional CriarParaImportacao(string tipoProfissional, string tipoPessoa, string nomeCompleto, string? areaAtuacao, string? cpf, string? cnpj,
                            string? crp, string? crfa, string? crefito, string? crm, string? crn, string? coffito, string sexo, string email, string celular, Guid? usuarioID, bool? ativo)
        {
            var profissional = new Profissional
            {
                TipoProfissional = tipoProfissional,
                TipoPessoa = tipoPessoa,
                NomeCompleto = nomeCompleto,
                AreaAtuacao = areaAtuacao,
                Cpf = cpf,
                Cnpj = cnpj,
                Crp = crp,
                Crfa = crfa,
                Crefito = crefito,
                Crm = crm,
                Crn = crn,
                Coffito = coffito,
                Sexo = sexo,
                Email = email,
                Celular = celular,
                UsuarioID = usuarioID,
                Ativo = ativo
            };
            return profissional;
        }

        public Profissional AtualizarPropriedades(string tipoProfissional, string tipoPessoa, string nomeCompleto, string? areaAtuacao, string? cpf, string? cnpj,
                            string? crp, string? crfa, string? crefito, string? crm, string? crn, string? coffito, string sexo, string email, string celular, Guid? usuarioID, bool? ativo)
        {
            TipoProfissional = tipoProfissional;
            TipoPessoa = tipoPessoa;
            NomeCompleto = nomeCompleto;

            if (areaAtuacao != null)
                AreaAtuacao = areaAtuacao;

            if (cpf != null)
                Cpf = cpf;

            if (cnpj != null)
                Cnpj = cnpj;

            if (crp != null)
                Crp = crp;

            if (crfa != null)
                Crfa = crfa;

            if (crefito != null)
                Crefito = crefito;

            if (crm != null)
                Crm = crm;

            if (crn != null)
                Crn = crn;

            if (coffito != null)
                Coffito = coffito;

            Sexo = sexo;
            Email = email;
            Celular = celular;

            if (usuarioID != null)
                UsuarioID = usuarioID;
            
            if (ativo != null)
                Ativo = ativo;

            return this;
        }
    }
}
