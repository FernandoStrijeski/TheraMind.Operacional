using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dominio.EmpresasAssinaturas;
using Dominio.Entidades;
using Infra.Context;
using Infra.Core.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Infra.EmpresasAssinaturas
{
    public class EmpresaAssinaturaRepo : BaseRepositorio<EmpresaAssinatura, Guid>, IEmpresaAssinaturaRepo
    {
        public EmpresaAssinaturaRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<EmpresaAssinatura>> BuscarFiltros(
            Expression<Func<EmpresaAssinatura, bool>> filtro = null,
            Func<IQueryable<EmpresaAssinatura>, IOrderedQueryable<EmpresaAssinatura>> orderBy = null,
            int skip = 0,
            int take = 0
            )
        {
            IQueryable<EmpresaAssinatura> query = _dbSet.AsNoTracking()
                .Include(empresaAssinatura => empresaAssinatura.Empresa)
                .Include(empresaAssinatura => empresaAssinatura.Plano);

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

        public async Task<EmpresaAssinatura>? BuscarPorID(Guid empresaAssinaturaID)
        {
            var query = _dbSet.AsQueryable().Include(empresaAssinatura => empresaAssinatura.Plano);
            var plano = await query.FirstOrDefaultAsync(where => where.EmpresaAssinaturaId == empresaAssinaturaID);
            return plano;
        }

        public async Task<List<EmpresaAssinatura>> BuscarPorIdEmpresa(Guid empresaID)
        {
            IQueryable<EmpresaAssinatura> query = _dbSet.AsNoTracking().Include(empresaAssinatura => empresaAssinatura.Plano); ;
            query = query.Where(where => where.EmpresaId == empresaID);
            return await query.ToListAsync();
        }
    }
}
