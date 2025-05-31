using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class AnamneseSubGrupoQuestaoInputModel
    {
        /// <summary>
        /// Id da questão do subgrupo das anamneses
        /// </summary>
        public int AnamneseSubGrupoQuestaoId { get; set; }
        
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
        /// Título da anamnese
        /// </summary>
        public string Titulo { get; set; } = null!;

        /// <summary>
        /// Tipo de opção (0-Texto Curto, 1-Texto Longo, 2-SimNao, 3-Optativa, 4-MultiplaEscolha)
        /// </summary>
        public short TipoOpcao { get; set; }

        /// <summary>
        /// Ordem do subgrupo da anamnese
        /// </summary>
        public short Ordem { get; set; }
        /// <summary>
        /// Ativo Sim ou Não
        /// </summary> 
        public bool Ativo { get; set; }
    }

    public class AnamneseSubGrupoQuestaoValidator : AbstractValidator<AnamneseSubGrupoQuestaoInputModel>
    {
        public AnamneseSubGrupoQuestaoValidator()
        {
            RuleFor(x => x.Titulo).MaximumLength(250);            
        }
    }
}
