using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class AcompanhamentoClinicoInputModel
    {
        /// <summary>
        /// Id do acompanhamento clínico
        /// </summary>
        public Guid AcompanhamentoClinicoId { get; set; }

        /// <summary>
        /// Id da empresa
        /// </summary>
        public Guid EmpresaId { get; set; }

        /// <summary>
        /// Id da filial
        /// </summary>
        public int FilialId { get; set; }

        /// <summary>
        /// Id do profissional
        /// </summary>
        public Guid ProfissionalId { get; set; }

        /// <summary>
        /// Id do cliente
        /// </summary>
        public Guid? ClienteId { get; set; }

        /// <summary>
        /// Avaliação da Demanda
        /// </summary>
        public string? AvaliacaoDemanda { get; set; }

        /// <summary>
        /// Plano de tratamento
        /// </summary>
        public string? PlanoTratamento { get; set; }

        /// <summary>
        /// Diagnóstico
        /// </summary>
        public string? Diagnostico { get; set; }

        /// <summary>
        /// Registro de encerramento
        /// </summary>
        public string? RegistroEncerramento { get; set; }

        /// <summary>
        /// Data de criação do registro
        /// </summary> 
        public DateTime DataCriacao { get; set; }
    }

    public class AcompanhamentoClinicoValidator : AbstractValidator<AcompanhamentoClinicoInputModel>
    {
        public AcompanhamentoClinicoValidator()
        {               
        }
    }
}
