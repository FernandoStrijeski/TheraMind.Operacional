namespace AdmissaoDigital.Core.Utils
{
    using global::AdmissaoDigital.Core.Utils.Class;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;

    namespace API.Core.Utils
    {
        public static class TokenHelper
        {
            //public static TokenClaims LerToken(string token)
            //{
            //    var handler = new JwtSecurityTokenHandler();

            //    if (!handler.CanReadToken(token))
            //    {
            //        throw new ArgumentException("Token inválido.");
            //    }

            //    var jwtToken = handler.ReadJwtToken(token);

            //    var claims = jwtToken.Claims;

            //    return new TokenClaims
            //    {
            //        Cpf = claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value,
            //        Email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
            //        NomeCompleto = claims.First(c => c.Type == ClaimTypes.GivenName).Value,
            //        OrganizacaoId = claims.First(c => c.Type == "OrganizacaoID").Value,
            //        TipoUsuario = claims.First(c => c.Type == "TipoUsuario").Value,
            //        Celular = claims.FirstOrDefault(c => c.Type == "Celular")?.Value,
            //        Alias = claims.First(c => c.Type == "Alias").Value,
            //    };
            //}
        }
    }
}
