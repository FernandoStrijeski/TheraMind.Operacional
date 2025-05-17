using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.Profissionais
{
    public interface IProfissionalRepo : IBaseRepositorio<Profissional>
    {
        Task<List<Profissional>> BuscarFiltros(
            Expression<Func<Profissional, bool>> filtro = null,
            Func<IQueryable<Profissional>, IOrderedQueryable<Profissional>> orderBy = null,
            int skip = 0,
            int take = 0
            );        
        Task<Profissional>? BuscarPorID(Guid profissionalID);
    }
}
