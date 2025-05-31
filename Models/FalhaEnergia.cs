using System;

namespace GlobalSolutionCSharp.Models
{
    // Classe para representar um evento de falta de energia
    public class FalhaEnergia
    {
        public int Id { get; set; }
        public required string Local { get; set; }
        public required string Descricao { get; set; }
        public required string TipoFalha { get; set; }
        public required string Status { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int ImpactoUsuarios { get; set; }
        public decimal ImpactoFinanceiro { get; set; }
        public bool AlertaCritico { get; set; }

        // Propriedade calculada: duração em minutos
        public double DuracaoEmMinutos => (DataFim - DataInicio).TotalMinutes;

        public FalhaEnergia()
        {
            Status = "Em Análise";
            DataInicio = DateTime.Now;
            DataFim = DateTime.Now;
            ImpactoFinanceiro = 0;
            AlertaCritico = false;
        }

        public static FalhaEnergia Criar(
            DateTime dataInicio,
            DateTime dataFim,
            string local,
            string descricao,
            string tipoFalha,
            int impactoUsuarios)
        {
            if (dataFim <= dataInicio)
                throw new ArgumentException("Data de fim deve ser posterior à data de início");

            if (string.IsNullOrWhiteSpace(local))
                throw new ArgumentException("Local não pode ser vazio");

            if (string.IsNullOrWhiteSpace(descricao))
                throw new ArgumentException("Descrição não pode ser vazia");

            if (impactoUsuarios < 0)
                throw new ArgumentException("Impacto de usuários não pode ser negativo");

            var falha = new FalhaEnergia
            {
                DataInicio = dataInicio,
                DataFim = dataFim,
                Local = local,
                Descricao = descricao,
                TipoFalha = tipoFalha,
                ImpactoUsuarios = impactoUsuarios,
                Status = "Em Análise"
            };

            // Verifica regras de negócio para alerta crítico
            falha.AlertaCritico = falha.DuracaoEmMinutos > 120 ||
                                 local.ToLower().Contains("data center") ||
                                 local.ToLower().Contains("hospital") ||
                                 impactoUsuarios > 1000;

            return falha;
        }

        public void AtualizarStatus(string novoStatus)
        {
            if (string.IsNullOrWhiteSpace(novoStatus))
                throw new ArgumentException("Status não pode ser vazio");

            Status = novoStatus;
        }
    }
} 