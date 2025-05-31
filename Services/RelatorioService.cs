using System;
using System.Linq;
using System.Collections.Generic;
using GlobalSolutionCSharp.Models;
using GlobalSolutionCSharp.Data;

namespace GlobalSolutionCSharp.Services
{
    // Serviço para gerar relatórios estatísticos
    public static class RelatorioService
    {
        // Exibe falhas por dia
        public static void ExibirFalhasPorDia()
        {
            var falhasPorDia = Repository.Falhas
                .GroupBy(f => f.DataInicio.Date)
                .OrderByDescending(g => g.Key);

            Console.WriteLine("\n=== FALHAS POR DIA ===");
            foreach (var grupo in falhasPorDia)
            {
                Console.WriteLine($"\nData: {grupo.Key:dd/MM/yyyy}");
                Console.WriteLine($"Total de falhas: {grupo.Count()}");
                Console.WriteLine($"Impacto médio: {grupo.Average(f => f.ImpactoUsuarios):F0} usuários");
                Console.WriteLine($"Custo total: R$ {grupo.Sum(f => f.ImpactoFinanceiro):F2}");
            }
        }

        // Exibe média de duração de falhas
        public static void ExibirMediaDuracao()
        {
            var mediaDuracao = Repository.Falhas.Average(f => f.DuracaoEmMinutos);
            var maiorDuracao = Repository.Falhas.Max(f => f.DuracaoEmMinutos);
            var menorDuracao = Repository.Falhas.Min(f => f.DuracaoEmMinutos);

            Console.WriteLine("\n=== DURAÇÃO DAS FALHAS ===");
            Console.WriteLine($"Média: {mediaDuracao:F0} minutos");
            Console.WriteLine($"Maior: {maiorDuracao:F0} minutos");
            Console.WriteLine($"Menor: {menorDuracao:F0} minutos");
        }

        // Exibe impacto financeiro total
        public static void ExibirImpactoFinanceiro()
        {
            var impactoTotal = Repository.Falhas.Sum(f => f.ImpactoFinanceiro);
            var impactoMedio = Repository.Falhas.Average(f => f.ImpactoFinanceiro);
            var maiorImpacto = Repository.Falhas.Max(f => f.ImpactoFinanceiro);

            Console.WriteLine("\n=== IMPACTO FINANCEIRO ===");
            Console.WriteLine($"Total: R$ {impactoTotal:F2}");
            Console.WriteLine($"Médio por falha: R$ {impactoMedio:F2}");
            Console.WriteLine($"Maior impacto: R$ {maiorImpacto:F2}");
        }

        // Exibe falhas por tipo
        public static void ExibirFalhasPorTipo()
        {
            var falhasPorTipo = Repository.Falhas
                .GroupBy(f => f.TipoFalha)
                .OrderByDescending(g => g.Count());

            Console.WriteLine("\n=== FALHAS POR TIPO ===");
            foreach (var grupo in falhasPorTipo)
            {
                Console.WriteLine($"\nTipo: {grupo.Key}");
                Console.WriteLine($"Quantidade: {grupo.Count()}");
                Console.WriteLine($"Impacto médio: {grupo.Average(f => f.ImpactoUsuarios):F0} usuários");
                Console.WriteLine($"Custo total: R$ {grupo.Sum(f => f.ImpactoFinanceiro):F2}");
            }
        }

        // Exibe falhas críticas
        public static void ExibirFalhasCriticas()
        {
            var falhasCriticas = Repository.Falhas
                .Where(f => f.AlertaCritico)
                .OrderByDescending(f => f.ImpactoFinanceiro);

            Console.WriteLine("\n=== FALHAS CRÍTICAS ===");
            foreach (var falha in falhasCriticas)
            {
                Console.WriteLine($"\nID: {falha.Id}");
                Console.WriteLine($"Local: {falha.Local}");
                Console.WriteLine($"Data: {falha.DataInicio:dd/MM/yyyy HH:mm}");
                Console.WriteLine($"Impacto: {falha.ImpactoUsuarios} usuários");
                Console.WriteLine($"Custo: R$ {falha.ImpactoFinanceiro:F2}");
            }
        }

        // Exibe status das falhas
        public static void ExibirStatusFalhas()
        {
            var falhasPorStatus = Repository.Falhas
                .GroupBy(f => f.Status)
                .OrderByDescending(g => g.Count());

            Console.WriteLine("\n=== STATUS DAS FALHAS ===");
            foreach (var grupo in falhasPorStatus)
            {
                Console.WriteLine($"\nStatus: {grupo.Key}");
                Console.WriteLine($"Quantidade: {grupo.Count()}");
                Console.WriteLine($"Impacto médio: {grupo.Average(f => f.ImpactoUsuarios):F0} usuários");
                Console.WriteLine($"Custo total: R$ {grupo.Sum(f => f.ImpactoFinanceiro):F2}");
            }
        }

        // Exibe impacto por local
        public static void ExibirImpactoPorLocal()
        {
            var impactoPorLocal = Repository.Falhas
                .GroupBy(f => f.Local)
                .OrderByDescending(g => g.Sum(f => f.ImpactoFinanceiro));

            Console.WriteLine("\n=== IMPACTO POR LOCAL ===");
            foreach (var grupo in impactoPorLocal)
            {
                Console.WriteLine($"\nLocal: {grupo.Key}");
                Console.WriteLine($"Total de falhas: {grupo.Count()}");
                Console.WriteLine($"Impacto médio: {grupo.Average(f => f.ImpactoUsuarios):F0} usuários");
                Console.WriteLine($"Custo total: R$ {grupo.Sum(f => f.ImpactoFinanceiro):F2}");
            }
        }

        // Você pode criar outros métodos de relatório (por local, por alerta crítico, etc.)
    }
} 