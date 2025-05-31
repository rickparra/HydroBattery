using System;
using GlobalSolutionCSharp.Models;

namespace GlobalSolutionCSharp.Services
{
    public class ConsoleNotificationService : INotificationService
    {
        public void EnviarNotificacao(TipoNotificacao tipo, string mensagem)
        {
            var corOriginal = Console.ForegroundColor;
            
            Console.ForegroundColor = tipo switch
            {
                TipoNotificacao.INFO => ConsoleColor.White,
                TipoNotificacao.ALERTA => ConsoleColor.Yellow,
                TipoNotificacao.ERRO => ConsoleColor.Red,
                TipoNotificacao.CRITICO => ConsoleColor.DarkRed,
                TipoNotificacao.SUCESSO => ConsoleColor.Green,
                _ => ConsoleColor.White
            };

            Console.WriteLine($"\n[{tipo}] {mensagem}");
            Console.ForegroundColor = corOriginal;
        }
    }
} 