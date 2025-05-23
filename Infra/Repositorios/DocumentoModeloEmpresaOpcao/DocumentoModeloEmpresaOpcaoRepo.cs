using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dominio.DocumentosModelosEmpresasOpcoes;
using Dominio.Entidades;
using Infra.Context;
using Infra.Core.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Infra.DocumentosModelosEmpresasOpcoes
{
    public class DocumentoModeloEmpresaOpcaoRepo : BaseRepositorio<DocumentoModeloEmpresaOpcao>, IDocumentoModeloEmpresaOpcaoRepo
    {
        public DocumentoModeloEmpresaOpcaoRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<DocumentoModeloEmpresaOpcao>> BuscarFiltros(
            Expression<Func<DocumentoModeloEmpresaOpcao, bool>> filtro = null,
            Func<IQueryable<DocumentoModeloEmpresaOpcao>, IOrderedQueryable<DocumentoModeloEmpresaOpcao>> orderBy = null,
            int skip = 0,
            int take = 0
            )
        {
            IQueryable<DocumentoModeloEmpresaOpcao> query = _dbSet.AsNoTracking();

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

        public async Task<DocumentoModeloEmpresaOpcao>? BuscarPorID(int documentoModeloEmpresaOpcaoID)
        {
            var query = _dbSet.AsQueryable();
            var documentoModeloEmpresaOpcao = await query.FirstOrDefaultAsync(where => where.DocumentoModeloEmpresaOpcaoId == documentoModeloEmpresaOpcaoID);
            return documentoModeloEmpresaOpcao;
        }
    }
}
