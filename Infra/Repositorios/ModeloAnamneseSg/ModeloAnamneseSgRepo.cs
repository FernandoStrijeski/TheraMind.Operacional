using Dominio.Entidades;
using Dominio.ModelosAnamneseSG;
using Infra.Context;
using Infra.Core.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infra.ModelosAnamneseSG
{
    public class ModeloAnamneseSgRepo : BaseRepositorio<ModeloAnamneseSg>, IModeloAnamneseSgRepo
    {
        public ModeloAnamneseSgRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<ModeloAnamneseSg>> BuscarFiltros(
            Expression<Func<ModeloAnamneseSg, bool>> filtro = null,
            Func<IQueryable<ModeloAnamneseSg>, IOrderedQueryable<ModeloAnamneseSg>> orderBy = null,
            int skip = 0,
            int take = 0
            )
        {
            IQueryable<ModeloAnamneseSg> query = _dbSet.AsNoTracking().Include(modeloAnamneseSg => modeloAnamneseSg.ModeloAnamneseSubGrupoQuestoes);

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

        public async Task<ModeloAnamneseSg>? BuscarPorID(int ModeloAnamneseSgID)
        {
            var query = _dbSet.AsQueryable().Include(modeloAnamneseSg => modeloAnamneseSg.ModeloAnamneseSubGrupoQuestoes);
            var modeloAnamneseSg = await query.FirstOrDefaultAsync(where => where.ModeloAnamneseSgid == ModeloAnamneseSgID);
            return modeloAnamneseSg;
        }
    }
}
