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
    public class TipoDocumentoRepo : BaseRepositorio<TipoDocumento>, ITipoDocumentoRepo
    {
        public TipoDocumentoRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<TipoDocumento>? BuscarPorID(int tipoDocumentoID)
        {
            var query = _dbSet.AsQueryable();
            var tipoDocumento = await query.FirstOrDefaultAsync(where => where.TipoDocumentoId == tipoDocumentoID);
            return tipoDocumento;
        }

        public async Task<List<TipoDocumento>> BuscarPorNome(string nome)
        {
            var query = _dbSet.Where(
                tabela => tabela.Descricao.Contains(nome)
            ).AsNoTracking();

            return await query.ToListAsync();
        }
    }
}
