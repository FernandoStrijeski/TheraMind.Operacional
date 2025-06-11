using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class EmpresaAssinaturaInputModel
    {
        /// <summary>
        /// Id da assinatura da empresa
        /// </summary>
        public Guid EmpresaAssinaturaId { get; set; }

        /// <summary>
        /// Id da Empresa
        /// </summary>
        public Guid EmpresaId { get; set; }

        /// <summary>
        /// Id do plano selecionado
        /// </summary>
        public Guid PlanoId { get; set; }

        /// <summary>
        /// Tipo do plano selecionado
        /// </summary>
        public short TipoPlano { get; set; }

        /// <summary>
        /// Valor atual da assinatura da empresa
        /// </summary>
        public decimal ValorAtual { get; set; }

        /// <summary>
        /// Valor do desconto promocional quando existir
        /// </summary>
        public decimal? DescontoPromocional { get; set; }

        /// <summary>
        /// Quantidade de meses que o desconto é valido
        /// </summary>
        public short? DescontoMeses { get; set; }
        
        /// <summary>
        /// Data de expiração da assinatura contratada
        /// </summary>
        public DateTime? DataExpiracao { get; set; }        

        /// <summary>
        /// Ativo Sim ou Não
        /// </summary> 
        public bool Ativo { get; set; }

    }

    public class EmpresaAssinaturaValidator : AbstractValidator<EmpresaAssinaturaInputModel>
    {
        public EmpresaAssinaturaValidator()
        {

        }
    }
}
