using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class Servico
    {
        public Servico()
        {
            AgendaSessaoItems = new HashSet<AgendaSessaoItem>();
            AgendaSessaos = new HashSet<AgendaSessao>();
            FormularioSessaoCampos = new HashSet<FormularioSessaoCampo>();
            FormularioSessaos = new HashSet<FormularioSessao>();
        }

        [Key]
        public int ServicoId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public string Nome { get; set; } = null!;
        public bool Padrao { get; set; }
        public short? DuracaoMinutos { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }

        public virtual Empresa Empresa { get; set; } = null!;
        public virtual Filial Filial { get; set; } = null!;
        public virtual ICollection<AgendaSessaoItem> AgendaSessaoItems { get; set; }
        public virtual ICollection<AgendaSessao> AgendaSessaos { get; set; }
        public virtual ICollection<FormularioSessaoCampo> FormularioSessaoCampos { get; set; }
        public virtual ICollection<FormularioSessao> FormularioSessaos { get; set; }
    }
}
