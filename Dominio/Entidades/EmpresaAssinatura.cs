using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class EmpresaAssinatura
    {
        public EmpresaAssinatura()
        {
            EmpresaFaturas = new HashSet<EmpresaFatura>();
        }

        [Key]
        public Guid EmpresaAssinaturaId { get; set; }
        public Guid EmpresaId { get; set; }
        public Guid PlanoId { get; set; }
        public short TipoPlano { get; set; }
        public decimal ValorAtual { get; set; }
        public decimal? DescontoPromocional { get; set; }
        public short? DescontoMeses { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataExpiracao { get; set; }
        public DateTime? DataCriacao { get; set; }

        public virtual Empresa Empresa { get; set; } = null!;
        public virtual Plano Plano { get; set; } = null!;
        public virtual ICollection<EmpresaFatura> EmpresaFaturas { get; set; }
    }
}
