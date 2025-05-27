using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dominio.EmpresaFaturas;
using Dominio.Entidades;
using Infra.Context;
using Infra.Core.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Infra.EmpresaFaturas
{
    public class EmpresaFaturaRepo : BaseRepositorio<EmpresaFatura>, IEmpresaFaturaRepo
    {
        public EmpresaFaturaRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<EmpresaFatura>> BuscarFiltros(
            Expression<Func<EmpresaFatura, bool>> filtro = null,
            Func<IQueryable<EmpresaFatura>, IOrderedQueryable<EmpresaFatura>> orderBy = null,
            int skip = 0,
            int take = 0
            )
        {
            IQueryable<EmpresaFatura> query = _dbSet.AsNoTracking();

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

        public async Task<EmpresaFatura>? BuscarPorID(int empresaFaturaID)
        {
            var query = _dbSet.AsQueryable();
            var plano = await query.FirstOrDefaultAsync(where => where.EmpresaFaturaId == empresaFaturaID);
            return plano;
        }
    }
}
