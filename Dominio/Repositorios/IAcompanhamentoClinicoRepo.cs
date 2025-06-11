using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.AcompanhamentosClinicos
{
    public interface IAcompanhamentoClinicoRepo : IBaseRepositorio<AcompanhamentoClinico, Guid>
    {
        Task<List<AcompanhamentoClinico>> BuscarFiltros(
            Expression<Func<AcompanhamentoClinico, bool>> filtro = null,
            Func<IQueryable<AcompanhamentoClinico>, IOrderedQueryable<AcompanhamentoClinico>> orderBy = null,
            int skip = 0,
            int take = 0
            );
        Task<AcompanhamentoClinico>? BuscarPorID(Guid acompanhamentoClinicoID);
    }
}
