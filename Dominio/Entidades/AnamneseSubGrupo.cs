using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class AnamneseSubGrupo
    {
        public AnamneseSubGrupo()
        {
            AnamneseRespostaClientes = new HashSet<AnamneseRespostaCliente>();
            AnamneseSubGrupoQuestaoOpcoes = new HashSet<AnamneseSubGrupoQuestaoOpcao>();
            AnamneseSubGrupoQuestaos = new HashSet<AnamneseSubGrupoQuestao>();
        }

        [Key]
        public int AnamneseSubGrupoId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public Guid ProfissionalId { get; set; }
        public int AnamneseGrupoId { get; set; }
        public string Titulo { get; set; } = null!;
        public short Ordem { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }

        public virtual AnamneseGrupo AnamneseGrupo { get; set; } = null!;
        public virtual Empresa Empresa { get; set; } = null!;
        public virtual Filial Filial { get; set; } = null!;
        public virtual Profissional Profissional { get; set; } = null!;
        public virtual ICollection<AnamneseRespostaCliente> AnamneseRespostaClientes { get; set; }
        public virtual ICollection<AnamneseSubGrupoQuestaoOpcao> AnamneseSubGrupoQuestaoOpcoes { get; set; }
        public virtual ICollection<AnamneseSubGrupoQuestao> AnamneseSubGrupoQuestaos { get; set; }

        public static AnamneseSubGrupo CriarParaImportacao(Guid empresaID, int filialID, Guid profissionalID, int anamneseGrupoId, string titulo, short ordem, bool? ativo)
        {
            var anamneseSubGrupo = new AnamneseSubGrupo
            {
                EmpresaId = empresaID,
                FilialId = filialID,
                ProfissionalId = profissionalID,
                AnamneseGrupoId = anamneseGrupoId,
                Titulo = titulo,
                Ordem = ordem,
                Ativo = ativo
            };
            return anamneseSubGrupo;
        }

        public AnamneseSubGrupo AtualizarPropriedades(Guid empresaID, int filialID, Guid profissionalID, int anamneseGrupoId, string titulo, short ordem, bool? ativo)
        {
            EmpresaId = empresaID;
            FilialId = filialID;
            ProfissionalId = profissionalID;
            AnamneseGrupoId = anamneseGrupoId;

            Titulo = titulo;
            Ordem = ordem;

            if (ativo != null)
                Ativo = ativo;

            return this;
        }
    }
}
