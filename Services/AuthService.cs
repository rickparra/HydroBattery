using System;
using System.Linq;
using GlobalSolutionCSharp.Models;
using GlobalSolutionCSharp.Data;

namespace GlobalSolutionCSharp.Services
{
    // Serviço para autenticação
    public static class AuthService
    {
        // Retorna o usuário se as credenciais baterem, senão null
        public static User? Login(string username, string password)
        {
            return Repository.Users.FirstOrDefault(u => 
                u.Username == username && u.Password == password);
        }

        public static User CriarUsuario(string username, string password)
        {
            if (Repository.Users.Any(u => u.Username == username))
            {
                throw new InvalidOperationException("Nome de usuário já existe.");
            }

            var novoUsuario = new User(username, password)
            {
                Id = Repository.Users.Count + 1,
                Role = "User"
            };

            Repository.Users.Add(novoUsuario);
            return novoUsuario;
        }
    }
} 