using System;

namespace GlobalSolutionCSharp.Services
{
    public static class ValidacaoService
    {
        public static void ValidarData(DateTime dataInicio, DateTime dataFim)
        {
            if (dataFim <= dataInicio)
            {
                throw new ArgumentException("Data de fim deve ser posterior à data de início.");
            }
        }

        public static void ValidarImpacto(int impacto)
        {
            if (impacto <= 0)
            {
                throw new ArgumentException("Impacto deve ser maior que zero.");
            }
        }
    }
} 