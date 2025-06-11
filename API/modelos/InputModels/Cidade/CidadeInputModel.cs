using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class CidadeInputModel
    {
        public int CidadeId { get; set; }
        public int PaisId { get; set; }
        public string Uf { get; set; } = null!;
        public string Nome { get; set; } = null!;
        public int CodigoIbge { get; set; }

    }

    public class CidadeValidator : AbstractValidator<CidadeInputModel>
    {
        public CidadeValidator()
        {
            RuleFor(x => x.Nome).MaximumLength(255);            
        }
    }
}
