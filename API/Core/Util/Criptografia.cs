using System.Security.Cryptography;
using System.Text;

namespace API.Core.Utils
{
    public static class Criptografia
    {
        private static Aes CriarInstanciaAes(string token, string chave)
        {
            if (string.IsNullOrEmpty(token) || (token.Length != 16 && token.Length != 24 && token.Length != 32))
                throw new Exception("O Token deve possuir 16, 24 ou 32 caracteres.");
            if (string.IsNullOrEmpty(chave) || chave.Length != 16)
                throw new Exception("A Chave deve possuir 16 caracteres.");

            if (!ValidarChaveEToken(chave, token))
                throw new Exception("A Chave e o Token devem possuir apenas letras ou números.");

            Aes aes = Aes.Create();
            aes.Key = Encoding.ASCII.GetBytes(token);
            aes.IV = Encoding.ASCII.GetBytes(chave);
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            return aes;
        }

        public static bool ValidarChaveEToken(string chave, string token) => chave.All(char.IsLetterOrDigit) && token.All(char.IsLetterOrDigit);        

        private static string ArrayBytesToHexString(byte[] conteudo) => BitConverter.ToString(conteudo).Replace("-", "");

        private static byte[] HexStringToArrayBytes(string conteudo)
        {
            int length = conteudo.Length / 2;
            byte[] resultado = new byte[length];

            for (int i = 0; i < length; i++)
                resultado[i] = Convert.ToByte(conteudo.Substring(i * 2, 2), 16);

            return resultado;
        }

        public static string Encriptar(string token, string chave, string conteudo)
        {
            if (string.IsNullOrWhiteSpace(conteudo))
                throw new Exception("O conteúdo a ser encriptado não pode ser uma string vazia.");

            using (Aes aes = CriarInstanciaAes(token, chave))
            using (MemoryStream memoryStream = new MemoryStream())
            using (ICryptoTransform encryptor = aes.CreateEncryptor())
            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
            using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
            {
                streamWriter.Write(conteudo);
                streamWriter.Flush();
                cryptoStream.FlushFinalBlock();
                return ArrayBytesToHexString(memoryStream.ToArray());
            }
        }

        public static string Decriptar(string token, string chave, string conteudo)
        {
            if (string.IsNullOrWhiteSpace(conteudo))
                throw new Exception("O conteúdo a ser decriptado não pode ser uma string vazia.");
            if (conteudo.Length % 2 != 0)
                throw new Exception("O conteúdo a ser decriptado é inválido.");

            using (Aes aes = CriarInstanciaAes(token, chave))
            using (MemoryStream memoryStream = new MemoryStream(HexStringToArrayBytes(conteudo)))
            using (ICryptoTransform decryptor = aes.CreateDecryptor())
            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
            using (StreamReader streamReader = new StreamReader(cryptoStream))
            {
                return streamReader.ReadToEnd();
            }
        }

        public static Dictionary<string, string> EncriptarToDictionary(string token, string chave, string queryString)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            queryString = queryString.Replace("?", "");
            char[] separadoresPrincipais = { '&', '+' };
            char[] separadoresValores = { '=', ':' };

            foreach (char separadorPrincipal in separadoresPrincipais)
            {
                if (queryString.Contains(separadorPrincipal))
                {
                    string[] pares = queryString.Split(separadorPrincipal);
                    foreach (char separadorValor in separadoresValores)
                    {
                        if (queryString.Contains(separadorValor))
                        {
                            foreach (string par in pares)
                            {
                                string[] chaveValor = par.Split(separadorValor);
                                if (chaveValor.Length == 2)
                                {
                                    dictionary.Add(Encriptar(token, chave, chaveValor[0]), Encriptar(token, chave, chaveValor[1]));
                                }
                            }
                        }
                    }
                }
            }
            return dictionary;
        }

        public static Dictionary<string, string> DecriptarToDictionary(string token, string chave, string queryString)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            queryString = Decriptar(token, chave, queryString);
            queryString = queryString.Replace("?", "");
            char[] separadoresPrincipais = { '&', '+' };
            char[] separadoresValores = { '=', ':' };

            foreach (char separadorPrincipal in separadoresPrincipais)
            {
                if (queryString.Contains(separadorPrincipal))
                {
                    string[] pares = queryString.Split(separadorPrincipal);
                    foreach (char separadorValor in separadoresValores)
                    {
                        if (queryString.Contains(separadorValor))
                        {
                            foreach (string par in pares)
                            {
                                string[] chaveValor = par.Split(separadorValor);
                                if (chaveValor.Length == 2)
                                {
                                    dictionary.Add(chaveValor[0], chaveValor[1]);
                                }
                            }
                        }
                    }
                }
            }
            return dictionary;
        }
    }

}