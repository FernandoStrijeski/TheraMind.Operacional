using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.EmpresasAssinaturas
{
    public interface IEmpresaAssinaturaRepo : IBaseRepositorio<EmpresaAssinatura, Guid>
    {
        Task<List<EmpresaAssinatura>> BuscarFiltros(
            Expression<Func<EmpresaAssinatura, bool>> filtro = null,
            Func<IQueryable<EmpresaAssinatura>, IOrderedQueryable<EmpresaAssinatura>> orderBy = null,
            int skip = 0,
            int take = 0
            );        
        Task<EmpresaAssinatura>? BuscarPorID(Guid empresaAssinaturaID);

        Task<List<EmpresaAssinatura>> BuscarPorIdEmpresa(Guid empresaID);
    }
}
