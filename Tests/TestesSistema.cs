using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using GlobalSolutionCSharp.Models;
using GlobalSolutionCSharp.Services;
using GlobalSolutionCSharp.Data;
using GlobalSolutionCSharp.Utils;

namespace GlobalSolutionCSharp.Tests
{
    public static class TestesSistema
    {
        private static int _totalTestes = 0;
        private static int _testesSucesso = 0;

        public static void ExecutarTodosTestes()
        {
            Console.WriteLine("Iniciando testes do sistema...\n");

            TestarAutenticacao();
            TestarCadastroFalhas();
            TestarRelatorios();
            TestarExportacao();
            TestarGerenciamentoFalhas();
            TestarLogs();
            TestarNotificacoes();
            TestarValidacoes();

            ExibirResumoTestes();
        }

        private static void TestarAutenticacao()
        {
            Console.WriteLine("=== Testes de Autenticação ===");

            // Teste 1: Login com credenciais válidas
            RegistrarTeste("Login com credenciais válidas", () =>
            {
                var usuario = AuthService.Login("admin", "1234");
                return usuario != null && usuario.Username == "admin";
            });

            // Teste 2: Login com credenciais inválidas
            RegistrarTeste("Login com credenciais inválidas", () =>
            {
                var usuario = AuthService.Login("admin", "senha_errada");
                return usuario == null;
            });

            // Teste 3: Criar novo usuário
            RegistrarTeste("Criar novo usuário", () =>
            {
                var usuario = AuthService.CriarUsuario("teste", "senha123");
                return usuario != null && usuario.Username == "teste";
            });

            // Teste 4: Tentar criar usuário duplicado
            RegistrarTeste("Tentar criar usuário duplicado", () =>
            {
                try
                {
                    AuthService.CriarUsuario("admin", "senha123");
                    return false;
                }
                catch (InvalidOperationException)
                {
                    return true;
                }
            });
        }

        private static void TestarCadastroFalhas()
        {
            Console.WriteLine("\n=== Testes de Cadastro de Falhas ===");

            // Teste 1: Cadastrar falha válida
            RegistrarTeste("Cadastrar falha válida", () =>
            {
                var falha = FalhaService.CadastrarFalha(
                    DateTime.Now,
                    DateTime.Now.AddHours(1),
                    "Data Center A",
                    "Falha de energia",
                    "Falha Elétrica",
                    100
                );
                return falha != null && falha.Id > 0;
            });

            // Teste 2: Tentar cadastrar falha com data inválida
            RegistrarTeste("Tentar cadastrar falha com data inválida", () =>
            {
                try
                {
                    FalhaService.CadastrarFalha(
                        DateTime.Now,
                        DateTime.Now.AddHours(-1),
                        "Data Center B",
                        "Falha de rede",
                        "Falha de Rede",
                        50
                    );
                    return false;
                }
                catch (ArgumentException)
                {
                    return true;
                }
            });
        }

        private static void TestarRelatorios()
        {
            Console.WriteLine("\n=== Testes de Relatórios ===");

            // Teste 1: Verificar falhas por dia
            RegistrarTeste("Verificar falhas por dia", () =>
            {
                try
                {
                    RelatorioService.ExibirFalhasPorDia();
                    return true;
                }
                catch
                {
                    return false;
                }
            });

            // Teste 2: Verificar impacto financeiro
            RegistrarTeste("Verificar impacto financeiro", () =>
            {
                try
                {
                    RelatorioService.ExibirImpactoFinanceiro();
                    return true;
                }
                catch
                {
                    return false;
                }
            });
        }

        private static void TestarExportacao()
        {
            Console.WriteLine("\n=== Testes de Exportação ===");

            // Teste 1: Exportar para CSV
            RegistrarTeste("Exportar para CSV", () =>
            {
                try
                {
                    ExportService.ExportarFalhasParaCsv("Exportacao/teste.csv");
                    return File.Exists("Exportacao/teste.csv");
                }
                catch
                {
                    return false;
                }
            });
        }

        private static void TestarGerenciamentoFalhas()
        {
            Console.WriteLine("\n=== Testes de Gerenciamento de Falhas ===");

            // Teste 1: Atualizar status de falha
            RegistrarTeste("Atualizar status de falha", () =>
            {
                try
                {
                    var falha = Repository.Falhas.First();
                    FalhaService.AtualizarStatusFalha(falha.Id, "Resolvido");
                    return Repository.Falhas.First(f => f.Id == falha.Id).Status == "Resolvido";
                }
                catch
                {
                    return false;
                }
            });

            // Teste 2: Buscar falhas por status
            RegistrarTeste("Buscar falhas por status", () =>
            {
                var falhas = FalhaService.ObterFalhasPorStatus("Resolvido");
                return falhas != null;
            });
        }

        private static void TestarLogs()
        {
            Console.WriteLine("\n=== Testes de Logs ===");

            // Teste 1: Registrar log
            RegistrarTeste("Registrar log", () =>
            {
                try
                {
                    LogService.Registrar(new LogEntry("TESTE", "Mensagem de teste"));
                    return true;
                }
                catch
                {
                    return false;
                }
            });

            // Teste 2: Exibir logs
            RegistrarTeste("Exibir logs", () =>
            {
                try
                {
                    LogService.ExibirLog();
                    return true;
                }
                catch
                {
                    return false;
                }
            });
        }

        private static void TestarNotificacoes()
        {
            Console.WriteLine("\n=== Testes de Notificações ===");

            // Teste 1: Notificação de console
            RegistrarTeste("Notificação de console", () =>
            {
                try
                {
                    var notificacao = new ConsoleNotificationService();
                    notificacao.EnviarNotificacao(TipoNotificacao.INFO, "Teste de notificação");
                    return true;
                }
                catch
                {
                    return false;
                }
            });

            // Teste 2: Notificação em arquivo
            RegistrarTeste("Notificação em arquivo", () =>
            {
                try
                {
                    var notificacao = new FileNotificationService("Logs/notificacoes.log");
                    notificacao.EnviarNotificacao(TipoNotificacao.INFO, "Teste de notificação");
                    return File.Exists("Logs/notificacoes.log");
                }
                catch
                {
                    return false;
                }
            });
        }

        private static void TestarValidacoes()
        {
            Console.WriteLine("\n=== Testes de Validações ===");

            // Teste 1: Validar data
            RegistrarTeste("Validar data", () =>
            {
                try
                {
                    ValidacaoService.ValidarData(DateTime.Now, DateTime.Now.AddHours(1));
                    return true;
                }
                catch
                {
                    return false;
                }
            });

            // Teste 2: Validar impacto
            RegistrarTeste("Validar impacto", () =>
            {
                try
                {
                    ValidacaoService.ValidarImpacto(50);
                    return true;
                }
                catch
                {
                    return false;
                }
            });
        }

        private static void RegistrarTeste(string descricao, Func<bool> teste)
        {
            _totalTestes++;
            Console.Write($"Teste {_totalTestes}: {descricao}... ");

            try
            {
                bool resultado = teste();
                if (resultado)
                {
                    _testesSucesso++;
                    Console.WriteLine("OK");
                }
                else
                {
                    Console.WriteLine("FALHA");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO: {ex.Message}");
            }
        }

        private static void ExibirResumoTestes()
        {
            Console.WriteLine($"\nResumo dos testes:");
            Console.WriteLine($"Total de testes: {_totalTestes}");
            Console.WriteLine($"Testes com sucesso: {_testesSucesso}");
            Console.WriteLine($"Testes com falha: {_totalTestes - _testesSucesso}");
        }
    }
} 