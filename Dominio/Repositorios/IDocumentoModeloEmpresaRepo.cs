using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.DocumentosModelosEmpresas
{
    public interface IDocumentoModeloEmpresaRepo : IBaseRepositorio<DocumentoModeloEmpresa>
    {
        Task<List<DocumentoModeloEmpresa>> BuscarFiltros(
            Expression<Func<DocumentoModeloEmpresa, bool>> filtro = null,
            Func<IQueryable<DocumentoModeloEmpresa>, IOrderedQueryable<DocumentoModeloEmpresa>> orderBy = null,
            int skip = 0,
            int take = 0
            );        
        Task<DocumentoModeloEmpresa>? BuscarPorID(int documentoModeloEmpresaID);
    }
}
