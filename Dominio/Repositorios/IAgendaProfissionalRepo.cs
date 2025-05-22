using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.AgendasProfissionais
{
    public interface IAgendaProfissionalRepo : IBaseRepositorio<AgendaProfissional>
    {
        Task<List<AgendaProfissional>> BuscarFiltros(
            Expression<Func<AgendaProfissional, bool>> filtro = null,
            Func<IQueryable<AgendaProfissional>, IOrderedQueryable<AgendaProfissional>> orderBy = null,
            int skip = 0,
            int take = 0
            );        
        Task<AgendaProfissional>? BuscarPorID(int agendaProfissionalID);
    }
}
