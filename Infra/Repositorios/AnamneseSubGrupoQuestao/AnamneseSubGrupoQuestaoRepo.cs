using Dominio.AnamneseSubGrupoQuestoes;
using Dominio.Entidades;
using Infra.Context;
using Infra.Core.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infra.AnamneseSubGrupoQuestoes
{
    public class AnamneseSubGrupoQuestaoRepo : BaseRepositorio<AnamneseSubGrupoQuestao, int>, IAnamneseSubGrupoQuestaoRepo
    {
        public AnamneseSubGrupoQuestaoRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<AnamneseSubGrupoQuestao>> BuscarFiltros(
            Expression<Func<AnamneseSubGrupoQuestao, bool>> filtro = null,
            Func<IQueryable<AnamneseSubGrupoQuestao>, IOrderedQueryable<AnamneseSubGrupoQuestao>> orderBy = null,
            int skip = 0,
            int take = 0
            )
        {
            IQueryable<AnamneseSubGrupoQuestao> query = _dbSet.AsNoTracking().Include(anamneseSubGrupoQuestao => anamneseSubGrupoQuestao.AnamneseSubGrupoQuestaoOpcoes);

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

        public async Task<AnamneseSubGrupoQuestao>? BuscarPorID(int anamneseSubGrupoQuestaoID)
        {
            var query = _dbSet.AsQueryable().Include(anamneseSubGrupoQuestao => anamneseSubGrupoQuestao.AnamneseSubGrupoQuestaoOpcoes);

            var anamneseGrupo = await query.FirstOrDefaultAsync(where => where.AnamneseSubGrupoQuestaoId == anamneseSubGrupoQuestaoID);
            return anamneseGrupo;
        }
    }
}
