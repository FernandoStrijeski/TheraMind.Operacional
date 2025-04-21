using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Context;
using Infra.Core.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorios
{
    public class FilialRepo : BaseRepositorio<Filial>, IFilialRepo
    {
        public FilialRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<Filial>? BuscarPorID(Guid empresaID, int filialID)
        {
            var query = _dbSet.AsQueryable();
            var retorno = await query.FirstOrDefaultAsync(where => where.EmpresaId == empresaID && where.FilialId == filialID);
            return retorno;
        }

        public async Task<List<Filial>> BuscarTodasPorEmpresa(Guid empresaID)
        {
            var query = _dbSet.Where(
                tabela => tabela.EmpresaId == empresaID
            ).AsNoTracking();

            return await query.ToListAsync();
        }
    }
}
