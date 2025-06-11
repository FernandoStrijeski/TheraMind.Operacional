using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.AnamneseGrupos
{
    public interface IAnamneseGrupoRepo : IBaseRepositorio<AnamneseGrupo, int>
    {
        Task<List<AnamneseGrupo>> BuscarFiltros(
            Expression<Func<AnamneseGrupo, bool>> filtro = null,
            Func<IQueryable<AnamneseGrupo>, IOrderedQueryable<AnamneseGrupo>> orderBy = null,
            int skip = 0,
            int take = 0
            );        
        Task<AnamneseGrupo>? BuscarPorID(int anamneseGrupoID);
    }
}
