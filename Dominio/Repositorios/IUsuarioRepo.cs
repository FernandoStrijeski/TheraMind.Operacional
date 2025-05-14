using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.Repositorios
{
    public interface IUsuarioRepo : IBaseRepositorio<Usuario>
    {
        Task<List<Usuario>> BuscarFiltros(
            Expression<Func<Usuario, bool>> filtro = null,
            Func<IQueryable<Usuario>, IOrderedQueryable<Usuario>> orderBy = null,
            int skip = 0,
            int take = 0
            );        
        Task<Usuario>? BuscarPorID(Guid usuarioID);
    }
}
