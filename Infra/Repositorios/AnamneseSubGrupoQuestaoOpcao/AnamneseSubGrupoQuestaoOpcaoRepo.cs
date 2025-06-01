using Dominio.AnamneseSubGrupoQuestaoOpcoes;
using Dominio.Entidades;
using Infra.Context;
using Infra.Core.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infra.AnamneseSubGrupoQuestaoOpcoes
{
    public class AnamneseSubGrupoQuestaoOpcaoRepo : BaseRepositorio<AnamneseSubGrupoQuestaoOpcao>, IAnamneseSubGrupoQuestaoOpcaoRepo
    {
        public AnamneseSubGrupoQuestaoOpcaoRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<AnamneseSubGrupoQuestaoOpcao>> BuscarFiltros(
            Expression<Func<AnamneseSubGrupoQuestaoOpcao, bool>> filtro = null,
            Func<IQueryable<AnamneseSubGrupoQuestaoOpcao>, IOrderedQueryable<AnamneseSubGrupoQuestaoOpcao>> orderBy = null,
            int skip = 0,
            int take = 0
            )
        {
            IQueryable<AnamneseSubGrupoQuestaoOpcao> query = _dbSet.AsNoTracking();

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

        public async Task<AnamneseSubGrupoQuestaoOpcao>? BuscarPorID(int anamneseSubGrupoQuestaoOpcaoID)
        {
            var query = _dbSet.AsQueryable();

            var anamneseGrupo = await query.FirstOrDefaultAsync(where => where.AnamneseSubGrupoQuestaoOpcaoId == anamneseSubGrupoQuestaoOpcaoID);
            return anamneseGrupo;
        }
    }
}
