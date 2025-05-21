using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dominio.DocumentosVariaveis;
using Dominio.Entidades;
using Infra.Context;
using Infra.Core.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Infra.DocumentosVariaveis
{
    public class DocumentoVariavelRepo : BaseRepositorio<DocumentoVariavel>, IDocumentoVariavelRepo
    {
        public DocumentoVariavelRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<DocumentoVariavel>> BuscarFiltros(
            Expression<Func<DocumentoVariavel, bool>> filtro = null,
            Func<IQueryable<DocumentoVariavel>, IOrderedQueryable<DocumentoVariavel>> orderBy = null,
            int skip = 0,
            int take = 0
            )
        {
            IQueryable<DocumentoVariavel> query = _dbSet.AsNoTracking();

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

        public async Task<DocumentoVariavel>? BuscarPorID(int documentoVariavelID)
        {
            var query = _dbSet.AsQueryable();
            var plano = await query.FirstOrDefaultAsync(where => where.DocumentoVariavelId == documentoVariavelID);
            return plano;
        }
    }
}
