using Dominio.AnamneseSubGrupos;
using Dominio.Entidades;
using Infra.Context;
using Infra.Core.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infra.AnamneseSubGrupos
{
    public class AnamneseSubGrupoRepo : BaseRepositorio<AnamneseSubGrupo, int>, IAnamneseSubGrupoRepo
    {
        public AnamneseSubGrupoRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<AnamneseSubGrupo>> BuscarFiltros(
            Expression<Func<AnamneseSubGrupo, bool>> filtro = null,
            Func<IQueryable<AnamneseSubGrupo>, IOrderedQueryable<AnamneseSubGrupo>> orderBy = null,
            int skip = 0,
            int take = 0
            )
        {
            IQueryable<AnamneseSubGrupo> query = _dbSet.AsNoTracking().Include(anamneseSubGrupo => anamneseSubGrupo.AnamneseSubGrupoQuestoes)
                                                                      .ThenInclude(anamneseSubGrupoQuestoes => anamneseSubGrupoQuestoes.AnamneseSubGrupoQuestaoOpcoes);

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

        public async Task<AnamneseSubGrupo>? BuscarPorID(int anamneseSubGrupoID)
        {
            var query = _dbSet.AsQueryable().Include(anamneseSubGrupo => anamneseSubGrupo.AnamneseSubGrupoQuestoes)
                                            .ThenInclude(anamneseSubGrupoQuestoes => anamneseSubGrupoQuestoes.AnamneseSubGrupoQuestaoOpcoes);

            var anamneseGrupo = await query.FirstOrDefaultAsync(where => where.AnamneseSubGrupoId == anamneseSubGrupoID);
            return anamneseGrupo;
        }
    }
}
