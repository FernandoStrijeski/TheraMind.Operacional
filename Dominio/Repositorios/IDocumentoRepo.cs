using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;

namespace Dominio.Repositorios
{
    public interface IDocumentoRepo : IBaseRepositorio<Documento>
    {
        Task<List<Documento>> BuscarTodosDocumentosPorCandidatoID(int candidatoID);
        Task<List<Documento>> BuscarDocumentosPorTipo(int candidatoID, string tipoDocumento);
        Task<Documento>? BuscarDocumentoPorID(int documentoID);        
    }
}
