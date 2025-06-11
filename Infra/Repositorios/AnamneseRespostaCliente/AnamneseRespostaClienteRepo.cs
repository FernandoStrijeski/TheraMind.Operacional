using Dominio.AnamneseRespostaClientes;
using Dominio.Entidades;
using Infra.Context;
using Infra.Core.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infra.AnamneseRespostaClientes
{
    public class AnamneseRespostaClienteRepo : BaseRepositorio<AnamneseRespostaCliente, int>, IAnamneseRespostaClienteRepo
    {
        public AnamneseRespostaClienteRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<AnamneseRespostaCliente>> BuscarFiltros(
            Expression<Func<AnamneseRespostaCliente, bool>> filtro = null,
            Func<IQueryable<AnamneseRespostaCliente>, IOrderedQueryable<AnamneseRespostaCliente>> orderBy = null,
            int skip = 0,
            int take = 0
            )
        {
            IQueryable<AnamneseRespostaCliente> query = _dbSet.AsNoTracking();

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

        public async Task<AnamneseRespostaCliente>? BuscarPorID(int anamneseSubGrupoQuestaoID)
        {
            var query = _dbSet.AsQueryable();

            var anamneseRespostaCliente = await query.FirstOrDefaultAsync(where => where.AnamneseSubGrupoQuestaoId == anamneseSubGrupoQuestaoID);
            return anamneseRespostaCliente;
        }
    }
}
