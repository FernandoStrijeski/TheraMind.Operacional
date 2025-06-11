using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.Repositorios
{
    public interface ICidadeRepo : IBaseRepositorio<Cidade, int>
    {
        Task<List<Cidade>> BuscarFiltros(
          Expression<Func<Cidade, bool>> filtro = null,
          Func<IQueryable<Cidade>, IOrderedQueryable<Cidade>> orderBy = null,
          int skip = 0,
          int take = 0
          );
        Task<Cidade> BuscarPorID(int cidadeID);
        Task<Cidade> BuscarPorIBGE(int codigoIBGE);
    }
}
