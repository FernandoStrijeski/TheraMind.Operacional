using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.AnamneseSubGrupos
{
    public interface IAnamneseSubGrupoRepo : IBaseRepositorio<AnamneseSubGrupo>
    {
        Task<List<AnamneseSubGrupo>> BuscarFiltros(
            Expression<Func<AnamneseSubGrupo, bool>> filtro = null,
            Func<IQueryable<AnamneseSubGrupo>, IOrderedQueryable<AnamneseSubGrupo>> orderBy = null,
            int skip = 0,
            int take = 0
            );        
        Task<AnamneseSubGrupo>? BuscarPorID(int anamneseSubGrupoID);
    }
}
