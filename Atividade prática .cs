using System;
using System.Linq;
using System.Collections.Generic;

public class Programa
{
    public static void Main(string[] args)
    {
        var opcao = 0;
        var opcao_valida = true;

        List<string> nome_exer = new List<string>();
        List<string> grupo_exer = new List<string>();
        List<double> carga_exer = new List<double>();
        List<int> reps_exer = new List<int>();

        do
        {
            Console.WriteLine("---------------MENU---------------");
            Console.WriteLine("1 - Adicionar exercicio");
            Console.WriteLine("2 - Listar exercicios");
            Console.WriteLine("3 - Buscar exercicio por nome");
            Console.WriteLine("4 - Filtrar por grupo muscular");
            Console.WriteLine("5 - Calcular carga total do treino");
            Console.WriteLine("6 - Exibir exercicio mais pesado");
            Console.WriteLine("7 - Remover exercicio");
            Console.WriteLine("0 - Sair");

            Console.Write("Sua escolha: ");

            opcao_valida = int.TryParse(Console.ReadLine(), out opcao);

            if (opcao_valida == false)
            {
                opcao = -1;
            }

            switch (opcao)
            {
                case 1:
                    Adicionar_exer(nome_exer, grupo_exer, carga_exer, reps_exer);
                    break;

                case 2:
                    Listar_exer(nome_exer, grupo_exer, carga_exer, reps_exer);
                    break;

                case 3:
                    Buscar_exer(nome_exer, grupo_exer, carga_exer, reps_exer);
                    break;

                case 4:
                    Filtrar_exer(nome_exer, grupo_exer, carga_exer, reps_exer);
                    break;

                case 5:
                    Calcula_exer(carga_exer);
                    break;

                case 6:
                    Maior_exer(carga_exer, nome_exer);
                    break;

                case 7:
                    Excluir_exer(nome_exer, grupo_exer, carga_exer, reps_exer);
                    break;

                default:
                    Console.WriteLine("Opcao de escolha invalida, tente novamente");
                    break;
            }

        } while (opcao != 0);
    }

    public static void Adicionar_exer(List<string> nome_exer, List<string> grupo_exer, List<double> carga_exer, List<int> reps_exer)
    {
        bool sucesso;
        string nome;
        double carga;
        int reps;
        string grupo;

        do
        {
            Console.Write("Escreva o nome do exericios: ");
            nome = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nome))
            {
                Console.Write("O nome do exercicio nao pode ser nulo");
            }
        } while (string.IsNullOrWhiteSpace(nome));

        Console.Write("Escreva o grupo muscular do exericios: ");
        grupo = Console.ReadLine();

        do
        {
            Console.Write("Escreva a carga (em kg) do exericios: ");
            sucesso = double.TryParse(Console.ReadLine(), out carga);

            if (carga <= 0)
            {
                Console.WriteLine("A carga precisa ser maior que 0");
            }
        } while (carga <= 0 || sucesso == false);

        do
        {
            Console.Write("Digite a quantidade de repiticoes: ");
            sucesso = int.TryParse(Console.ReadLine(), out reps);
            if (reps <= 0)
            {
                Console.WriteLine("A qtd de reps precisa ser pelo menos 1");
            }
        } while (reps < 1 || sucesso == false);

        nome_exer.Add(nome);
        grupo_exer.Add(grupo);
        carga_exer.Add(carga);
        reps_exer.Add(reps);
    }

    public static void Listar_exer(List<string> nome_exer, List<string> grupo_exer, List<double> carga_exer, List<int> reps_exer)
    {
        if (nome_exer.Count == 0)
        {
            Console.WriteLine("Nenhum exercício cadastrado");
            return;
        }

        for (int i = 0; i < nome_exer.Count; i++)
        {
            Console.WriteLine("Exercicio nº" + (i + 1) + " : " + nome_exer[i] + " - " + grupo_exer[i] + " - " + carga_exer[i] + "kg - " + reps_exer[i]);
        }
    }

    public static void Buscar_exer(List<string> nome_exer, List<string> grupo_exer, List<double> carga_exer, List<int> reps_exer)
    {
        if (nome_exer.Count == 0)
        {
            Console.WriteLine("Nenhum exercício cadastrado");
            return;
        }

        Console.Write("Qual o nome do exercicio a ser buscado: ");
        string busca = Console.ReadLine();
        int i = nome_exer.FindIndex(n => n == busca);

        if (i == -1)
        {
            Console.WriteLine("Exercício não encontrado.");
        }
        else
        {
            Console.WriteLine(nome_exer[i] + " - " + grupo_exer[i] + " - " + carga_exer[i] + "kg - " + reps_exer[i]);
        }
    }

    public static void Filtrar_exer(List<string> nome_exer, List<string> grupo_exer, List<double> carga_exer, List<int> reps_exer)
    {
        if (nome_exer.Count == 0)
        {
            Console.WriteLine("Nenhum exercício cadastrado");
            return;
        }

        bool encontrado = false;

        Console.Write("Qual o grupo muscular a ser buscado: ");
        string busca = Console.ReadLine();

        for (int i = 0; i < carga_exer.Count; i++)
        {
            if (grupo_exer[i] == busca)
            {
                Console.WriteLine("Exercicio nº" + (i + 1) + " : " + nome_exer[i] + " - " + grupo_exer[i] + " - " + carga_exer[i] + "kg - " + reps_exer[i]);
                encontrado = true;
            }
        }

        if (encontrado == false)
        {
            Console.WriteLine("Nenhum exercio para este grupo");
        }
    }

    public static void Calcula_exer(List<double> carga_exer)
    {
        if (carga_exer.Count == 0)
        {
            Console.WriteLine("Nenhum exercício cadastrado");
            return;
        }

        double soma = carga_exer.Sum();

        Console.WriteLine("Total de carga no treino: " + soma);
    }

    public static void Maior_exer(List<double> carga_exer, List<string> nome_exer)
    {
        if (carga_exer.Count == 0)
        {
            Console.WriteLine("Nenhum exercício cadastrado");
            return;
        }

        double maior = carga_exer.Max();

        for (int i = 0; i < carga_exer.Count; i++)
        {
            if (carga_exer[i] == maior)
            {
                Console.WriteLine("O exercicio com maior carga e: " + nome_exer[i] + " com " + carga_exer[i] + "kg");
            }
        }
    }

    public static void Excluir_exer(List<string> nome_exer, List<string> grupo_exer, List<double> carga_exer, List<int> reps_exer)
    {
        if (nome_exer.Count == 0)
        {
            Console.WriteLine("Nenhum exercício cadastrado");
            return;
        }

        string nome;
        double carga;
        int reps;
        string grupo;

        Console.Write("Qual o nome do exercicio a ser excluido: ");
        string busca = Console.ReadLine();
        int i = nome_exer.FindIndex(n => n == busca);

        if (i == -1)
        {
            Console.WriteLine("Exercício não encontrado.");
        }
        else
        {
            nome = nome_exer[i];
            grupo = grupo_exer[i];
            carga = carga_exer[i];
            reps = reps_exer[i];

            nome_exer.RemoveAt(i);
            grupo_exer.RemoveAt(i);
            carga_exer.RemoveAt(i);
            reps_exer.RemoveAt(i);

            Console.WriteLine("O exercicios foi excluido. Os dados excluidos foram: " + "Exercicio nº" + (i + 1) + " : " + nome + " - " + grupo + " - " + carga + "kg - " + reps);
        }
    }
}
