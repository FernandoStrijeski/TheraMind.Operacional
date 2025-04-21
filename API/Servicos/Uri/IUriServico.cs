/// NOTA: Esse código foi escrito baseado no seguinte tutorial, já que n fazia muito sentido
/// mudar a lógica : https://codewithmukesh.com/blog/pagination-in-aspnet-core-webapi/


namespace API.Servicos
{
    public interface IUriService<T>
    {
        public Uri GerarUriProximaPagina(T filtro);
    }
}
