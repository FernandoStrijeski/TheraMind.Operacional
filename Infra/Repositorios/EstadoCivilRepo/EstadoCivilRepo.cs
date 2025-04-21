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
    public class EstadoCivilRepo : BaseRepositorio<EstadoCivil>, IEstadoCivilRepo
    {
        public EstadoCivilRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<EstadoCivil>> BuscarPorNome(string nome)
        {
            var query = _dbSet.Where(
                tabela => tabela.Descricao.Contains(nome)
                        
            ).AsNoTracking();

            return await query.ToListAsync();
        }

        public async Task<EstadoCivil>? BuscarPorID(string estadoCivilID)
        {
            var query = _dbSet.AsQueryable();
            var pais = await query.FirstOrDefaultAsync(where => where.EstadoCivilId == estadoCivilID);
            return pais;
        }
    }
}
