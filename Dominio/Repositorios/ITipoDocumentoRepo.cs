using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.Repositorios
{
    public interface ITipoDocumentoRepo : IBaseRepositorio<TipoDocumento>
    {
        Task<List<TipoDocumento>> BuscarFiltros(
            Expression<Func<TipoDocumento, bool>> filtro = null,
            Func<IQueryable<TipoDocumento>, IOrderedQueryable<TipoDocumento>> orderBy = null,
            int skip = 0,
            int take = 0
            );        
        Task<TipoDocumento>? BuscarPorID(int tipoDocumentoID);
    }
}
