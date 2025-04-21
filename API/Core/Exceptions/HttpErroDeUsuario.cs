using System.Net;

namespace API.Core.Exceptions
{
    [Serializable]
    public class HttpErroDeUsuario : Exception
    {
        public HttpStatusCode codigo;

        public HttpErroDeUsuario(HttpStatusCode codigo, string message) : base(message)
        {
            this.codigo = codigo;
        }
    }
}
