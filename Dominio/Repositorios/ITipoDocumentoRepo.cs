using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;

namespace Dominio.Repositorios
{
    public interface ITipoDocumentoRepo : IBaseRepositorio<TipoDocumento>
    {
        Task<List<TipoDocumento>> BuscarPorNome(string nome);
        Task<TipoDocumento>? BuscarPorID(int tipoDocumentoID);
    }
}
