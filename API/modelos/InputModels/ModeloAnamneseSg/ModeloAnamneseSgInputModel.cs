using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class ModeloAnamneseSgInputModel
    {
        /// <summary>
        /// Id do subgrupo do modelo de anamnese
        /// </summary>
        public int ModeloAnamneseSgId { get; set; }

        /// <summary>
        /// Id do modelo de anamnese
        /// </summary>
        public int ModeloAnamneseGId { get; set; }

        /// <summary>
        /// Título do modelo de anamnese
        /// </summary>
        public string Titulo { get; set; } = null!;

        /// <summary>
        /// Ordem numérica de emissão
        /// </summary>
        public short Ordem { get; set; }

        /// <summary>
        /// Ativo Sim ou Não
        /// </summary> 
        public bool Ativo { get; set; }

    }

    public class ModeloAnamneseSgValidator : AbstractValidator<ModeloAnamneseSgInputModel>
    {
        public ModeloAnamneseSgValidator()
        {
            RuleFor(x => x.Titulo).MaximumLength(250);
        }
    }
}
