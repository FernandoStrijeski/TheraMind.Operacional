using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.Clientes
{
    public interface IClienteRepo : IBaseRepositorio<Cliente, Guid>
    {
        Task<List<Cliente>> BuscarFiltros(
            Expression<Func<Cliente, bool>> filtro = null,
            Func<IQueryable<Cliente>, IOrderedQueryable<Cliente>> orderBy = null,
            int skip = 0,
            int take = 0
            );        
        Task<Cliente>? BuscarPorID(Guid clienteID);
    }
}
