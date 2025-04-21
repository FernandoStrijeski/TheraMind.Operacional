using FluentValidation;

namespace API.Core.Extensoes
{
    public static class ValidadoresExtensao
    {
        public static IServiceCollection AdicionarValidadores(
            this IServiceCollection services,
            IConfiguration config
        )
        {
            //services
                //.AddScoped<IValidator<CriarColaboradorDTO>, CriarColaboradorDTOValidator>()
                
            return services;
        }
    }
}
