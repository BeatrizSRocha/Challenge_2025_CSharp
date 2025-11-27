using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Challenge2025
{
    internal class Program
    {
        const int InitialCapacity = 8;

        // Arrays internos
        static string[] tipos = new string[InitialCapacity];
        static double[] valores = new double[InitialCapacity];
        static DateTime[] datas = new DateTime[InitialCapacity];

        // Número registros armazenados
        static int count = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("Registro de Atividades de Saúde");

            // Loop do menu 
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Menu:");
                Console.WriteLine("1 - Adicionar registro");
                Console.WriteLine("2 - Listar registros");
                Console.WriteLine("3 - Exibir estatísticas");
                Console.WriteLine("4 - Sair");
                Console.Write("Escolha uma opção: ");
                var choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1":
                        AdicionarRegistro();
                        break;
                    case "2":
                        ListarRegistros();
                        break;
                    case "3":
                        ExibirEstatisticas();
                        break;
                    case "4":
                        Console.WriteLine("Encerrando...");
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Por favor, tente novamente.");
                        break;
                }
            }
        }

        // Lê entrada do usuário e adiciona um novo registro.
        // Valida tipo, formato da data e valor numérico (não negativo).
        static void AdicionarRegistro()
        {
            Console.Write("Tipo de atividade (ex: Exercício, Água, Sono): ");
            var tipo = Console.ReadLine()?.Trim();

            // Validação simples do tipo (não vazio)
            if (string.IsNullOrEmpty(tipo))
            {
                Console.WriteLine("Tipo inválido.");
                return;
            }

            // Leitura e validação da data 
            Console.Write("Data (dd/MM/yyyy ou yyyy-MM-dd) [Enter = hoje]: ");
            var dateInput = Console.ReadLine()?.Trim();
            DateTime dt;
            if (string.IsNullOrEmpty(dateInput))
            {
                dt = DateTime.Today;
            }
            else
            {
                string[] formats = { "dd/MM/yyyy", "d/M/yyyy", "yyyy-MM-dd", "yyyy-M-d" };
                if (!DateTime.TryParseExact(dateInput, formats, CultureInfo.CurrentCulture, DateTimeStyles.None, out dt)
                    && !DateTime.TryParse(dateInput, CultureInfo.CurrentCulture, DateTimeStyles.None, out dt))
                {
                    Console.WriteLine("Data inválida.");
                    return;
                }
            }

            // Leitura e validação do valor
            Console.Write("Valor (minutos, litros, horas etc.): ");
            var valueInput = Console.ReadLine()?.Trim();
            if (!double.TryParse(valueInput, NumberStyles.Float | NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.CurrentCulture, out double valor))
            {
                Console.WriteLine("Valor inválido.");
                return;
            }

            if (valor < 0)
            {
                Console.WriteLine("Valor não pode ser negativo.");
                return;
            }

            // Redimensiona os arrays para dobrar a capacidade
            if (count >= tipos.Length)
            {
                int newSize = tipos.Length * 2;
                Array.Resize(ref tipos, newSize);
                Array.Resize(ref valores, newSize);
                Array.Resize(ref datas, newSize);
            }

            // Armazena os valores nos arrays internos
            tipos[count] = tipo!;
            datas[count] = dt;
            valores[count] = valor;
            count++;

            Console.WriteLine("Registro adicionado com sucesso.");
        }

        // Mostra todos os registros cadastrados de forma organizada
        static void ListarRegistros()
        {
            if (count == 0)
            {
                Console.WriteLine("Nenhum registro cadastrado.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("{0,-4} {1,-20} {2,-12} {3,8}", "Id", "Atividade", "Data", "Valor");
            Console.WriteLine(new string('-', 52));

            // Itera somente até 'count'
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine("{0,-4} {1,-20} {2,-12} {3,8}",
                    i + 1,
                    tipos[i],
                    datas[i].ToString("dd/MM/yyyy"),
                    valores[i].ToString("N2", CultureInfo.CurrentCulture));
            }
        }
        // Calcula e exibe, para cada tipo de atividade, a soma total e a média dos valores
        // Agrupa por tipo usando dicionários
        static void ExibirEstatisticas()
        {
            if (count == 0)
            {
                Console.WriteLine("Nenhum registro para calcular estatísticas.");
                return;
            }

            // Dicionários para acumular soma e contagem por tipo
            var sums = new Dictionary<string, double>(StringComparer.CurrentCultureIgnoreCase);
            var counts = new Dictionary<string, int>(StringComparer.CurrentCultureIgnoreCase);

            // Preenche os dicionários com base nos arrays internos
            for (int i = 0; i < count; i++)
            {
                var t = tipos[i];
                var v = valores[i];

                if (!sums.ContainsKey(t))
                {
                    sums[t] = 0;
                    counts[t] = 0;
                }

                sums[t] += v;
                counts[t] += 1;
            }

            // Exibe o resultado organizado
            Console.WriteLine();
            Console.WriteLine("{0,-20} {1,12} {2,12}", "Atividade", "Total", "Média");
            Console.WriteLine(new string('-', 48));
            foreach (var kvp in sums.OrderBy(x => x.Key, StringComparer.CurrentCultureIgnoreCase))
            {
                var tipo = kvp.Key;
                var total = kvp.Value;
                var media = total / counts[tipo];
                Console.WriteLine("{0,-20} {1,12} {2,12}",
                    tipo,
                    total.ToString("N2", CultureInfo.CurrentCulture),
                    media.ToString("N2", CultureInfo.CurrentCulture));
            }
        }
    }
}