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

        public static EmpresaFatura CriarParaImportacao(Guid empresaAssinaturaID, Guid empresaID, Guid planoID, string descricao, DateTime dataInicio, DateTime dataExpiracao, decimal valor,
                                                        short? formaPagamento, byte[]? anexo, short situacao, DateTime? dataPagamento, bool? ativo)
        {
            var convenio = new EmpresaFatura
            {
                EmpresaId = empresaID,
                EmpresaAssinaturaId = empresaAssinaturaID,
                PlanoId = planoID,
                Descricao = descricao,
                DataInicio = dataInicio,
                DataExpiracao = dataExpiracao,
                Valor = valor,
                FormaPagamento = formaPagamento,
                Anexo = anexo,
                Situacao = situacao,
                DataPagamento = dataPagamento,
                Ativo = ativo
            };
            return convenio;
        }

        public EmpresaFatura AtualizarPropriedades(Guid empresaAssinaturaID, Guid empresaID, Guid planoID, string descricao, DateTime dataInicio, DateTime dataExpiracao, decimal valor,
                                                        short? formaPagamento, byte[]? anexo, short situacao, DateTime? dataPagamento, bool? ativo)
        {
            EmpresaId = empresaID;
            EmpresaAssinaturaId = empresaAssinaturaID;
            PlanoId = planoID;
            Descricao = descricao;
            DataInicio = dataInicio;
            DataExpiracao = dataExpiracao;
            Valor = valor;

            if (formaPagamento != null)
                FormaPagamento = formaPagamento;

            if (anexo != null)
                Anexo = anexo;

            Situacao = situacao;

            if (dataPagamento != null)
                DataPagamento = dataPagamento;

            if (ativo != null)
                Ativo = ativo;

            return this;
        }
    }
}
