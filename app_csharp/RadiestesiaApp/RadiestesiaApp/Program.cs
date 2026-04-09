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
                Console.WriteLine(p);
            }

        } // FIM do Main

        // Esta função é uma "ferramenta" que o Main usa
        
        static List<string> BuscarPerguntas(List<Pergunta> lista, string termo)
        {
            return lista
                .Where(p => p.pergunta.Contains(termo, StringComparison.OrdinalIgnoreCase)) // Filtra a lista onde a pergunta contém o termo (ignorando maiúsculas/minúsculas)
                .Select(p => p.pergunta) // Transforma Pergunta em string
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