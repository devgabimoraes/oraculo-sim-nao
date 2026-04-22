using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq; // Essencial para buscas em listas (LINQ)

namespace ProjetoRadiestesia
{
    class Program
    {
        // --- GAVETA 1: O FLUXO PRINCIPAL (O QUE O PROGRAMA FAZ) ---
        static void Main(string[] args)
        {
            ExibirBoasVindas(); // Basta chamar o nome dela. Não precisa de "var" porque ela não entrega um valor para ser guardado.

            string caminho = @"C:\Users\DELL\Documents\Cursos\Programacao\Projeto radiestesia\dados\perguntas_brutas.json";
            // o @ garante que o \ seja lido

            // 1. Lendo o arquivo
            string json = File.ReadAllText(caminho);

            // 2. Deserializando (Transformando texto em objetos)
            List<Pergunta> perguntas = JsonSerializer.Deserialize<List<Pergunta>>(json);

            // 3. Interação com o usuário
            Console.WriteLine("Digite uma palavra para buscar:");
            string termo = Console.ReadLine();

            // 4. Chamando a função de busca
            var resultado = BuscarPerguntas(perguntas, termo);

            // 5. Exibindo os resultados da busca
            Console.WriteLine("\n--- Resultados Encontrados ---");
            foreach (var p in resultado)
            {
                Console.WriteLine($"[{p.id}] - {p.pergunta} ({p.resposta.ToUpper()})");
            }

            // 6. Parte de Detalhe por ID
            Console.WriteLine("\n--- Teste de Busca por ID Direta ---");
            Console.Write("Digite o ID de uma pergunta para ver o detalhe: ");
            int idDigitado = int.Parse(Console.ReadLine()); // Transformamos o texto do teclado em número

            Pergunta perguntaEncontrada = BuscarPorId(perguntas, idDigitado); // Chamando a nova função que foi criada!

            if (perguntaEncontrada != null)
            {
                Console.WriteLine($"\n[DETALHE]: {perguntaEncontrada.pergunta}");
                Console.WriteLine($"[RESPOSTA]: {perguntaEncontrada.resposta.ToUpper()}");

                // Lógica da "Mãe" (Contexto)
                if (perguntaEncontrada.relacionadaId != null)
                {
                    var perguntaMae = BuscarPorId(perguntas, perguntaEncontrada.relacionadaId.Value);
                    // Aqui chamamos a função de buscar por ID usando o ID da mãe!

                    if (perguntaMae != null)
                    {
                        Console.WriteLine("\n--- CONTEXTO IMPORTANTE ---");
                        Console.WriteLine($"Esta pergunta é um esclarecimento de: \"{perguntaMae.pergunta}\"");
                        Console.WriteLine("Recomendamos ler a pergunta acima para entender melhor o contexto.");
                        Console.WriteLine("---------------------------\n");
                    }
                }
            }
            else
            {
                Console.WriteLine("ID não encontrado.");
            }

        } // FIM do Main


        // --- GAVETA 2: AS FERRAMENTAS DE BUSCA (LÓGICA) ---

        static List<Pergunta> BuscarPerguntas(List<Pergunta> lista, string termo)
        {
            return lista
                .Where(p => p.pergunta.Contains(termo, StringComparison.OrdinalIgnoreCase))
                // Filtra a lista onde a pergunta contém o termo (ignorando maiúsculas/minúsculas)
                .ToList();
        }

        static Pergunta BuscarPorId(List<Pergunta> lista, int idProcurado)
        {
            return lista.FirstOrDefault(p => p.id == idProcurado);
            // O .FirstOrDefault retorna o primeiro item que encontrar com esse ID
            // Se não encontrar nada, ele retorna null
        }

        static List<Pergunta> BuscarRelacionadas(List<Pergunta> lista, int idDaPerguntaAtual)
        {
            return lista
                .Where(p => p.relacionadaId == idDaPerguntaAtual)
                .ToList();
        }


        // --- GAVETA 3: FERRAMENTAS VISUAIS ---

        static void ExibirBoasVindas() // void = vazio. Ela faz, mas não devolve.
        {
            Console.WriteLine("================================");
            Console.WriteLine("   PROJETO RADIESTESIA v1.0    ");
            Console.WriteLine("================================");
        }

    } // FIM da Class Program


    // --- GAVETA 4: O MOLDE DOS DADOS ---

    class Pergunta
    {
        public int id { get; set; }
        public string pergunta { get; set; }
        public string resposta { get; set; }
        public string video { get; set; }
        public string tema { get; set; }
        public string origem { get; set; }
        public int? relacionadaId { get; set; }
    }
}