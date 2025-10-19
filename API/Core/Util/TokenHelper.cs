using global::Operacional.Core.Utils.Class;
using global::API.Core.Utils;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace API.Core.Utils
{
    public static class TokenHelper
    {
        public static TokenClaims LerToken(string token)
        {
            string tokenSemBearer = token.Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();

            if (!handler.CanReadToken(tokenSemBearer))
            {
                throw new ArgumentException("Token inválido.");
            }

            var jwtToken = handler.ReadJwtToken(tokenSemBearer);

            var claims = jwtToken.Claims;

            return new TokenClaims
            {
                NameIdentifier = claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value,
                Email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                GivenName = claims.First(c => c.Type == ClaimTypes.GivenName).Value,
                Surname = claims.First(c => c.Type == ClaimTypes.Surname).Value,
                Role = claims.First(c => c.Type == ClaimTypes.Role).Value,
                Alias = claims.First(c => c.Type == "Alias").Value,
            };
        }

        public static InformacoesAudit ObterInformacoesToken(string token, HttpContext? httpContext, string tokenCriptografia, string chaveCriptografia)
        {
            string tokenSemBearer = token.Replace("Bearer ", "");

            string ipCapturado = "" + JobUtils.GetClientIp(httpContext);

            string IPEncriptado = "";
            if (!String.IsNullOrEmpty(ipCapturado))
            {
                IPEncriptado = Criptografia.Encriptar(
                    tokenCriptografia,
                    chaveCriptografia,
                    ipCapturado
                );
            }
            var handler = new JwtSecurityTokenHandler();

            if (!handler.CanReadToken(tokenSemBearer))
            {
                throw new ArgumentException("Token inválido.");
            }

            var jwtToken = handler.ReadJwtToken(tokenSemBearer);

            var claims = jwtToken.Claims;

            return new InformacoesAudit
            {
                UsuarioId = Guid.Parse(claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value),
                PerfilAcesso = claims.First(c => c.Type == ClaimTypes.Role).Value,
                IPAcesso = IPEncriptado
            };
        }
    }
}
