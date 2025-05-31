using System;

namespace GlobalSolutionCSharp.Models
{
    // Classe que representa um usuário do sistema
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        // Você pode adicionar, se quiser, um campo Role ou Permissão
        
        public User(string username, string password)
        {
            Username = username;
            Password = password;
            Role = "User";
        }
    }
} 