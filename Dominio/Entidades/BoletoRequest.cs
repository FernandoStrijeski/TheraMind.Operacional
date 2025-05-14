using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class BoletoRequest
    {
        public string NomeCliente { get; set; }
        public string DocumentoCliente { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        public decimal Valor { get; set; }
        public DateTime Vencimento { get; set; }
        public string NumeroDocumento { get; set; }
    }

}
