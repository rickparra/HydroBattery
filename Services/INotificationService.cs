using GlobalSolutionCSharp.Models;

namespace GlobalSolutionCSharp.Services
{
    public interface INotificationService
    {
        void EnviarNotificacao(TipoNotificacao tipo, string mensagem);
    }
} 