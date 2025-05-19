using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class ProfissionalAcessoInputModel
    {
        /// <summary>
        /// Id de acesso do Profissional
        /// </summary>
        public int ProfissionalAcessoId { get; set; }

        //IDdo profissional
        public Guid ProfissionalId { get; set; }

        /// <summary>
        /// Id da empresa
        /// </summary>
        public Guid EmpresaId { get; set; }

        /// <summary>
        /// Id da filial
        /// </summary>
        public int FilialId { get; set; }

        /// <summary>
        /// Tipo de acesso do profissional
        /// </summary>
        public short AcessoTipo { get; set; }

        /// <summary>
        /// Data de criação do registro
        /// </summary> 
        public DateTime DataCriacao { get; set; }
    }

    public class ProfissionalAcessoValidator : AbstractValidator<ProfissionalAcessoInputModel>
    {
        public ProfissionalAcessoValidator()
        {
            
        }
    }
}
