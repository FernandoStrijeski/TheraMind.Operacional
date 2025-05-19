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


        public static EmpresaAssinatura CriarParaImportacao(Guid empresaID, Guid planoID, short tipoPlano, decimal valorAtual, decimal? descontoPromocional, short? descontoMeses, DateTime? dataExpiracao, bool? ativo)
        {
            var pacoteFechado = new EmpresaAssinatura
            {
                EmpresaId = empresaID,
                PlanoId = planoID,
                TipoPlano = tipoPlano,  
                ValorAtual = valorAtual,
                DescontoPromocional = descontoPromocional,
                DescontoMeses = descontoMeses,
                DataExpiracao = dataExpiracao,
                Ativo = ativo
            };
            return pacoteFechado;
        }

        public EmpresaAssinatura AtualizarPropriedades(Guid empresaID, Guid planoID, short tipoPlano, decimal valorAtual, decimal? descontoPromocional, short? descontoMeses, DateTime? dataExpiracao, bool? ativo)
        {
            EmpresaId = empresaID;
            PlanoId = planoID;
            TipoPlano = tipoPlano;
            ValorAtual = valorAtual;

            if (descontoPromocional != null)
                DescontoPromocional = descontoPromocional;

            if (descontoMeses != null)
                DescontoMeses = descontoMeses;

            if (dataExpiracao != null)
                DataExpiracao = dataExpiracao;

            if (ativo != null)
                Ativo = ativo;

            return this;
        }
    }
}
