using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class AnamneseRespostaClienteInputModel
    {               
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
        /// Id do grupo de anamnese
        /// </summary>
        public int AnamneseGrupoId { get; set; }

        /// <summary>
        /// Id do subgrupo da anamnese
        /// </summary>
        public int AnamneseSubGrupoId { get; set; }

        /// <summary>
        /// Id da questão do subgrupo das anamneses
        /// </summary>
        public int AnamneseSubGrupoQuestaoId { get; set; }

        /// <summary>
        /// Id do cliente da anamnese
        /// </summary>
        public Guid ClienteID { get; set; }

        /// <summary>
        /// Resposta da questão da anamnese, texto livre ou as opções separadas por vírgula (1,3,5)
        /// </summary>
        public string? Resposta { get; set; }

    }

    public class AnamneseRespostaClienteValidator : AbstractValidator<AnamneseRespostaClienteInputModel>
    {
        public AnamneseRespostaClienteValidator()
        {
                  
        }
    }
}
