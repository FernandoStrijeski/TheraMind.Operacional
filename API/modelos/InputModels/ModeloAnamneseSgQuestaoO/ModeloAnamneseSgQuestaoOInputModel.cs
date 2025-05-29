using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class ModeloAnamneseSgQuestaoOInputModel
    {
        /// <summary>
        /// Id da opção da questão do subgrupo dos grupos de anamnese
        /// </summary>
        public int ModeloAnamneseSgQuestaoOid { get; set; }

        /// <summary>
        /// Id do grupo do modelo da anamnese
        /// </summary>
        public int ModeloAnamneseGid { get; set; }

        /// <summary>
        /// Id do subgrupo do modelo da anamnese
        /// </summary>
        public int ModeloAnamneseSgid { get; set; }

        /// <summary>
        /// Id da questão do subgrupo do grupo de modelo da anamnese
        /// </summary>
        public int ModeloAnamneseSgQuestaoId { get; set; }

        /// <summary>
        /// Texto da opção
        /// </summary>
        public string Texto { get; set; } = null!;

        /// <summary>
        /// Ordem numérica de emissão
        /// </summary>
        public short Ordem { get; set; }

        /// <summary>
        /// Ativo Sim ou Não
        /// </summary> 
        public bool Ativo { get; set; }
    }

    public class ModeloAnamneseSgQuestaoOValidator : AbstractValidator<ModeloAnamneseSgQuestaoOInputModel>
    {
        public ModeloAnamneseSgQuestaoOValidator()
        {
            RuleFor(x => x.Texto).MaximumLength(250);
        }
    }
}
