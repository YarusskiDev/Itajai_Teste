using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Teste_Sc.ViewModels;

namespace Teste_Sc.Servicos
{
    public class ExcelServico
    {
        public async Task CriaPlanilhaExcelEstados(List<EstadosViewModel> resultadoCompleto)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var file = new FileInfo(@"c:\ExcelTeste\TodosOsEstados.xlsx");
            await SaveExcelArquivoEstados(resultadoCompleto,file);

        }
        public async Task CriaPlanilhaExcelCidades(List<CidadesViewModel> resultSerializadoEstados)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var file = new FileInfo(@"c:\ExcelTeste\TodasCidades.xlsx");
            await SaveExcelArquivoPorCidades(resultSerializadoEstados, file);

        }
        private static async Task SaveExcelArquivoEstados(List<EstadosViewModel> resultSerializadoEstados, FileInfo file)
        {
            DeleteIfExists(file);
            string caminhoDaPastaExcel = @"C:\ExcelTeste";
            if (!Directory.Exists(caminhoDaPastaExcel))
            {
                Directory.CreateDirectory(caminhoDaPastaExcel);
            }
            using var package = new ExcelPackage(file);

            var ws = package.Workbook.Worksheets.Add("Planilha completa");

            var range = ws.Cells["A1"].LoadFromCollection(resultSerializadoEstados, true);  
            range.AutoFitColumns();

            await package.SaveAsync();

        }
        private static async Task SaveExcelArquivoPorCidades(List<CidadesViewModel> resultSerializadoCidades, FileInfo file)
        {
            DeleteIfExists(file);
            string caminhoDaPastaExcel = @"C:\ExcelTeste";
            if (!Directory.Exists(caminhoDaPastaExcel))
            {
                Directory.CreateDirectory(caminhoDaPastaExcel);
            }
            using var package = new ExcelPackage(file);

            var ws = package.Workbook.Worksheets.Add("Planilha com estados");

            var range = ws.Cells["A1"].LoadFromCollection(resultSerializadoCidades, true);
            range.AutoFitColumns();

            await package.SaveAsync();

        }
        private static void DeleteIfExists(FileInfo file)
        {
            if (file.Exists)
            {
                file.Delete();
            }
        }
    }
}
