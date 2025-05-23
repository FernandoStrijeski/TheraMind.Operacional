using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class FormularioSessao
    {
        public FormularioSessao()
        {
            AgendaSessaoItens = new HashSet<AgendaSessaoItem>();
            AgendaSessoes = new HashSet<AgendaSessao>();
            FormularioSessaoCampos = new HashSet<FormularioSessaoCampo>();
        }

        [Key]
        public int FormularioSessaoId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public int ServicoId { get; set; }
        public string Nome { get; set; } = null!;
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }

        public virtual Empresa Empresa { get; set; } = null!;
        public virtual Filial Filial { get; set; } = null!;
        public virtual Servico Servico { get; set; } = null!;
        public virtual ICollection<AgendaSessaoItem> AgendaSessaoItens { get; set; }
        public virtual ICollection<AgendaSessao> AgendaSessoes { get; set; }
        public virtual ICollection<FormularioSessaoCampo> FormularioSessaoCampos { get; set; }
    }
}
