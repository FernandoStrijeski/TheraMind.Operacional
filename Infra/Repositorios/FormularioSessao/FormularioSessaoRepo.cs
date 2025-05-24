using Dominio.AgendasSessoes;
using Dominio.Entidades;
using Dominio.FormulariosSessoes;
using Infra.Context;
using Infra.Core.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infra.FormulariosSessoes
{
    public class FormularioSessaoRepo : BaseRepositorio<FormularioSessao>, IFormularioSessaoRepo
    {
        public FormularioSessaoRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<FormularioSessao>> BuscarFiltros(
            Expression<Func<FormularioSessao, bool>> filtro = null,
            Func<IQueryable<FormularioSessao>, IOrderedQueryable<FormularioSessao>> orderBy = null,
            int skip = 0,
            int take = 0
            )
        {
            IQueryable<FormularioSessao> query = _dbSet.AsNoTracking().Include(formularioSessao => formularioSessao.FormularioSessaoCampos);

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

        public async Task<FormularioSessao>? BuscarPorID(int formularioSessaoID)
        {
            var query = _dbSet.AsQueryable().Include(formularioSessao => formularioSessao.FormularioSessaoCampos);

            var formularioSessao = await query.FirstOrDefaultAsync(where => where.FormularioSessaoId == formularioSessaoID);
            return formularioSessao;
        }
    }
}
