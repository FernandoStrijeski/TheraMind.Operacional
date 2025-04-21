using System.Net;

namespace API.Core.Exceptions
{
    [Serializable]
    public class HttpErroDeServidor : Exception
    {
        public HttpStatusCode codigo;

        public HttpErroDeServidor(string message, HttpStatusCode codigo = HttpStatusCode.BadRequest)
            : base(message)
        {
            this.codigo = codigo;
        }
    }
}
