using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class AgendaSessaoItem
    {
        [Key]
        public int AgendaSessaoItemId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public Guid ProfissionalId { get; set; }
        public int AgendaProfissionalId { get; set; }
        public int ServicoId { get; set; }
        public int FormularioSessaoId { get; set; }
        public Guid? ClienteId { get; set; }
        public Guid AgendaSessaoId { get; set; }
        public short CampoTipo { get; set; }
        public string CampoNome { get; set; } = null!;
        public string? CampoTexto { get; set; }
        public byte[]? CampoArquivo { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }

        public virtual AgendaProfissional Agenda { get; set; } = null!;
        public virtual AgendaSessao AgendaSessao { get; set; } = null!;
        public virtual Cliente? Cliente { get; set; }
        public virtual Empresa Empresa { get; set; } = null!;
        public virtual Filial Filial { get; set; } = null!;
        public virtual FormularioSessao FormularioSessao { get; set; } = null!;
        public virtual Profissional Profissional { get; set; } = null!;
        public virtual Servico Servico { get; set; } = null!;
    }
}
