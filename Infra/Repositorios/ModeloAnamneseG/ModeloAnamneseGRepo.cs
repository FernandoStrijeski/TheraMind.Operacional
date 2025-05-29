using Dominio.Entidades;
using Dominio.ModelosAnamneseG;
using Infra.Context;
using Infra.Core.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infra.ModelosAnamneseG
{
    public class ModeloAnamneseGRepo : BaseRepositorio<ModeloAnamneseG>, IModeloAnamneseGRepo
    {
        public ModeloAnamneseGRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<ModeloAnamneseG>> BuscarFiltros(
            Expression<Func<ModeloAnamneseG, bool>> filtro = null,
            Func<IQueryable<ModeloAnamneseG>, IOrderedQueryable<ModeloAnamneseG>> orderBy = null,
            int skip = 0,
            int take = 0
            )
        {
            IQueryable<ModeloAnamneseG> query = _dbSet.AsNoTracking().Include(modeloAnamneseG => modeloAnamneseG.ModeloAnamneseSubGrupos)
                                                                     .ThenInclude(modeloAnamneseSg => modeloAnamneseSg.ModeloAnamneseSubGrupoQuestoes)
                                                                     .ThenInclude(modeloAnamneseSgQuestao => modeloAnamneseSgQuestao.ModeloAnamneseSgQuestaoOpcoes);

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

        public async Task<ModeloAnamneseG>? BuscarPorID(int ModeloAnamneseGID)
        {
            var query = _dbSet.AsQueryable().Include(modeloAnamneseG => modeloAnamneseG.ModeloAnamneseSubGrupos)
                                            .ThenInclude(modeloAnamneseSg => modeloAnamneseSg.ModeloAnamneseSubGrupoQuestoes)
                                            .ThenInclude(modeloAnamneseSgQuestao => modeloAnamneseSgQuestao.ModeloAnamneseSgQuestaoOpcoes);

            var modeloAnamneseG = await query.FirstOrDefaultAsync(where => where.ModeloAnamneseGid == ModeloAnamneseGID);
            return modeloAnamneseG;
        }
    }
}
