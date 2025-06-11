using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.AnamneseRespostaClientes
{
    public interface IAnamneseRespostaClienteRepo : IBaseRepositorio<AnamneseRespostaCliente, int>
    {
        Task<List<AnamneseRespostaCliente>> BuscarFiltros(
            Expression<Func<AnamneseRespostaCliente, bool>> filtro = null,
            Func<IQueryable<AnamneseRespostaCliente>, IOrderedQueryable<AnamneseRespostaCliente>> orderBy = null,
            int skip = 0,
            int take = 0
            );        
        Task<AnamneseRespostaCliente>? BuscarPorID(int anamneseSubGrupoQuestaoID);
    }
}
