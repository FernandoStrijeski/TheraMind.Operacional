using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class AnamneseGrupo
    {
        public AnamneseGrupo()
        {
            AnamneseRespostaClientes = new HashSet<AnamneseRespostaCliente>();
            AnamneseSubGrupoQuestaoOpcoes = new HashSet<AnamneseSubGrupoQuestaoOpcao>();
            AnamneseSubGrupoQuestaos = new HashSet<AnamneseSubGrupoQuestao>();
            AnamneseSubGrupos = new HashSet<AnamneseSubGrupo>();
        }

        [Key]
        public int AnamneseGrupoId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public Guid ProfissionalId { get; set; }
        public string Titulo { get; set; } = null!;
        public bool? Privado { get; set; }
        public bool EditadoPorTodos { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }

        public virtual Empresa Empresa { get; set; } = null!;
        public virtual Filial Filial { get; set; } = null!;
        public virtual ICollection<AnamneseRespostaCliente> AnamneseRespostaClientes { get; set; }
        public virtual ICollection<AnamneseSubGrupoQuestaoOpcao> AnamneseSubGrupoQuestaoOpcoes { get; set; }
        public virtual ICollection<AnamneseSubGrupoQuestao> AnamneseSubGrupoQuestaos { get; set; }
        public virtual ICollection<AnamneseSubGrupo> AnamneseSubGrupos { get; set; }
    }
}
