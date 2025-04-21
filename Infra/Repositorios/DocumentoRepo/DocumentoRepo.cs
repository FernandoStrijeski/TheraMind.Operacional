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
    public class DocumentoRepo : BaseRepositorio<Documento>, IDocumentoRepo
    {
        public DocumentoRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<Documento>> BuscarTodosDocumentosPorCandidatoID(int candidatoID)
        {
            var query = _dbSet.Where(
                tabela => tabela.CandidatoId == candidatoID
            ).AsNoTracking();

            return await query.ToListAsync();

        }

        public async Task<List<Documento>>? BuscarDependentePorID(int documentoID)
        {
            var query = _dbSet.Where(
                tabela => tabela.DocumentoId == documentoID
            ).AsNoTracking();

            return await query.ToListAsync();

        }

        public async Task<List<Documento>> BuscarDocumentosPorTipo(int candidatoID, string tipoDocumento)
        {
            var query = _dbSet.Where(
                tabela => tabela.CandidatoId == candidatoID && tabela.TipoDocumento.Descricao.ToUpper().Contains(tipoDocumento.ToUpper())

            ).AsNoTracking();

            return await query.ToListAsync();
        }

        public async Task<Documento>? BuscarDocumentoPorID(int documentoID)
        {
            var query = _dbSet.AsQueryable();
            var retorno = await query.FirstOrDefaultAsync(where => where.DocumentoId == documentoID);
            return retorno;
        }
    }
}
