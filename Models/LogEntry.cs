using System;

namespace GlobalSolutionCSharp.Models
{
    // Classe que representa uma linha de log
    public class LogEntry
    {
        public DateTime Timestamp { get; set; }
        public string Tipo { get; set; }
        public string Mensagem { get; set; }

        public LogEntry(string tipo, string mensagem)
        {
            Timestamp = DateTime.Now;
            Tipo = tipo;
            Mensagem = mensagem;
        }

        public override string ToString()
        {
            return $"[{Timestamp:yyyy-MM-dd HH:mm:ss}] {Tipo}: {Mensagem}";
        }
    }
} 