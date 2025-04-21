using System;
using System.Globalization;

namespace Dominio.Core.Utils
{
    public static class Validador
    {
        public static bool ENumerico(string x)
        {
            return (long.TryParse(x, out var val) && val > 0);
        }

        public static bool EDataValida(string x)
        {
            return DateTime.TryParse(
                x,
                new CultureInfo("pt-BR"),
                DateTimeStyles.None,
                out DateTime data
            );
        }
    }
}
