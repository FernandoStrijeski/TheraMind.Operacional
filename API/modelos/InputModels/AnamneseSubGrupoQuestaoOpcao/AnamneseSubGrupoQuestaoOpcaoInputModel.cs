using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class AnamneseSubGrupoQuestaoOpcaoInputModel
    {
        /// <summary>
        /// Id da opção da questão do subgrupo das anamneses
        /// </summary>
        public int AnamneseSubGrupoQuestaoOpcaoId { get; set; }
        
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
        /// Texto da opção da questão da anamnese
        /// </summary>
        public string Texto { get; set; } = null!;

        /// <summary>
        /// Ordem do subgrupo da anamnese
        /// </summary>
        public short Ordem { get; set; }
        /// <summary>
        /// Ativo Sim ou Não
        /// </summary> 
        public bool Ativo { get; set; }
    }

    public class AnamneseSubGrupoQuestaoOpcaoValidator : AbstractValidator<AnamneseSubGrupoQuestaoOpcaoInputModel>
    {
        public AnamneseSubGrupoQuestaoOpcaoValidator()
        {
            RuleFor(x => x.Texto).MaximumLength(250);            
        }
    }
}
