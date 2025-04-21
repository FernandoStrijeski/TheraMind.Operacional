using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominio.Core.Utils
{
    public static class Conversores
    {
        public static DateTime ConverterStringParaDateTime(string data)
        {
            return DateTime.ParseExact(
                data,
                "dd/MM/yyyy",
                System.Globalization.CultureInfo.InvariantCulture
            );
        }

        public static string TratarCnpj(string cnpj)
        {
            cnpj = cnpj.Replace(".", "").Replace("/", "").Replace("-", "").PadLeft(14, '0');
            return cnpj;
        }

        public static string TratarCpf(string cpf)
        {
            cpf = cpf.Replace(".", "").Replace("-", "").PadLeft(11, '0');
            return cpf;
        }

        public static string TratarRg(string rg)
        {
            while (rg.Contains("."))
            {
                rg = rg.Replace(".", "");
            }
            while (rg.Contains("-"))
            {
                rg = rg.Replace("-", "");
            }
            return rg;
        }

        public static string TratarCep(string cep)
        {
            cep = cep.Replace("-", "").PadLeft(8, '0');
            return cep;
        }

        public static string TratarTelefone(string telefone)
        {
            telefone = telefone.Replace("(", "").Replace(")", "").Replace("-", "").PadLeft(10, '0');
            return telefone;
        }

        public static string TratarCelular(string celular)
        {
            celular = celular.Replace("(", "").Replace(")", "").Replace("-", "").PadLeft(11, '0');
            return celular;
        }

        public static string TratarCodigoEmpresa(string CodEmpresa)
        {
            return CodEmpresa; //.PadLeft(3, '0');
        }

        public static IEnumerable<string> DividirStringPorTamanho(string s, int n)
        {
            if (String.IsNullOrEmpty(s) || n < 1)
            {
                throw new ArgumentException();
            }

            return Enumerable.Range(0, s.Length / n).Select(i => s.Substring(i * n, n));
        }
    }
}
