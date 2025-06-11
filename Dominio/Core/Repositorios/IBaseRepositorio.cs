using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Dominio.Core.Repositorios
{
    public interface IBaseRepositorio<TEntidade, TChave> where TEntidade : class
    {
        Task<List<TEntidade>> Buscar(
            Expression<Func<TEntidade, bool>> filtro = null,
            Func<IQueryable<TEntidade>, IOrderedQueryable<TEntidade>> orderBy = null,
            int skip = 0,
            int take = 0
        );
        Task<IQueryable<TEntidade>> BuscarTodos();
        Task Adicionar(TEntidade entidade);
        Task Atualizar(TEntidade entidade);
        Task Deletar(TChave id);
    }
}
