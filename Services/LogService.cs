using System;
using System.IO;
using System.Collections.Generic;
using GlobalSolutionCSharp.Models;

namespace GlobalSolutionCSharp.Services
{
    // Serviço para escrever e ler logs em arquivo
    public static class LogService
    {
        private static readonly string _caminhoLog = "Logs/sistema.log";
        private static readonly List<LogEntry> _logs = new List<LogEntry>();

        static LogService()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_caminhoLog) ?? "Logs");
        }

        // Adiciona uma entrada de log ao arquivo
        public static void Registrar(LogEntry log)
        {
            if (log == null)
            {
                throw new ArgumentNullException(nameof(log));
            }

            _logs.Add(log);

            try
            {
                var diretorio = Path.GetDirectoryName(_caminhoLog) ?? "Logs";
                if (!Directory.Exists(diretorio))
                {
                    Directory.CreateDirectory(diretorio);
                }

                File.AppendAllText(_caminhoLog, log.ToString() + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao registrar log: {ex.Message}");
            }
        }

        // Exibe as últimas N linhas do log (ou tudo se quiser)
        public static void ExibirLog()
        {
            Console.WriteLine("\n=== LOGS DO SISTEMA ===");
            foreach (var log in _logs)
            {
                Console.WriteLine(log);
            }
        }

        public static void LimparLog()
        {
            _logs.Clear();
            File.WriteAllText(_caminhoLog, string.Empty);
        }
    }
} 