using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq; // Essencial para buscas em listas (LINQ)

namespace ProjetoRadiestesia
{
    class Program
    {
        // TUDO que o programa faz começa dentro do Main
        static void Main(string[] args)
        {
            //chamando a função dentro do MAIN; 
            ExibirBoasVindas(); // Basta chamar o nome dela. Não precisa de "var" porque ela não entrega um valor para ser guardado.

            string caminho = @"C:\Users\DELL\Documents\Cursos\Programacao\Projeto radiestesia\dados\perguntas_brutas.json";

            // 1. Lendo o arquivo
            string json = File.ReadAllText(caminho);

            // 2. Deserializando (Transformando texto em objetos)
            List<Pergunta> perguntas = JsonSerializer.Deserialize<List<Pergunta>>(json);

            // 3. Interação com o usuário
            Console.WriteLine("Digite uma palavra para buscar:");
            string termo = Console.ReadLine();

            // 4. Chamando a função de busca
            // Como a função está logo abaixo, o Main consegue "enxergar" ela
            var resultado = BuscarPerguntas(perguntas, termo);

            // 5. Exibindo os resultados
            Console.WriteLine("\n--- Resultados Encontrados ---");
            foreach (var p in resultado)
            {
                // Usamos o p.id que você acabou de criar!
                Console.WriteLine($"[{p.id}] - {p.pergunta} ({p.resposta.ToUpper()})");
            }

            Console.WriteLine("\n--- Teste de Busca por ID Direta ---");
            Console.Write("Digite o ID de uma pergunta para ver o detalhe: ");
            int idDigitado = int.Parse(Console.ReadLine()); // Transformamos o texto do teclado em número

            // Chamando a nova função que você criou!
            Pergunta perguntaEncontrada = BuscarPorId(perguntas, idDigitado);


            if (perguntaEncontrada != null)
            {
                Console.WriteLine($"\n[DETALHE]: {perguntaEncontrada.pergunta}");
                Console.WriteLine($"[RESPOSTA]: {perguntaEncontrada.resposta.ToUpper()}");

                // Lógica para buscar a "Mãe" (Contexto)
                if (perguntaEncontrada.relacionadaId != null)
                {
                    // Aqui chamamos a função de buscar por ID usando o ID da mãe!
                    var perguntaMae = BuscarPorId(perguntas, perguntaEncontrada.relacionadaId.Value);

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

        // Esta função é uma "ferramenta" que o Main usa
        
        static List<Pergunta> BuscarPerguntas(List<Pergunta> lista, string termo)
        {
            return lista
                .Where(p => p.pergunta.Contains(termo, StringComparison.OrdinalIgnoreCase)) // Filtra a lista onde a pergunta contém o termo (ignorando maiúsculas/minúsculas)
                // .Select(p => p.pergunta) // Transforma Pergunta em string
                .ToList();
        }

        static Pergunta BuscarPorId(List<Pergunta> lista, int idProcurado)
        {
            // O .FirstOrDefault retorna o primeiro item que encontrar com esse ID
            // Se não encontrar nada, ele retorna null
            return lista.FirstOrDefault(p => p.id == idProcurado);
        }

        static List<Pergunta> BuscarRelacionadas(List<Pergunta> lista, int idDaPerguntaAtual)
        {
            return lista
                .Where(p => p.relacionadaId == idDaPerguntaAtual)
                .ToList();
        }


        // A DEFINIÇÃO DA FUNÇÃO
        static void ExibirBoasVindas()  // void = vazio. Ela faz, mas não devolve.
        {
            Console.WriteLine("================================");
            Console.WriteLine("   PROJETO RADIESTESIA v1.0    ");
            Console.WriteLine("================================");
        }

    } // FIM da Class Program

    class Pergunta
    {
        public int id { get; set; } // identificador único
        public string pergunta { get; set; }
        public string resposta { get; set; }
        public string video { get; set; }
        public string tema { get; set; }
        public string origem { get; set; }
        public int? relacionadaId { get; set; }  // pergunta relacionada
        
    }
}