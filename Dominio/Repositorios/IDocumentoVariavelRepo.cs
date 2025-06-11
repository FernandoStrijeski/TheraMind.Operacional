using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.DocumentosVariaveis
{
    public interface IDocumentoVariavelRepo : IBaseRepositorio<DocumentoVariavel, int>
    {
        Task<List<DocumentoVariavel>> BuscarFiltros(
            Expression<Func<DocumentoVariavel, bool>> filtro = null,
            Func<IQueryable<DocumentoVariavel>, IOrderedQueryable<DocumentoVariavel>> orderBy = null,
            int skip = 0,
            int take = 0
            );        
        Task<DocumentoVariavel>? BuscarPorID(int documentoVariavelID);
    }
}
