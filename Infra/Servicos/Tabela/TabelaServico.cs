using Dominio.Service;
using Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;

namespace Infra.Servicos.Tabela
{
    public class TabelaServico : ITabelaService
    {
        private readonly ApplicationDbContext _context;

        public TabelaServico(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool TabelaExiste(string nomeDaTabela)
        {
            var connection = _context.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            try
            {
                return connection.GetSchema("Tables")
                    .AsEnumerable()
                    .Any(row => row["TABLE_NAME"].ToString().Equals(nomeDaTabela, StringComparison.OrdinalIgnoreCase));
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
        }
    }
}
