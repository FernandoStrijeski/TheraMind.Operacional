using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Context;
using Infra.Core.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.Repositorios
{
    public class EmpresaRepo : BaseRepositorio<Empresa>, IEmpresaRepo
    {
        public EmpresaRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<Empresa>? BuscarPorID(Guid empresaID)
        {
            var query = _dbSet.AsQueryable();
            var retorno = await query.FirstOrDefaultAsync(where => where.EmpresaId == empresaID);
            return retorno;
        }
    }
}
