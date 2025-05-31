using System;
using System.Collections.Generic;
using System.Linq;
using GlobalSolutionCSharp.Models;
using GlobalSolutionCSharp.Data;
using GlobalSolutionCSharp.Utils;

namespace GlobalSolutionCSharp.Services
{
    // Serviço para gerenciar falhas de energia
    public static class FalhaService
    {
        private static readonly INotificationService _notificationService = new ConsoleNotificationService();

        // Cadastra uma nova falha
        public static FalhaEnergia CadastrarFalha(
            DateTime dataInicio,
            DateTime dataFim,
            string local,
            string descricao,
            string tipoFalha,
            int impactoUsuarios)
        {
            if (dataFim <= dataInicio)
            {
                throw new ArgumentException("Data de fim deve ser posterior à data de início.");
            }

            if (string.IsNullOrWhiteSpace(local))
                throw new ArgumentException("Local não pode ser vazio");

            if (string.IsNullOrWhiteSpace(descricao))
                throw new ArgumentException("Descrição não pode ser vazia");

            if (string.IsNullOrWhiteSpace(tipoFalha))
                throw new ArgumentException("Tipo de falha não pode ser vazio");

            if (impactoUsuarios <= 0)
                throw new ArgumentException("Impacto em usuários deve ser maior que zero");

            var falha = new FalhaEnergia
            {
                Id = Repository.GerarNovoIdFalha(),
                DataInicio = dataInicio,
                DataFim = dataFim,
                Local = local,
                Descricao = descricao,
                TipoFalha = tipoFalha,
                ImpactoUsuarios = impactoUsuarios,
                Status = "Em Análise",
                ImpactoFinanceiro = CalcularImpactoFinanceiro(dataInicio, dataFim, impactoUsuarios)
            };

            Repository.Falhas.Add(falha);

            if (falha.AlertaCritico)
            {
                _notificationService.EnviarNotificacao(
                    TipoNotificacao.CRITICO,
                    $"ALERTA CRÍTICO: Falha registrada no local {local} com impacto em {impactoUsuarios} usuários!"
                );
            }
            else
            {
                _notificationService.EnviarNotificacao(
                    TipoNotificacao.INFO,
                    $"Nova falha registrada: {descricao} em {local}"
                );
            }

            return falha;
        }

        // Retorna todas as falhas cadastradas
        public static List<FalhaEnergia> ObterTodasFalhas()
        {
            return Repository.Falhas;
        }

        // Retorna falhas por status
        public static List<FalhaEnergia> ObterFalhasPorStatus(string status)
        {
            return Repository.Falhas
                .Where(f => f.Status == status)
                .OrderByDescending(f => f.DataInicio)
                .ToList();
        }

        // Retorna falhas críticas
        public static List<FalhaEnergia> ObterFalhasCriticas()
        {
            return Repository.Falhas
                .Where(f => f.AlertaCritico)
                .OrderByDescending(f => f.ImpactoFinanceiro)
                .ToList();
        }

        // Atualiza o status de uma falha
        public static void AtualizarStatusFalha(int id, string novoStatus)
        {
            var falha = Repository.Falhas.Find(f => f.Id == id);
            if (falha == null)
            {
                throw new ArgumentException($"Falha com ID {id} não encontrada.");
            }

            falha.Status = novoStatus;
            _notificationService.EnviarNotificacao(
                TipoNotificacao.INFO,
                $"Status da falha {id} atualizado para: {novoStatus}"
            );
        }

        // Retorna falhas por período
        public static List<FalhaEnergia> ObterFalhasPorPeriodo(DateTime inicio, DateTime fim)
        {
            return Repository.Falhas
                .Where(f => f.DataInicio >= inicio && f.DataFim <= fim)
                .OrderByDescending(f => f.DataInicio)
                .ToList();
        }

        // Retorna falhas por local
        public static List<FalhaEnergia> ObterFalhasPorLocal(string local)
        {
            return Repository.Falhas
                .Where(f => f.Local.Contains(local, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(f => f.DataInicio)
                .ToList();
        }

        // Calcula o impacto financeiro total
        public static decimal CalcularImpactoFinanceiroTotal()
        {
            return Repository.Falhas.Sum(f => f.ImpactoFinanceiro);
        }

        // Calcula o impacto total de usuários
        public static int CalcularImpactoUsuariosTotal()
        {
            return Repository.Falhas.Sum(f => f.ImpactoUsuarios);
        }

        private static decimal CalcularImpactoFinanceiro(DateTime inicio, DateTime fim, int impactoUsuarios)
        {
            // Custo base por minuto de falha
            const decimal CUSTO_BASE_POR_MINUTO = 100.0m;
            
            // Custo adicional por usuário afetado
            const decimal CUSTO_POR_USUARIO = 0.5m;
            
            // Calcula a duração em minutos
            var duracao = (fim - inicio).TotalMinutes;
            
            // Calcula o impacto financeiro base
            var impactoBase = CUSTO_BASE_POR_MINUTO * (decimal)duracao;
            
            // Adiciona o custo por usuário afetado
            var custoUsuarios = CUSTO_POR_USUARIO * impactoUsuarios;
            
            return impactoBase + custoUsuarios;
        }
    }
} 