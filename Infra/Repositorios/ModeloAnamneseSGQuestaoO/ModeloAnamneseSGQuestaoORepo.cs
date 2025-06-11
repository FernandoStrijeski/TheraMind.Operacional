using Dominio.Entidades;
using Dominio.ModelosAnamneseSGQuestaoOpcoes;
using Infra.Context;
using Infra.Core.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infra.ModelosAnamneseSGQuestaoOpcoes
{
    public class ModeloAnamneseSgQuestaoORepo : BaseRepositorio<ModeloAnamneseSgQuestaoO, int>, IModeloAnamneseSgQuestaoORepo
    {
        public ModeloAnamneseSgQuestaoORepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<ModeloAnamneseSgQuestaoO>> BuscarFiltros(
            Expression<Func<ModeloAnamneseSgQuestaoO, bool>> filtro = null,
            Func<IQueryable<ModeloAnamneseSgQuestaoO>, IOrderedQueryable<ModeloAnamneseSgQuestaoO>> orderBy = null,
            int skip = 0,
            int take = 0
            )
        {
            IQueryable<ModeloAnamneseSgQuestaoO> query = _dbSet.AsNoTracking();

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

        public async Task<ModeloAnamneseSgQuestaoO>? BuscarPorID(int modeloAnamneseSgQuestaoOID)
        {
            var query = _dbSet.AsQueryable();
            var modeloAnamneseSgQuestaoO = await query.FirstOrDefaultAsync(where => where.ModeloAnamneseSgQuestaoOid == modeloAnamneseSgQuestaoOID);
            return modeloAnamneseSgQuestaoO;
        }
    }
}
