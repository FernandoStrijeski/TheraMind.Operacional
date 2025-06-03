using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class AnamneseSubGrupoQuestao
    {
        public AnamneseSubGrupoQuestao()
        {
            AnamneseSubGrupoQuestaoOpcoes = new HashSet<AnamneseSubGrupoQuestaoOpcao>();
        }

        [Key]
        public int AnamneseSubGrupoQuestaoId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public Guid ProfissionalId { get; set; }
        public int AnamneseGrupoId { get; set; }
        public int AnamneseSubGrupoId { get; set; }
        public string Titulo { get; set; } = null!;
        public short TipoOpcao { get; set; }
        public short Ordem { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }

        public virtual AnamneseGrupo AnamneseGrupo { get; set; } = null!;
        public virtual AnamneseSubGrupo AnamneseSubGrupo { get; set; } = null!;
        public virtual Empresa Empresa { get; set; } = null!;
        public virtual Filial Filial { get; set; } = null!;
        public virtual Profissional Profissional { get; set; } = null!;
        public virtual ICollection<AnamneseSubGrupoQuestaoOpcao> AnamneseSubGrupoQuestaoOpcoes { get; set; }

        public static AnamneseSubGrupoQuestao CriarParaImportacao(Guid empresaID, int filialID, Guid profissionalID, int anamneseGrupoID, int anamneseSubGrupoID, string titulo, short tipoOpcao, short ordem, bool? ativo)
        {
            var anamneseSubGrupoQuestao = new AnamneseSubGrupoQuestao
            {
                EmpresaId = empresaID,
                FilialId = filialID,
                ProfissionalId = profissionalID,
                AnamneseGrupoId = anamneseGrupoID,
                AnamneseSubGrupoId = anamneseSubGrupoID,
                Titulo = titulo,
                TipoOpcao = tipoOpcao,
                Ordem = ordem,
                Ativo = ativo
            };
            return anamneseSubGrupoQuestao;
        }

        public AnamneseSubGrupoQuestao AtualizarPropriedades(Guid empresaID, int filialID, Guid profissionalID, int anamneseGrupoID, int anamneseSubGrupoID, string titulo, short tipoOpcao, short ordem, bool? ativo)
        {
            EmpresaId = empresaID;
            FilialId = filialID;
            ProfissionalId = profissionalID;
            AnamneseGrupoId = anamneseGrupoID;
            AnamneseSubGrupoId = anamneseSubGrupoID;
            Titulo = titulo;
            TipoOpcao = tipoOpcao;
            Ordem = ordem;

            if (ativo != null)
                Ativo = ativo;

            return this;
        }
    }
}
