using Dominio.AnamneseGrupos;
using Dominio.Entidades;
using Infra.Context;
using Infra.Core.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infra.AnamneseGrupos
{
    public class AnamneseGrupoRepo : BaseRepositorio<AnamneseGrupo, int>, IAnamneseGrupoRepo
    {
        public AnamneseGrupoRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<AnamneseGrupo>> BuscarFiltros(
            Expression<Func<AnamneseGrupo, bool>> filtro = null,
            Func<IQueryable<AnamneseGrupo>, IOrderedQueryable<AnamneseGrupo>> orderBy = null,
            int skip = 0,
            int take = 0
            )
        {
            IQueryable<AnamneseGrupo> query = _dbSet.AsNoTracking().Include(anamneseGrupo => anamneseGrupo.AnamneseSubGrupos)
                                                                   .ThenInclude(anamneseSubGrupo => anamneseSubGrupo.AnamneseSubGrupoQuestoes)
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

        public async Task<AnamneseGrupo>? BuscarPorID(int anamneseGrupoID)
        {
            var query = _dbSet.AsQueryable().Include(anamneseGrupo => anamneseGrupo.AnamneseSubGrupos)
                                            .ThenInclude(anamneseSubGrupo => anamneseSubGrupo.AnamneseSubGrupoQuestoes)
                                            .ThenInclude(anamneseSubGrupoQuestoes => anamneseSubGrupoQuestoes.AnamneseSubGrupoQuestaoOpcoes);

            var anamneseGrupo = await query.FirstOrDefaultAsync(where => where.AnamneseGrupoId == anamneseGrupoID);
            return anamneseGrupo;
        }
    }
}
