using Dominio.Entidades;
using Dominio.ModelosAnamneseSG;
using Dominio.ModelosAnamneseSGQuestoes;
using Infra.Context;
using Infra.Core.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infra.ModelosAnamneseSGQuestoes
{
    public class ModeloAnamneseSgQuestaoRepo : BaseRepositorio<ModeloAnamneseSgQuestao>, IModeloAnamneseSgQuestaoRepo
    {
        public ModeloAnamneseSgQuestaoRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<ModeloAnamneseSgQuestao>> BuscarFiltros(
            Expression<Func<ModeloAnamneseSgQuestao, bool>> filtro = null,
            Func<IQueryable<ModeloAnamneseSgQuestao>, IOrderedQueryable<ModeloAnamneseSgQuestao>> orderBy = null,
            int skip = 0,
            int take = 0
            )
        {
            IQueryable<ModeloAnamneseSgQuestao> query = _dbSet.AsNoTracking().Include(modeloAnamneseSqQuestao => modeloAnamneseSqQuestao.ModeloAnamneseSgQuestaoOpcoes);

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

        public async Task<ModeloAnamneseSgQuestao>? BuscarPorID(int modeloAnamneseSgQuestaoID)
        {
            var query = _dbSet.AsQueryable().Include(modeloAnamneseSqQuestao => modeloAnamneseSqQuestao.ModeloAnamneseSgQuestaoOpcoes);
            var modeloAnamneseSgQuestao = await query.FirstOrDefaultAsync(where => where.ModeloAnamneseSgQuestaoId == modeloAnamneseSgQuestaoID);
            return modeloAnamneseSgQuestao;
        }
    }
}
