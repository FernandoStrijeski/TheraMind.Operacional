using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace API.Core.Filtros
{
    public class RequerValidacaoDeToken : Attribute, IAuthorizationFilter
    {
        private const string NomeHeader = "Alias";
        private const string NomeRolesHeader = "Roles";
        private const string NomeAutorization = "Authorization";


        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Headers[NomeAutorization];
            if (string.IsNullOrEmpty(token))
            {
                context.Result = new ForbidResult("Não contém token de validação");
                return;
            }
            context.HttpContext.Request.Headers.Remove(NomeHeader);
            var alias = BuscarAliasNoToken(token);
            if (alias == null)
            {
                context.Result = new ForbidResult(
                    "O Alias para o banco de dados precisa ser informado"
                );
                return;
            }
            List<string?> roleClaims = BuscarRolesClaims(token);
            if (roleClaims.Count > 0)
            {
                string roleClaimsValores = roleClaims.Distinct().Aggregate((a, b) => a + "," + b);
                context.HttpContext.Request.Headers.Add(NomeRolesHeader, roleClaimsValores);
            }
            context.HttpContext.Request.Headers.Add(NomeHeader, alias);
            return;
        }

        private List<string?> BuscarRolesClaims(string token)
        {
            var jwt = lerJwt(token);
            var firstClaim = jwt.Claims.FirstOrDefault();
            var claims = new ClaimsIdentity(jwt.Claims).FindAll(claim => claim.Type == ClaimTypes.Role);
            return (from Claim claim in claims
                    select claim.Value).ToList();
        }

        private string? BuscarAliasNoToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            token = token.Replace("Bearer ", "");
            var jwt = tokenHandler.ReadJwtToken(token);
            return jwt.Payload.TryGetValue(NomeHeader, out var alias) ? alias.ToString() : null;
        }

        private JwtSecurityToken lerJwt(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            token = token.Replace("Bearer ", "");
            return tokenHandler.ReadJwtToken(token);
        }
    }
}
