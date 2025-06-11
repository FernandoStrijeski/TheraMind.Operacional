using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.AnamneseSubGrupoQuestaoOpcoes
{
    public interface IAnamneseSubGrupoQuestaoOpcaoRepo : IBaseRepositorio<AnamneseSubGrupoQuestaoOpcao, int>
    {
        Task<List<AnamneseSubGrupoQuestaoOpcao>> BuscarFiltros(
            Expression<Func<AnamneseSubGrupoQuestaoOpcao, bool>> filtro = null,
            Func<IQueryable<AnamneseSubGrupoQuestaoOpcao>, IOrderedQueryable<AnamneseSubGrupoQuestaoOpcao>> orderBy = null,
            int skip = 0,
            int take = 0
            );        
        Task<AnamneseSubGrupoQuestaoOpcao>? BuscarPorID(int anamneseSubGrupoQuestaoOpcaoID);
    }
}
