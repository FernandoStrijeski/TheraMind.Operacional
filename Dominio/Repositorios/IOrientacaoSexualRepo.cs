using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.Repositorios
{
    public interface IOrientacaoSexualRepo : IBaseRepositorio<OrientacaoSexual>
    {
        Task<List<OrientacaoSexual>> BuscarFiltros(
        Expression<Func<OrientacaoSexual, bool>> filtro = null,
        Func<IQueryable<OrientacaoSexual>, IOrderedQueryable<OrientacaoSexual>> orderBy = null,
        int skip = 0,
        int take = 0
        );
        Task<OrientacaoSexual> BuscarPorID(int orientacaoSexualID);
    }
}
