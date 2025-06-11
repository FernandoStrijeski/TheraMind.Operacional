using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.Repositorios
{
    public interface IFilialRepo : IBaseRepositorio<Filial, int>
    {
        Task<List<Filial>> BuscarFiltros(
            Expression<Func<Filial, bool>> filtro = null,
            Func<IQueryable<Filial>, IOrderedQueryable<Filial>> orderBy = null,
            int skip = 0,
            int take = 0
            );

        Task<Filial>? BuscarPorID(int filialID);
        Task<List<Filial>> BuscarTodasPorEmpresa(Guid empresaID);
    }
}
