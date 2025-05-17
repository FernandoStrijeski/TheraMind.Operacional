using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class Profissional
    {
        public Profissional()
        {
            AcompanhamentoClinicos = new HashSet<AcompanhamentoClinico>();
            AgendaProfissionals = new HashSet<AgendaProfissional>();
            AgendaSessaoItems = new HashSet<AgendaSessaoItem>();
            AgendaSessaos = new HashSet<AgendaSessao>();
            AnamneseRespostaClientes = new HashSet<AnamneseRespostaCliente>();
            AnamneseSubGrupoQuestaoOpcaos = new HashSet<AnamneseSubGrupoQuestaoOpcao>();
            AnamneseSubGrupoQuestaos = new HashSet<AnamneseSubGrupoQuestao>();
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

        public virtual ICollection<AcompanhamentoClinico> AcompanhamentoClinicos { get; set; }
        public virtual ICollection<AgendaProfissional> AgendaProfissionals { get; set; }
        public virtual ICollection<AgendaSessaoItem> AgendaSessaoItems { get; set; }
        public virtual ICollection<AgendaSessao> AgendaSessaos { get; set; }
        public virtual ICollection<AnamneseRespostaCliente> AnamneseRespostaClientes { get; set; }
        public virtual ICollection<AnamneseSubGrupoQuestaoOpcao> AnamneseSubGrupoQuestaoOpcaos { get; set; }
        public virtual ICollection<AnamneseSubGrupoQuestao> AnamneseSubGrupoQuestaos { get; set; }
        public virtual ICollection<AnamneseSubGrupo> AnamneseSubGrupos { get; set; }
        public virtual ICollection<ProfissionalAcesso> ProfissionalAcessos { get; set; }        
    }
}
