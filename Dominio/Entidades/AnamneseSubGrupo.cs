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
            AnamneseSubGrupoQuestaoOpcaos = new HashSet<AnamneseSubGrupoQuestaoOpcao>();
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
        public virtual ICollection<AnamneseSubGrupoQuestaoOpcao> AnamneseSubGrupoQuestaoOpcaos { get; set; }
        public virtual ICollection<AnamneseSubGrupoQuestao> AnamneseSubGrupoQuestaos { get; set; }
    }
}
