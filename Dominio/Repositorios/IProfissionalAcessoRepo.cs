using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.ProfissionaisAcessos
{
    public interface IProfissionalAcessoRepo : IBaseRepositorio<ProfissionalAcesso>
    {
        Task<List<ProfissionalAcesso>> BuscarFiltros(
            Expression<Func<ProfissionalAcesso, bool>> filtro = null,
            Func<IQueryable<ProfissionalAcesso>, IOrderedQueryable<ProfissionalAcesso>> orderBy = null,
            int skip = 0,
            int take = 0
            );        
        Task<ProfissionalAcesso>? BuscarPorID(int profissionalAcessoID);

        Task<List<ProfissionalAcesso>> BuscarPorIDProfissional(Guid profissionalID);
    }
}
