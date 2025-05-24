using Dominio.Entidades;
using Dominio.FormulariosSessaoCampos;
using Infra.Context;
using Infra.Core.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infra.FormularioSessaoCampos
{
    public class FormularioSessaoCampoRepo : BaseRepositorio<FormularioSessaoCampo>, IFormularioSessaoCampoRepo
    {
        public FormularioSessaoCampoRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<FormularioSessaoCampo>> BuscarFiltros(
            Expression<Func<FormularioSessaoCampo, bool>> filtro = null,
            Func<IQueryable<FormularioSessaoCampo>, IOrderedQueryable<FormularioSessaoCampo>> orderBy = null,
            int skip = 0,
            int take = 0
            )
        {
            IQueryable<FormularioSessaoCampo> query = _dbSet.AsNoTracking();

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

        public async Task<FormularioSessaoCampo>? BuscarPorID(int formularioSessaoCampoID)
        {
            var query = _dbSet.AsQueryable();

            var formularioSessaoCampo = await query.FirstOrDefaultAsync(where => where.FormularioSessaoCampoId == formularioSessaoCampoID);
            return formularioSessaoCampo;
        }
    }
}
