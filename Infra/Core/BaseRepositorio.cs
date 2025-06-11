using Microsoft.EntityFrameworkCore;
using Dominio.Core.Repositorios;
using Infra.Context;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;

namespace Infra.Core.Repositorios
{
    public class BaseRepositorio<TDbEntidade, TChave> : IBaseRepositorio<TDbEntidade, TChave>
        where TDbEntidade : class
    {
        protected internal readonly ApplicationDbContext _contexto;
        protected internal readonly DbSet<TDbEntidade> _dbSet;

        public BaseRepositorio(ApplicationDbContext contexto)
        {
            _contexto = contexto;
            _dbSet = _contexto.Set<TDbEntidade>();
        }

        public virtual async Task<List<TDbEntidade>> Buscar(
    Expression<Func<TDbEntidade, bool>> filtro = null,
    Func<IQueryable<TDbEntidade>, IOrderedQueryable<TDbEntidade>> orderBy = null,
    int skip = 0,
    int take = 0
)
        {
            IQueryable<TDbEntidade> query = _dbSet.AsNoTracking();

            if (filtro != null)
            {
                query = query.Where(filtro);
            }

            if (skip > 0)
            {
                query = query.Skip(skip);
            }

            if (take > 0)
            {
                query = query.Take(take);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            //esse metodo inclui todos relacionamentos e pode criar recursão com repetições
            //query = IncluirRelacionamentos(query);

            return await query.ToListAsync();
        }

        public virtual async Task<IQueryable<TDbEntidade>> BuscarTodos()
        {
            IQueryable<TDbEntidade> query = _dbSet.AsQueryable().AsNoTracking();
            return await Task.Run(() => query);
        }

        public async Task Adicionar(TDbEntidade entidade)
        {
            await _dbSet.AddAsync(entidade);
        }

        public Task Atualizar(TDbEntidade entidade)
        {
            _dbSet.Update(entidade);

            return Task.CompletedTask;
        }

        public async Task Deletar(TChave id)
        {
            var entidade = await _dbSet.FindAsync(id);
            if (entidade != null)
            {
                _dbSet.Remove(entidade);
            }
        }

        //private IQueryable<TDbEntidade> IncluirRelacionamentos(IQueryable<TDbEntidade> query)
        //{
        //    var propriedadesDeNavegacao = _contexto.Model
        //        .FindEntityType(typeof(TDbEntidade))?
        //        .GetNavigations()
        //        .Select(n => n.Name);

        //    if (propriedadesDeNavegacao != null)
        //    {
        //        foreach (var propriedade in propriedadesDeNavegacao)
        //        {
        //            query = query.Include(propriedade);
        //        }
        //    }

        //    return query;
        //}

    }
}
