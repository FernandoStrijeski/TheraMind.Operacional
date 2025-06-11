using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dominio.DocumentosModelos;
using Dominio.Entidades;
using Infra.Context;
using Infra.Core.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Infra.DocumentosModelos
{
    public class DocumentoModeloRepo : BaseRepositorio<DocumentoModelo, int>, IDocumentoModeloRepo
    {
        public DocumentoModeloRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<DocumentoModelo>> BuscarFiltros(
            Expression<Func<DocumentoModelo, bool>> filtro = null,
            Func<IQueryable<DocumentoModelo>, IOrderedQueryable<DocumentoModelo>> orderBy = null,
            int skip = 0,
            int take = 0
            )
        {
            IQueryable<DocumentoModelo> query = _dbSet.AsNoTracking().Include(documentoModelo => documentoModelo.TipoDocumento);

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

        public async Task<DocumentoModelo>? BuscarPorID(int documentoModeloID)
        {
            var query = _dbSet.AsQueryable().Include(documentoModelo => documentoModelo.TipoDocumento);
            var plano = await query.FirstOrDefaultAsync(where => where.DocumentoModeloId == documentoModeloID);
            return plano;
        }
    }
}
