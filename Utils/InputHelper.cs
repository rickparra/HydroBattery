using System;
using System.Globalization;
using GlobalSolutionCSharp.Models;

namespace GlobalSolutionCSharp.Utils
{
    // Classe utilitária para ler entradas do console com tratamento de erros
    public static class InputHelper
    {
        // Lê uma string não vazia
        public static string LerStringNaoVazia(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt}: ");
                var input = Console.ReadLine()?.Trim();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input;
                }
                Console.WriteLine("Entrada inválida. Tente novamente.");
            }
        }

        // Lê uma data no formato "yyyy-MM-dd HH:mm"
        public static DateTime LerDataHora(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt} (dd/MM/yyyy HH:mm): ");
                var input = Console.ReadLine();
                if (DateTime.TryParseExact(input, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime data))
                {
                    return data;
                }
                Console.WriteLine("Formato inválido. Use dd/MM/yyyy HH:mm");
            }
        }

        // Lê um número inteiro positivo
        public static int LerInteiroPositivo(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt}: ");
                if (int.TryParse(Console.ReadLine(), out int valor) && valor > 0)
                {
                    return valor;
                }
                Console.WriteLine("Por favor, digite um número inteiro positivo.");
            }
        }

        // Lê um decimal positivo
        public static decimal LerDecimalPositivo(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt}: ");
                string? input = Console.ReadLine()?.Trim();
                
                if (decimal.TryParse(input, out decimal numero) && numero > 0)
                {
                    return numero;
                }
                
                Console.WriteLine("Entrada inválida. Digite um número decimal positivo.");
            }
        }

        // Lê uma opção do menu
        public static string LerOpcaoMenu(string[] opcoes)
        {
            while (true)
            {
                Console.Write("Opção: ");
                string? input = Console.ReadLine()?.Trim();
                
                if (!string.IsNullOrEmpty(input) && Array.Exists(opcoes, op => op == input))
                {
                    return input;
                }
                
                Console.WriteLine("Opção inválida. Tente novamente.");
            }
        }

        // Lê um tipo de falha
        public static string LerTipoFalha(string prompt)
        {
            Console.WriteLine("\nTipos de falha disponíveis:");
            Console.WriteLine("1. Falha Elétrica");
            Console.WriteLine("2. Falha de Rede");
            Console.WriteLine("3. Falha de Equipamento");
            Console.WriteLine("4. Falha de Infraestrutura");
            Console.WriteLine("5. Outro");

            while (true)
            {
                Console.Write($"{prompt} (1-5): ");
                var opcao = Console.ReadLine();
                return opcao switch
                {
                    "1" => "Falha Elétrica",
                    "2" => "Falha de Rede",
                    "3" => "Falha de Equipamento",
                    "4" => "Falha de Infraestrutura",
                    "5" => "Outro",
                    _ => throw new ArgumentException("Opção inválida")
                };
            }
        }

        // Converte código do tipo de falha para descrição
        public static string ObterDescricaoTipoFalha(string codigo)
        {
            return codigo switch
            {
                "1" => "Falha Elétrica",
                "2" => "Falha de Rede",
                "3" => "Falha de Equipamento",
                "4" => "Falha de Software",
                "5" => "Outros",
                _ => throw new ArgumentException("Código de tipo de falha inválido")
            };
        }
    }
} 