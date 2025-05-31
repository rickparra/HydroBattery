using System;
using System.IO;
using GlobalSolutionCSharp.Models;

namespace GlobalSolutionCSharp.Services
{
    public class FileNotificationService : INotificationService
    {
        private readonly string _caminhoArquivo;

        public FileNotificationService(string caminhoArquivo)
        {
            _caminhoArquivo = caminhoArquivo;
            var diretorio = Path.GetDirectoryName(_caminhoArquivo);
            if (!string.IsNullOrEmpty(diretorio) && !Directory.Exists(diretorio))
            {
                Directory.CreateDirectory(diretorio);
            }
        }

        public void EnviarNotificacao(TipoNotificacao tipo, string mensagem)
        {
            var notificacao = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{tipo}] {mensagem}";
            File.AppendAllText(_caminhoArquivo, notificacao + Environment.NewLine);
        }
    }
} 