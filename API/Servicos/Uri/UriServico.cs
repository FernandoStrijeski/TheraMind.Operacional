using API.modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;

namespace API.Servicos
{
    public class UriService<T> : IUriService<T>
    {
        private readonly string _baseUri;
        private readonly string _rota;
        private readonly HttpRequest _request;

        public UriService(HttpRequest request)
        {
            _baseUri = string.Concat(
                request.Scheme,
                "://",
                request.Host.ToUriComponent()
            );
            _rota = request.Path;
        }

        public Uri GerarUriProximaPagina(T filtro)
        {
            var _endpointUri = new Uri(string.Concat(_baseUri, _rota));
            string uriModificada = _endpointUri.ToString();
            var objectDict = paraDicionario(filtro);

            foreach (KeyValuePair<string, object> entry in objectDict)
            {
                if (entry.Value == null)
                {
                    continue;
                }

                string valor = entry.Value.ToString();

                if (entry.Key == "NumeroPagina")
                {
                    short numeroPagina = Convert.ToInt16(valor);
                    numeroPagina += (short)1;
                    valor = numeroPagina.ToString();
                }
                uriModificada = QueryHelpers.AddQueryString(uriModificada, entry.Key, valor);
            }

            return new Uri(uriModificada);
        }

        private Dictionary<string, object?> paraDicionario(object Obj)
        {
            return Obj.GetType()
                .GetProperties()
                .ToDictionary(propInfo => propInfo.Name, propInfo => propInfo.GetValue(Obj, null));
        }
    }
}
