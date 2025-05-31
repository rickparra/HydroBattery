using System;
using System.IO;
using System.Linq;
using System.Text;
using GlobalSolutionCSharp.Models;
using GlobalSolutionCSharp.Data;

namespace GlobalSolutionCSharp.Services
{
    // Serviço para exportar dados de falhas para CSV
    public static class ExportService
    {
        public static void ExportarFalhasParaCsv(string caminhoArquivo)
        {
            if (string.IsNullOrEmpty(caminhoArquivo))
            {
                throw new ArgumentException("Caminho do arquivo não pode ser vazio.");
            }

            var falhas = Repository.Falhas;
            if (!falhas.Any())
            {
                throw new InvalidOperationException("Não há falhas para exportar.");
            }

            var diretorio = Path.GetDirectoryName(caminhoArquivo);
            if (!string.IsNullOrEmpty(diretorio) && !Directory.Exists(diretorio))
            {
                Directory.CreateDirectory(diretorio);
            }

            var csv = new StringBuilder();
            csv.AppendLine("ID,Data Início,Data Fim,Local,Descrição,Tipo,Status,Impacto Usuários,Impacto Financeiro");

            foreach (var falha in falhas)
            {
                csv.AppendLine($"{falha.Id}," +
                    $"{falha.DataInicio:yyyy-MM-dd HH:mm:ss}," +
                    $"{falha.DataFim:yyyy-MM-dd HH:mm:ss}," +
                    $"\"{falha.Local}\"," +
                    $"\"{falha.Descricao}\"," +
                    $"\"{falha.TipoFalha}\"," +
                    $"\"{falha.Status}\"," +
                    $"{falha.ImpactoUsuarios}," +
                    $"{falha.ImpactoFinanceiro:F2}");
            }

            File.WriteAllText(caminhoArquivo, csv.ToString());
        }
    }
} 