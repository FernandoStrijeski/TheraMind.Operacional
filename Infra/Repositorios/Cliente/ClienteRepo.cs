using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dominio.Clientes;
using Dominio.Entidades;
using Infra.Context;
using Infra.Core.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Infra.Clientes
{
    public class ClienteRepo : BaseRepositorio<Cliente, Guid>, IClienteRepo
    {
        public ClienteRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<Cliente>> BuscarFiltros(
            Expression<Func<Cliente, bool>> filtro = null,
            Func<IQueryable<Cliente>, IOrderedQueryable<Cliente>> orderBy = null,
            int skip = 0,
            int take = 0
            )
        {
            IQueryable<Cliente> query = _dbSet.AsNoTracking();

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

        public async Task<Cliente>? BuscarPorID(Guid clienteID)
        {
            var query = _dbSet.AsQueryable();
            var cliente = await query.FirstOrDefaultAsync(where => where.ClienteId == clienteID);
            return cliente;
        }
    }
}
