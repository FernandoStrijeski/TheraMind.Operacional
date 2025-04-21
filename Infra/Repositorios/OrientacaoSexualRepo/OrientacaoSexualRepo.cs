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
    public class OrientacaoSexualRepo : BaseRepositorio<OrientacaoSexual>, IOrientacaoSexualRepo
    {
        public OrientacaoSexualRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<OrientacaoSexual>? BuscarPorID(int orientacaoSexualID)
        {
            var query = _dbSet.AsQueryable();
            var retorno = await query.FirstOrDefaultAsync(where => where.OrientacaoSexualId == orientacaoSexualID);
            return retorno;
        }

        public async Task<List<OrientacaoSexual>> BuscarPorNome(string nome)
        {
            var query = _dbSet.Where(
                tabela => tabela.Descricao.Contains(nome)
            ).AsNoTracking();

            return await query.ToListAsync();
        }
    }
}
