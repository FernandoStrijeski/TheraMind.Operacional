namespace API.Operacional.Core.Util
{
    using API.modelos.InputModels;
    using API.Operacional.modelos.ViewModels;
    using BoletoNetCore;
    using QuestPDF.Fluent;
    using QuestPDF.Helpers;
    using QuestPDF.Infrastructure;
    using System.Drawing;
    using System.Drawing.Imaging;
    using ZXing;
    using ZXing.Common;

    public class BoletoPdfGenerator
    {
        
        public BoletoGeradoViewModel GerarBoletoPdf(CriarBoletoInputModel criarBoletoInputModel)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            // 1. Configurar o banco (Sicredi, neste exemplo)
            var banco = Banco.Instancia(criarBoletoInputModel.Banco);

            // 2. Cedente
            var beneficiario = new Beneficiario
            {
                CPFCNPJ = criarBoletoInputModel.Beneficiario.CPFCNPJ,
                Nome = criarBoletoInputModel.Beneficiario.Nome,
                Codigo = criarBoletoInputModel.Beneficiario.Codigo,
                ContaBancaria = new ContaBancaria
                {
                    Agencia = criarBoletoInputModel.Beneficiario.ContaBancaria.Agencia,
                    DigitoAgencia = criarBoletoInputModel.Beneficiario.ContaBancaria.DigitoAgencia,
                    Conta = criarBoletoInputModel.Beneficiario.ContaBancaria.Conta,
                    DigitoConta = criarBoletoInputModel.Beneficiario.ContaBancaria.DigitoConta,
                    CarteiraPadrao = criarBoletoInputModel.Beneficiario.ContaBancaria.CarteiraPadrao,
                    VariacaoCarteiraPadrao = criarBoletoInputModel.Beneficiario.ContaBancaria.VariacaoCarteiraPadrao, // necessário sicredi!
                    OperacaoConta = criarBoletoInputModel.Beneficiario.ContaBancaria.OperacaoConta, // valor fictício – confirme com seu gerente ou manual do banco
                    TipoCarteiraPadrao = criarBoletoInputModel.Beneficiario.ContaBancaria.TipoCarteiraPadrao,
                    TipoFormaCadastramento = criarBoletoInputModel.Beneficiario.ContaBancaria.TipoFormaCadastramento
                }
            };

            banco.Beneficiario = beneficiario;

            // 3. Pagador
            var pagador = new Pagador
            {
                Nome = criarBoletoInputModel.Boleto.Pagador.Nome,
                CPFCNPJ = criarBoletoInputModel.Boleto.Pagador.CPFCNPJ,
                Endereco = new Endereco
                {
                    LogradouroEndereco = criarBoletoInputModel.Boleto.Pagador.Endereco.LogradouroEndereco,
                    LogradouroNumero = criarBoletoInputModel.Boleto.Pagador.Endereco.LogradouroNumero,
                    Bairro = criarBoletoInputModel.Boleto.Pagador.Endereco.Bairro,
                    Cidade = criarBoletoInputModel.Boleto.Pagador.Endereco.Cidade,
                    UF = criarBoletoInputModel.Boleto.Pagador.Endereco.UF,
                    CEP = criarBoletoInputModel.Boleto.Pagador.Endereco.CEP
                }
            };

            // 4. Criar o boleto
            var boleto = new Boleto(banco)
            {
                Pagador = pagador,
                DataVencimento = criarBoletoInputModel.Boleto.DataVencimento,
                ValorTitulo = criarBoletoInputModel.Boleto.ValorTitulo,
                NossoNumero = criarBoletoInputModel.Boleto.NossoNumero,
                NumeroDocumento = criarBoletoInputModel.Boleto.NumeroDocumento,
                EspecieDocumento = criarBoletoInputModel.Boleto.EspecieDocumento,
                CodigoMotivoOcorrencia = criarBoletoInputModel.Boleto.CodigoMotivoOcorrencia,
            };

            boleto.ValidarDados();

            // 5. Gerar imagem do código de barras
            var barcodeImage = GerarCodigoDeBarras(boleto.CodigoBarra.CodigoDeBarras);

            // 6. Criar o PDF
            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Size(PageSizes.A4);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Content()
                        .Column(col =>
                        {
                            col.Item().Text($"Boleto Bancário - {banco.Nome}");
                            col.Item().Text($"Cedente: {beneficiario.Nome} ({beneficiario.CPFCNPJ})");
                            col.Item().Text($"Pagador: {pagador.Nome} ({pagador.CPFCNPJ})");
                            col.Item().Text($"Endereço: {pagador.Endereco.LogradouroEndereco}, {pagador.Endereco.LogradouroNumero} - {pagador.Endereco.Bairro} - {pagador.Endereco.Cidade}/{pagador.Endereco.UF} - {pagador.Endereco.CEP}");
                            col.Item().Text($"Nosso Número: {boleto.NossoNumero}");
                            col.Item().Text($"Número do Documento: {boleto.NumeroDocumento}");
                            col.Item().Text($"Data de Vencimento: {boleto.DataVencimento:dd/MM/yyyy}");
                            col.Item().Text($"Valor: R$ {boleto.ValorTitulo:N2}");
                            col.Item().Text($"Linha Digitável: {boleto.CodigoBarra.LinhaDigitavel}").Bold().FontSize(14);

                            col.Item().PaddingTop(20).Image(barcodeImage);
                        });
                });
            });

            return new BoletoGeradoViewModel(boleto.NossoNumero, pdf.GeneratePdf());            
        }

        //public byte[] GerarBoletoPdf(CriarBoletoInputModel criarBoletoInputModel)
        //{
        //    QuestPDF.Settings.License = LicenseType.Community;

        //    // 1. Configurar o banco (Sicredi, neste exemplo)
        //    var banco = Banco.Instancia(Bancos.Sicredi);

        //    // 2. Cedente
        //    var beneficiario = new Beneficiario
        //    {                
        //        CPFCNPJ = "12345678000195",
        //        Nome = "EMPRESA SICREDI TESTE",
        //        Codigo = "12345",
        //        ContaBancaria = new ContaBancaria
        //        {
        //            Agencia = "1234",
        //            DigitoAgencia = "1",
        //            Conta = "56789",
        //            DigitoConta = "0",
        //            CarteiraPadrao = "1",
        //            VariacaoCarteiraPadrao = "A", // necessário sicredi!
        //            OperacaoConta = "01", // valor fictício – confirme com seu gerente ou manual do banco
        //            TipoCarteiraPadrao = TipoCarteira.CarteiraCobrancaSimples,
        //            TipoFormaCadastramento = TipoFormaCadastramento.ComRegistro
        //        }
        //    };

        //    banco.Beneficiario = beneficiario;

        //    // 3. Pagador
        //    var pagador = new Pagador
        //    {                
        //        Nome = "CLIENTE TESTE",
        //        CPFCNPJ = "98765432100",
        //        Endereco = new Endereco
        //        {
        //            LogradouroEndereco = "Rua Central",
        //            LogradouroNumero = "100",
        //            Bairro = "Centro",
        //            Cidade = "Porto Alegre",
        //            UF = "RS",
        //            CEP = "90000000"
        //        }
        //    };            

        //    // 4. Criar o boleto
        //    var boleto = new Boleto(banco)
        //    {
        //        Pagador = pagador,
        //        DataVencimento = DateTime.Today.AddDays(5),
        //        ValorTitulo = 150.00m,
        //        NossoNumero = "12345678",
        //        NumeroDocumento = "1001",
        //        EspecieDocumento = TipoEspecieDocumento.DM,
        //        CodigoMotivoOcorrencia = "01",
        //    };

        //    boleto.ValidarDados();

        //    // 5. Gerar imagem do código de barras
        //    var barcodeImage = GerarCodigoDeBarras(boleto.CodigoBarra.CodigoDeBarras);

        //    // 6. Criar o PDF
        //    var pdf = Document.Create(container =>
        //    {
        //        container.Page(page =>
        //        {
        //            page.Margin(30);
        //            page.Size(PageSizes.A4);
        //            page.DefaultTextStyle(x => x.FontSize(12));

        //            page.Content()
        //                .Column(col =>
        //                {
        //                    col.Item().Text($"Boleto Bancário - {banco.Nome}");
        //                    col.Item().Text($"Cedente: {beneficiario.Nome} ({beneficiario.CPFCNPJ})");
        //                    col.Item().Text($"Pagador: {pagador.Nome} ({pagador.CPFCNPJ})");
        //                    col.Item().Text($"Endereço: {pagador.Endereco.LogradouroEndereco}, {pagador.Endereco.LogradouroNumero} - {pagador.Endereco.Bairro} - {pagador.Endereco.Cidade}/{pagador.Endereco.UF} - {pagador.Endereco.CEP}");
        //                    col.Item().Text($"Nosso Número: {boleto.NossoNumero}");
        //                    col.Item().Text($"Número do Documento: {boleto.NumeroDocumento}");
        //                    col.Item().Text($"Data de Vencimento: {boleto.DataVencimento:dd/MM/yyyy}");
        //                    col.Item().Text($"Valor: R$ {boleto.ValorTitulo:N2}");
        //                    col.Item().Text($"Linha Digitável: {boleto.CodigoBarra.LinhaDigitavel}").Bold().FontSize(14);

        //                    col.Item().PaddingTop(20).Image(barcodeImage);
        //                });
        //        });
        //    });

        //    return pdf.GeneratePdf();
        //}

        private byte[] GerarCodigoDeBarras(string codigo)
        {
            var writer = new BarcodeWriterPixelData
            {
                Format = BarcodeFormat.ITF,
                Options = new EncodingOptions
                {
                    Height = 60,
                    Width = 400,
                    Margin = 10,
                    PureBarcode = true
                }
            };

            var pixelData = writer.Write(codigo);

            using var bitmap = new Bitmap(pixelData.Width, pixelData.Height, PixelFormat.Format32bppRgb);
            var bitmapData = bitmap.LockBits(
                new Rectangle(0, 0, pixelData.Width, pixelData.Height),
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppRgb);

            System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
            bitmap.UnlockBits(bitmapData);

            using var ms = new MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }
    }

}
