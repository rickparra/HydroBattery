using System;
using System.Collections.Generic;
using GlobalSolutionCSharp.Models;

namespace GlobalSolutionCSharp.Data
{
    // Repositório simples em memória
    public static class Repository
    {
        // Simula o banco de dados em memória
        public static List<User> Users { get; private set; } = new List<User>();
        public static List<FalhaEnergia> Falhas { get; private set; } = new List<FalhaEnergia>();

        // Você pode, se quiser, adicionar métodos para inicializar dados (ex: criar usuário admin)
        public static void InicializarDados()
        {
            // Inicializa usuários padrão
            Users.Add(new User("admin", "1234") { Id = 1, Role = "Admin" });
            Users.Add(new User("tecnico", "senha") { Id = 2, Role = "Tecnico" });
        }

        public static int GerarNovoIdFalha()
        {
            return Falhas.Count > 0 ? Falhas.Max(f => f.Id) + 1 : 1;
        }
    }
} 