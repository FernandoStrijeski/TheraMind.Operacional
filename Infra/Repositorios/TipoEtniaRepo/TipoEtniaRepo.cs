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
    public class TipoEtniaRepo : BaseRepositorio<TipoEtnia>, ITipoEtniaRepo
    {
        public TipoEtniaRepo(ApplicationDbContext contexto) : base(contexto) { }


        public async Task<List<TipoEtnia>> BuscarFiltros(
            Expression<Func<TipoEtnia, bool>> filtro = null,
            Func<IQueryable<TipoEtnia>, IOrderedQueryable<TipoEtnia>> orderBy = null,
            int skip = 0,
            int take = 0
            )
        {
            IQueryable<TipoEtnia> query = _dbSet.AsNoTracking();

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

        public async Task<TipoEtnia>? BuscarPorID(int tipoEtniaID)
        {
            var query = _dbSet.AsQueryable();
            var tipoEtnia = await query.FirstOrDefaultAsync(where => where.TipoEtniaId == tipoEtniaID);
            return tipoEtnia;
        }

        //public async Task<List<TipoEtnia>> BuscarPorNome(string nome)
        //{
        //    var query = _dbSet.Where(
        //        tabela => tabela.Descricao.Contains(nome)
        //    ).AsNoTracking();

        //    return await query.ToListAsync();
        //}
    }
}
