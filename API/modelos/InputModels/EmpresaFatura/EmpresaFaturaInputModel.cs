using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class EmpresaFaturaInputModel
    {
        /// <summary>
        /// Id da fatura da empresa
        /// </summary>
        public int EmpresaFaturaId { get; set; }

        /// <summary>
        /// Id da assinatura da empresa
        /// </summary>
        public Guid EmpresaAssinaturaId { get; set; }

        /// <summary>
        /// Id da empresa
        /// </summary>
        public Guid EmpresaId { get; set; }

        /// <summary>
        /// Id do plano
        /// </summary>
        public Guid PlanoId { get; set; }

        /// <summary>
        /// Descrição da fatura da empresa
        /// </summary>
        public string Descricao { get; set; } = null!;

        /// <summary>
        /// Data início validade da fatura
        /// </summary>
        public DateTime DataInicio { get; set; }

        /// <summary>
        /// Data de expiração da fatura
        /// </summary>
        public DateTime DataExpiracao { get; set; }

        /// <summary>
        /// Valor da fatura
        /// </summary>
        public decimal Valor { get; set; }

        /// <summary>
        /// Forma de pagamento da fatura (0-Pix, 1-Boleto, 2-Debito, 3-Credito)
        /// </summary>
        public short? FormaPagamento { get; set; }

        /// <summary>
        /// Anexo em caso de boleto ou código pix
        /// </summary>
        public byte[]? Anexo { get; set; }

        /// <summary>
        /// Situação da fatura (0-Pendente, 1-Pago)
        /// </summary>
        public short Situacao { get; set; }

        /// <summary>
        /// Data de pagamento da fatura pela empresa
        /// </summary>
        public DateTime? DataPagamento { get; set; }        

        /// <summary>
        /// Ativo Sim ou Não
        /// </summary> 
        public bool Ativo { get; set; }

        /// <summary>
        /// Data de criação do registro
        /// </summary> 
        public DateTime DataCriacao { get; set; }
    }

    public class EmpresaFaturaValidator : AbstractValidator<EmpresaFaturaInputModel>
    {
        public EmpresaFaturaValidator()
        {                  
        }
    }
}
