using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class AnamneseGrupoInputModel
    {
        /// <summary>
        /// Id da anamnese
        /// </summary>
        public int AnamneseGrupoId { get; set; }

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
        /// Título da anamnese
        /// </summary>
        public string Titulo { get; set; } = null!;

        /// <summary>
        /// Exibição da anamnese (Privado para o criador ou para todos da empresa)
        /// </summary>
        public bool? Privado { get; set; }

        /// <summary>
        /// Permite que todos possam editar ou apenas o criador da anamnese
        /// </summary>
        public bool EditadoPorTodos { get; set; }

        /// <summary>
        /// Ativo Sim ou Não
        /// </summary> 
        public bool Ativo { get; set; }
    }

    public class AnamneseGrupoValidator : AbstractValidator<AnamneseGrupoInputModel>
    {
        public AnamneseGrupoValidator()
        {
            RuleFor(x => x.Titulo).MaximumLength(100);            
        }
    }
}
