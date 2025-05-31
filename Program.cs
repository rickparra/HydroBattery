using System;
using System.IO;
using GlobalSolutionCSharp.Models;
using GlobalSolutionCSharp.Services;
using GlobalSolutionCSharp.Data;
using GlobalSolutionCSharp.Utils;
using GlobalSolutionCSharp.Tests;

namespace GlobalSolutionCSharp
{
    class Program
    {
        private const ConsoleColor COR_TITULO = ConsoleColor.Cyan;
        private const ConsoleColor COR_MENU = ConsoleColor.White;
        private const ConsoleColor COR_ERRO = ConsoleColor.Red;
        private const ConsoleColor COR_SUCESSO = ConsoleColor.Green;
        private const ConsoleColor COR_ALERTA = ConsoleColor.Yellow;
        private const ConsoleColor COR_INFO = ConsoleColor.Gray;

        static void Main(string[] args)
        {
            try
            {
                Console.Title = "HydroBattery - Sistema de Gerenciamento de Falhas de Energia";
                Console.OutputEncoding = System.Text.Encoding.UTF8;

                Repository.InicializarDados();
                ExibirMenuInicial();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nERRO CRÍTICO: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
        }

        private static void ExibirMenuInicial()
        {
            while (true)
            {
                Console.WriteLine("\n=== MENU INICIAL ===");
                Console.WriteLine("1. Fazer Login");
                Console.WriteLine("2. Criar Novo Usuário");
                Console.WriteLine("0. Sair");

                var op = Console.ReadLine();
                if (op == "0") break;

                switch (op)
                {
                    case "1":
                        RealizarLogin();
                        break;
                    case "2":
                        CriarNovoUsuario();
                        break;
                    default:
                        Console.WriteLine("\nOpção inválida!");
                        break;
                }
            }
        }

        private static void RealizarLogin()
        {
            Console.Write("\nUsuário: ");
            var username = Console.ReadLine();
            Console.Write("Senha: ");
            var senha = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(senha))
            {
                Console.WriteLine("\nUsuário e senha são obrigatórios!");
                return;
            }

            var usuario = AuthService.Login(username, senha);
            if (usuario != null)
            {
                Console.WriteLine($"\nBem-vindo, {usuario.Username}!");
                ExibirMenuPrincipal();
            }
            else
            {
                Console.WriteLine("\nUsuário ou senha inválidos!");
            }
        }

        private static void CriarNovoUsuario()
        {
            Console.Write("\nNovo usuário: ");
            var username = Console.ReadLine();
            Console.Write("Senha: ");
            var senha = Console.ReadLine();
            Console.Write("Confirmar senha: ");
            var confirmarSenha = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(senha))
            {
                Console.WriteLine("\nUsuário e senha são obrigatórios!");
                return;
            }

            if (senha != confirmarSenha)
            {
                Console.WriteLine("\nAs senhas não coincidem!");
                return;
            }

            try
            {
                var usuario = AuthService.CriarUsuario(username, senha);
                Console.WriteLine($"\nUsuário {usuario.Username} criado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro ao criar usuário: {ex.Message}");
            }
        }

        private static void ExibirMenuPrincipal()
        {
            while (true)
            {
                Console.WriteLine("\n=== MENU PRINCIPAL ===");
                Console.WriteLine("1. Cadastrar Falha");
                Console.WriteLine("2. Gerar Relatórios");
                Console.WriteLine("3. Exportar para CSV");
                Console.WriteLine("4. Visualizar Logs");
                Console.WriteLine("5. Gerenciar Falhas");
                Console.WriteLine("6. Executar Testes");
                Console.WriteLine("0. Sair");

                var op = Console.ReadLine();
                if (op == "0") break;

                switch (op)
                {
                    case "1":
                        CadastrarFalhaFlow();
                        break;
                    case "2":
                        GerarRelatorioFlow();
                        break;
                    case "3":
                        ExportarCsvFlow();
                        break;
                    case "4":
                        VisualizarLogsFlow();
                        break;
                    case "5":
                        GerenciarFalhasFlow();
                        break;
                    case "6":
                        ExecutarTestesFlow();
                        break;
                    default:
                        Console.WriteLine("\nOpção inválida!");
                        break;
                }
            }
        }

        private static void CadastrarFalhaFlow()
        {
            try
            {
                var dataInicio = InputHelper.LerDataHora("Data/hora de início");
                var dataFim = InputHelper.LerDataHora("Data/hora de fim");
                var local = InputHelper.LerStringNaoVazia("Local");
                var descricao = InputHelper.LerStringNaoVazia("Descrição");
                var tipoFalha = InputHelper.LerTipoFalha("Tipo de falha");
                var impactoUsuarios = InputHelper.LerInteiroPositivo("Impacto em usuários");

                var falha = FalhaService.CadastrarFalha(
                    dataInicio,
                    dataFim,
                    local,
                    descricao,
                    tipoFalha,
                    impactoUsuarios);

                Console.WriteLine($"\nFalha registrada com sucesso! ID: {falha.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro ao cadastrar falha: {ex.Message}");
            }
        }

        private static void GerarRelatorioFlow()
        {
            Console.WriteLine("\n=== RELATÓRIOS ===");
            Console.WriteLine("1. Falhas por dia");
            Console.WriteLine("2. Duração média");
            Console.WriteLine("3. Impacto financeiro");
            Console.WriteLine("4. Falhas por tipo");
            Console.WriteLine("5. Falhas críticas");
            Console.WriteLine("6. Status das falhas");
            Console.WriteLine("7. Impacto por local");
            Console.WriteLine("0. Voltar");

            var op = Console.ReadLine();
            switch (op)
            {
                case "1":
                    RelatorioService.ExibirFalhasPorDia();
                    break;
                case "2":
                    RelatorioService.ExibirMediaDuracao();
                    break;
                case "3":
                    RelatorioService.ExibirImpactoFinanceiro();
                    break;
                case "4":
                    RelatorioService.ExibirFalhasPorTipo();
                    break;
                case "5":
                    RelatorioService.ExibirFalhasCriticas();
                    break;
                case "6":
                    RelatorioService.ExibirStatusFalhas();
                    break;
                case "7":
                    RelatorioService.ExibirImpactoPorLocal();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("\nOpção inválida!");
                    break;
            }
        }

        private static void ExportarCsvFlow()
        {
            try
            {
                var caminho = "Exportacao/falhas.csv";
                ExportService.ExportarFalhasParaCsv(caminho);
                Console.WriteLine($"\nFalhas exportadas com sucesso para: {caminho}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro ao exportar: {ex.Message}");
            }
        }

        private static void VisualizarLogsFlow()
        {
            LogService.ExibirLog();
        }

        private static void GerenciarFalhasFlow()
        {
            Console.WriteLine("\n=== GERENCIAR FALHAS ===");
            Console.WriteLine("1. Atualizar status");
            Console.WriteLine("2. Buscar por status");
            Console.WriteLine("3. Buscar por local");
            Console.WriteLine("4. Buscar por período");
            Console.WriteLine("0. Voltar");

            var op = Console.ReadLine();
            switch (op)
            {
                case "1":
                    AtualizarStatusFalhaFlow();
                    break;
                case "2":
                    BuscarFalhasPorStatusFlow();
                    break;
                case "3":
                    BuscarFalhasPorLocalFlow();
                    break;
                case "4":
                    BuscarFalhasPorPeriodoFlow();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("\nOpção inválida!");
                    break;
            }
        }

        private static void AtualizarStatusFalhaFlow()
        {
            try
            {
                var id = InputHelper.LerInteiroPositivo("ID da falha");
                var novoStatus = InputHelper.LerStringNaoVazia("Novo status");
                FalhaService.AtualizarStatusFalha(id, novoStatus);
                Console.WriteLine("\nStatus atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro ao atualizar status: {ex.Message}");
            }
        }

        private static void BuscarFalhasPorStatusFlow()
        {
            try
            {
                var status = InputHelper.LerStringNaoVazia("Status");
                var falhas = FalhaService.ObterFalhasPorStatus(status);
                ExibirFalhas(falhas);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro ao buscar falhas: {ex.Message}");
            }
        }

        private static void BuscarFalhasPorLocalFlow()
        {
            try
            {
                var local = InputHelper.LerStringNaoVazia("Local");
                var falhas = FalhaService.ObterFalhasPorLocal(local);
                ExibirFalhas(falhas);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro ao buscar falhas: {ex.Message}");
            }
        }

        private static void BuscarFalhasPorPeriodoFlow()
        {
            try
            {
                var inicio = InputHelper.LerDataHora("Data/hora inicial");
                var fim = InputHelper.LerDataHora("Data/hora final");
                var falhas = FalhaService.ObterFalhasPorPeriodo(inicio, fim);
                ExibirFalhas(falhas);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro ao buscar falhas: {ex.Message}");
            }
        }

        private static void ExibirFalhas(List<FalhaEnergia> falhas)
        {
            if (falhas.Count == 0)
            {
                Console.WriteLine("\nNenhuma falha encontrada.");
                return;
            }

            Console.WriteLine($"\nEncontradas {falhas.Count} falha(s):");
            foreach (var falha in falhas)
            {
                Console.WriteLine($"\nID: {falha.Id}");
                Console.WriteLine($"Local: {falha.Local}");
                Console.WriteLine($"Descrição: {falha.Descricao}");
                Console.WriteLine($"Status: {falha.Status}");
                Console.WriteLine($"Impacto: {falha.ImpactoUsuarios} usuários");
                Console.WriteLine($"Custo: R$ {falha.ImpactoFinanceiro:F2}");
            }
        }

        private static void ExecutarTestesFlow()
        {
            Console.WriteLine("\nEXECUTANDO TESTES DO SISTEMA");
            Console.WriteLine("============================\n");
            TestesSistema.ExecutarTodosTestes();
            Console.WriteLine("\nTestes concluídos com sucesso!");
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}
