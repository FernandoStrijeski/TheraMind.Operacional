using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Context;
using Infra.Core.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorios
{
    public class FilialRepo : BaseRepositorio<Filial>, IFilialRepo
    {
        public FilialRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<Filial>> BuscarFiltros(
           Expression<Func<Filial, bool>> filtro = null,
           Func<IQueryable<Filial>, IOrderedQueryable<Filial>> orderBy = null,
           int skip = 0,
           int take = 0
           )
        {
            IQueryable<Filial> query = _dbSet.AsNoTracking().Include(filial => filial.Empresa);

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


        public async Task<Filial>? BuscarPorID(Guid empresaID, int filialID)
        {
            var query = _dbSet.AsQueryable().Include(filial => filial.Empresa);
            var retorno = await query.FirstOrDefaultAsync(where => where.EmpresaId == empresaID && where.FilialId == filialID);
            return retorno;
        }

        public async Task<List<Filial>> BuscarTodasPorEmpresa(Guid empresaID)
        {
            var query = _dbSet.Where(
                tabela => tabela.EmpresaId == empresaID
            ).AsNoTracking().Include(filial => filial.Empresa);

            return await query.ToListAsync();
        }
    }
}
