using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.DocumentosModelos
{
    public interface IDocumentoModeloRepo : IBaseRepositorio<DocumentoModelo>
    {
        Task<List<DocumentoModelo>> BuscarFiltros(
            Expression<Func<DocumentoModelo, bool>> filtro = null,
            Func<IQueryable<DocumentoModelo>, IOrderedQueryable<DocumentoModelo>> orderBy = null,
            int skip = 0,
            int take = 0
            );        
        Task<DocumentoModelo>? BuscarPorID(int documentoModeloID);
    }
}
