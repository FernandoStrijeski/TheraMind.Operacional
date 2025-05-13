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

        public static Plano CriarParaImportacao(string nomePlano, decimal valorPlanoMensal, decimal valorPlanoAnual, decimal? descontoPromocional, short? descontoMeses, bool? ativo)
        {
            var plano = new Plano
            {
                NomePlano = nomePlano,
                ValorPlanoMensal = valorPlanoMensal,
                ValorPlanoAnual = valorPlanoAnual,
                DescontoPromocional = descontoPromocional,
                DescontoMeses = descontoMeses,
                Ativo = ativo
            };
            return plano;
        }

        public Plano AtualizarPropriedades(string nomePlano, decimal valorPlanoMensal, decimal valorPlanoAnual, decimal? descontoPromocional, short? descontoMeses, bool? ativo)
        {
            NomePlano = nomePlano;
            ValorPlanoMensal = valorPlanoMensal;
            ValorPlanoAnual = valorPlanoAnual;

            if(descontoPromocional != null)
                DescontoPromocional = descontoPromocional;

            if (descontoMeses != null)
                DescontoMeses = descontoMeses;

            if (ativo != null)
                Ativo = ativo;

            return this;
        }

    }
}
