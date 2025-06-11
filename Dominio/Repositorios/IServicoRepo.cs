using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.Repositorios
{
    public interface IServicoRepo : IBaseRepositorio<Servico, int>
    {
        Task<List<Servico>> BuscarFiltros(
            Expression<Func<Servico, bool>> filtro = null,
            Func<IQueryable<Servico>, IOrderedQueryable<Servico>> orderBy = null,
            int skip = 0,
            int take = 0
            );        
        Task<Servico>? BuscarPorID(int servicoID);
    }
}
