using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.modelos.InputModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CriarProfissionalAcessoInputModel
    {
        public int ProfissionalAcessoId { get; set; }
        public Guid ProfissionalId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public short AcessoTipo { get; set; }
    }

    public class CriarProfissionalAcessoInputModelValidator : AbstractValidator<CriarProfissionalAcessoInputModel>
    {
        public CriarProfissionalAcessoInputModelValidator()
        {
        }
    }
}
