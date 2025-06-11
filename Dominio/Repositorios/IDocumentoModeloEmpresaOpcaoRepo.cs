using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.DocumentosModelosEmpresasOpcoes
{
    public interface IDocumentoModeloEmpresaOpcaoRepo : IBaseRepositorio<DocumentoModeloEmpresaOpcao, int>
    {
        Task<List<DocumentoModeloEmpresaOpcao>> BuscarFiltros(
            Expression<Func<DocumentoModeloEmpresaOpcao, bool>> filtro = null,
            Func<IQueryable<DocumentoModeloEmpresaOpcao>, IOrderedQueryable<DocumentoModeloEmpresaOpcao>> orderBy = null,
            int skip = 0,
            int take = 0
            );        
        Task<DocumentoModeloEmpresaOpcao>? BuscarPorID(int documentoModeloEmpresaOpcaoID);
    }
}
