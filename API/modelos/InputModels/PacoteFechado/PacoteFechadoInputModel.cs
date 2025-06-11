using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class PacoteFechadoInputModel
    {
        /// <summary>
        /// Id do Pacote fechado
        /// </summary>
        public int PacoteFechadoId { get; set; }

        /// <summary>
        /// Id da Empresa
        /// </summary>
        public Guid EmpresaId { get; set; }

        /// <summary>
        /// Id da Filial
        /// </summary>
        public int FilialId { get; set; }

        /// <summary>
        /// Quantidade de sessões no pacote
        /// </summary>
        public int QuantidadeSessoes { get; set; }

        /// <summary>
        /// Valor total do pacote contratado
        /// </summary>
        public decimal ValorTotal { get; set; }
        
        /// <summary>
        /// Ativo Sim ou Não
        /// </summary> 
        public bool Ativo { get; set; }

    }

    public class PacoteFechadoValidator : AbstractValidator<PacoteFechadoInputModel>
    {
        public PacoteFechadoValidator()
        {
        }
    }
}
