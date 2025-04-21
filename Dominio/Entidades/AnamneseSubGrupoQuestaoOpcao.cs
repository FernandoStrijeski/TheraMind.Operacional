using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class AnamneseSubGrupoQuestaoOpcao
    {
        [Key]
        public int AnamneseSubGrupoQuestaoOpcaoId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public Guid ProfissionalId { get; set; }
        public int AnamneseGrupoId { get; set; }
        public int AnamneseSubGrupoId { get; set; }
        public int AnamneseSubGrupoQuestaoId { get; set; }
        public string? Texto { get; set; }
        public short Ordem { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }

        public virtual AnamneseGrupo AnamneseGrupo { get; set; } = null!;
        public virtual AnamneseSubGrupo AnamneseSubGrupo { get; set; } = null!;
        public virtual AnamneseSubGrupoQuestao AnamneseSubGrupoQuestao { get; set; } = null!;
        public virtual Empresa Empresa { get; set; } = null!;
        public virtual Filial Filial { get; set; } = null!;
        public virtual Profissional Profissional { get; set; } = null!;
    }
}
