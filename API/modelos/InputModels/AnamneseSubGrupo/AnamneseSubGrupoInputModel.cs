using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class AnamneseSubGrupoInputModel
    {
        /// <summary>
        /// Id do subgrupo da anamnese
        /// </summary>
        public int AnamneseSubGrupoId { get; set; }

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
        /// Título da anamnese
        /// </summary>
        public string Titulo { get; set; } = null!;

        /// <summary>
        /// Ordem do subgrupo da anamnese
        /// </summary>
        public short Ordem { get; set; }
        /// <summary>
        /// Ativo Sim ou Não
        /// </summary> 
        public bool Ativo { get; set; }
    }

    public class AnamneseSubGrupoValidator : AbstractValidator<AnamneseSubGrupoInputModel>
    {
        public AnamneseSubGrupoValidator()
        {
            RuleFor(x => x.Titulo).MaximumLength(250);            
        }
    }
}
