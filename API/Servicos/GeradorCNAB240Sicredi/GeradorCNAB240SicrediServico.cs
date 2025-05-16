using API.modelos.InputModels;
using API.Operacional.Core.Util;
using Dominio.Core.Repositorios;
using Infra.Servicos.MultiTenant;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace API.Servicos.GeradorCNAB240Sicredi
{
    public class GeradorCNAB240SicrediServico : ServicoBase, IGeradorCNAB240SicrediServico
    {
        private IConfiguration _configuration;
        private IConnectionParamsServico _connectionParamsServico;

        public GeradorCNAB240SicrediServico(
            IConfiguration configuration,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
        }

        private int sequenciaLote = 1;
        private int sequenciaRegistro = 1;

        public byte[] GerarRemessaCnab240()
        {
            var sb = new StringBuilder();

            sb.AppendLine(GerarHeaderArquivo());
            sb.AppendLine(GerarHeaderLote());
            sb.AppendLine(GerarSegmentoA());
            sb.AppendLine(GerarSegmentoB());
            sb.AppendLine(GerarTrailerLote());
            sb.AppendLine(GerarTrailerArquivo());

            return Encoding.UTF8.GetBytes(sb.ToString());
        }

        private string GerarHeaderArquivo()
        {
            var sb = new StringBuilder();
            sb.Append("748"); // Código do banco
            sb.Append("0000"); // Lote
            sb.Append("0"); // Tipo de registro
            sb.Append("         "); // Filler
            sb.Append("2"); // Tipo inscrição (2=CNPJ)
            sb.Append("12345678000195".PadLeft(14, '0'));
            sb.Append("                    "); // Código convênio (20)
            sb.Append("1234"); // Agência
            sb.Append("1"); // Dígito
            sb.Append("56789".PadLeft(12, '0'));
            sb.Append("0"); // Dígito conta
            sb.Append(" "); // Dígito ag/conta
            sb.Append("EMPRESA SICREDI TESTE".PadRight(30));
            sb.Append("SICREDI".PadRight(30));
            sb.Append("          "); // CNAB
            sb.Append("1"); // Código remessa
            sb.Append(DateTime.Now.ToString("ddMMyyyy"));
            sb.Append(DateTime.Now.ToString("HHmmss"));
            sb.Append("000001"); // NSA
            sb.Append("082"); // Layout
            sb.Append("     "); // Densidade
            sb.Append("                    "); // Reservado banco
            sb.Append("                    "); // Reservado empresa
            sb.Append("                             "); // CNAB
            return sb.ToString();
        }

        private string GerarHeaderLote()
        {
            var sb = new StringBuilder();
            sb.Append("748"); // Código banco
            sb.Append("0001"); // Número do lote
            sb.Append("1"); // Tipo registro
            sb.Append("C"); // Operação
            sb.Append("20"); // Tipo serviço (20 = fornecedor)
            sb.Append("01"); // Forma de lançamento (01 = conta corrente)
            sb.Append("042"); // Layout lote
            sb.Append(" "); // CNAB
            sb.Append("2"); // Tipo inscrição
            sb.Append("12345678000195".PadLeft(14, '0'));
            sb.Append("                    "); // Código convênio
            sb.Append("1234"); // Agência
            sb.Append("1"); // Dígito
            sb.Append("56789".PadLeft(12, '0'));
            sb.Append("0"); // Dígito conta
            sb.Append(" "); // Dígito ag/conta
            sb.Append("EMPRESA SICREDI TESTE".PadRight(30));
            sb.Append("MENSAGEM TESTE".PadRight(40));
            sb.Append("Rua Central".PadRight(30));
            sb.Append("100".PadLeft(5, '0'));
            sb.Append("Sala 1".PadRight(15));
            sb.Append("Porto Alegre".PadRight(20));
            sb.Append("90000".PadLeft(5, '0'));
            sb.Append("000"); // Compl. CEP
            sb.Append("RS");
            sb.Append("        "); // CNAB
            sb.Append("          "); // Ocorrências
            return sb.ToString();
        }

        private string GerarSegmentoA()
        {
            var sb = new StringBuilder();
            sb.Append("748"); // Banco
            sb.Append("0001"); // Lote
            sb.Append("3"); // Tipo de registro
            sb.Append(sequenciaRegistro.ToString().PadLeft(5, '0'));
            sb.Append("A"); // Segmento
            sb.Append("0"); // Tipo movimento
            sb.Append("00"); // Código instrução
            sb.Append("000"); // Câmara
            sb.Append("748"); // Banco Favorecido
            sb.Append("1234".PadLeft(5, '0'));
            sb.Append("1");
            sb.Append("98765".PadLeft(12, '0'));
            sb.Append("0");
            sb.Append(" ");
            sb.Append("CLIENTE TESTE".PadRight(30));
            sb.Append("DOC123456789".PadRight(20));
            sb.Append(DateTime.Today.AddDays(1).ToString("ddMMyyyy"));
            sb.Append("BRL");
            sb.Append("0000000000");
            sb.Append("0000000150000");
            sb.Append(" ".PadRight(20)); // Nosso número
            sb.Append("00000000"); // Data real
            sb.Append("0000000000000"); // Valor real
            sb.Append(" ".PadRight(40)); // Informação 2
            sb.Append("  "); // Finalidade DOC
            sb.Append("00000"); // Finalidade TED
            sb.Append("  ");
            sb.Append("   ");
            sb.Append("0");
            sb.Append("          ");
            sequenciaRegistro++;
            return sb.ToString();
        }

        private string GerarSegmentoB()
        {
            var sb = new StringBuilder();
            sb.Append("748");
            sb.Append("0001");
            sb.Append("3");
            sb.Append(sequenciaRegistro.ToString().PadLeft(5, '0'));
            sb.Append("B");
            sb.Append("   "); // CNAB
            sb.Append("1");
            sb.Append("98765432100".PadLeft(14, '0'));
            sb.Append("Rua Central".PadRight(30));
            sb.Append("100".PadLeft(5, '0'));
            sb.Append("Sala 1".PadRight(15));
            sb.Append("Centro".PadRight(15));
            sb.Append("Porto Alegre".PadRight(20));
            sb.Append("90000".PadLeft(5, '0'));
            sb.Append("000");
            sb.Append("RS");
            sb.Append("        "); // CNAB
            sb.Append("00000000"); // Vencimento
            sb.Append("0000000150000");
            sb.Append("0000000000000");
            sb.Append("0000000000000");
            sb.Append("0000000000000");
            sb.Append("0000000000000");
            sb.Append(" ".PadRight(15)); // Código/documento favorecido
            sb.Append("0"); // Aviso
            sb.Append("000000"); // Código UG
            sb.Append("00000000"); // ISPB
            sequenciaRegistro++;
            return sb.ToString();
        }

        private string GerarTrailerLote()
        {
            var sb = new StringBuilder();
            sb.Append("748");
            sb.Append("0001");
            sb.Append("5");
            sb.Append("         "); // CNAB
            sb.Append("000002"); // Registros no lote
            sb.Append("0000000150000");
            sb.Append("0000000000000");
            sb.Append("000000");
            sb.Append(" ".PadRight(165));
            sb.Append("          ");
            return sb.ToString();
        }

        private string GerarTrailerArquivo()
        {
            var sb = new StringBuilder();
            sb.Append("748");
            sb.Append("9999");
            sb.Append("9");
            sb.Append("         ");
            sb.Append("000001"); // Quantidade de lotes
            sb.Append("000006"); // Quantidade de registros (header + 2 segmentos + trailer do lote + header lote + trailer arquivo)
            sb.Append("000000");
            sb.Append(" ".PadRight(205));
            return sb.ToString();
        }

        public string EnviarRemessaParaSicredi(byte[] arquivoRemessa, string nomeArquivo, out byte[] arquivoRetorno)
        {
            using var sftp = new Renci.SshNet.SftpClient("sftp.sicredi.com.br", 22, "usuario", "senha");
            arquivoRetorno = null;

            try
            {
                sftp.Connect();

                // Enviar arquivo para /entrada
                using var ms = new MemoryStream(arquivoRemessa);
                sftp.UploadFile(ms, $"/entrada/{nomeArquivo}");

                // Aguardar retorno processado (em produção, use polling com delay ou WebHook)
                var retornoPath = $"/retorno/RET_{nomeArquivo}";
                if (sftp.Exists(retornoPath))
                {
                    using var retornoStream = new MemoryStream();
                    sftp.DownloadFile(retornoPath, retornoStream);
                    arquivoRetorno = retornoStream.ToArray();
                }

                sftp.Disconnect();
                return "";
            }
            catch (Exception ex)
            {
               return $"Erro SFTP: {ex.Message}";
            }
        }

    }
}
