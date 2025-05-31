using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.AnamneseSubGrupoQuestoes
{
    public interface IAnamneseSubGrupoQuestaoRepo : IBaseRepositorio<AnamneseSubGrupoQuestao>
    {
        Task<List<AnamneseSubGrupoQuestao>> BuscarFiltros(
            Expression<Func<AnamneseSubGrupoQuestao, bool>> filtro = null,
            Func<IQueryable<AnamneseSubGrupoQuestao>, IOrderedQueryable<AnamneseSubGrupoQuestao>> orderBy = null,
            int skip = 0,
            int take = 0
            );        
        Task<AnamneseSubGrupoQuestao>? BuscarPorID(int anamneseSubGrupoQuestaoID);
    }
}
