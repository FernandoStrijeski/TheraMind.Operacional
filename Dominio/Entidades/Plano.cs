using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class Plano
    {
        public Plano()
        {
            EmpresaAssinaturas = new HashSet<EmpresaAssinatura>();
            EmpresaFaturas = new HashSet<EmpresaFatura>();
        }

        [Key]
        public Guid PlanoId { get; set; }
        public string NomePlano { get; set; } = null!;
        public decimal ValorPlanoMensal { get; set; }
        public decimal ValorPlanoAnual { get; set; }
        public decimal? DescontoPromocional { get; set; }
        public short? DescontoMeses { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }

        public virtual ICollection<EmpresaAssinatura> EmpresaAssinaturas { get; set; }
        public virtual ICollection<EmpresaFatura> EmpresaFaturas { get; set; }
    }
}
