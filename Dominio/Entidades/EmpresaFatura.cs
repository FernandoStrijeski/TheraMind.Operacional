using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class EmpresaFatura
    {
        [Key]
        public int EmpresaFaturaId { get; set; }
        public Guid EmpresaAssinaturaId { get; set; }
        public Guid EmpresaId { get; set; }
        public Guid PlanoId { get; set; }
        public string Descricao { get; set; } = null!;
        public DateTime DataInicio { get; set; }
        public DateTime DataExpiracao { get; set; }
        public decimal Valor { get; set; }
        public short? FormaPagamento { get; set; }
        public byte[]? Anexo { get; set; }
        public short Situacao { get; set; }
        public DateTime? DataPagamento { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }

        public virtual Empresa Empresa { get; set; } = null!;
        public virtual EmpresaAssinatura EmpresaAssinatura { get; set; } = null!;
        public virtual Plano Plano { get; set; } = null!;
    }
}
