using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dominio.DocumentosModelosEmpresas;
using Dominio.Entidades;
using Infra.Context;
using Infra.Core.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Infra.DocumentosModelosEmpresas
{
    public class DocumentoModeloEmpresaRepo : BaseRepositorio<DocumentoModeloEmpresa>, IDocumentoModeloEmpresaRepo
    {
        public DocumentoModeloEmpresaRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<DocumentoModeloEmpresa>> BuscarFiltros(
            Expression<Func<DocumentoModeloEmpresa, bool>> filtro = null,
            Func<IQueryable<DocumentoModeloEmpresa>, IOrderedQueryable<DocumentoModeloEmpresa>> orderBy = null,
            int skip = 0,
            int take = 0
            )
        {
            IQueryable<DocumentoModeloEmpresa> query = _dbSet.AsNoTracking().Include(documentoModeloEmpresa => documentoModeloEmpresa.TipoDocumento);

            if (filtro != null)
                query = query.Where(filtro);

            if (skip > 0)
                query = query.Skip(skip);

            if (take > 0)
                query = query.Take(take);

            if (orderBy != null)
                query = orderBy(query);

            return await query.ToListAsync();
        }

        public async Task<DocumentoModeloEmpresa>? BuscarPorID(int documentoModeloEmpresaID)
        {
            var query = _dbSet.AsQueryable().Include(documentoModelo => documentoModelo.TipoDocumento);
            var documentoModeloEmpresa = await query.FirstOrDefaultAsync(where => where.DocumentoModeloEmpresaID == documentoModeloEmpresaID);
            return documentoModeloEmpresa;
        }
    }
}
