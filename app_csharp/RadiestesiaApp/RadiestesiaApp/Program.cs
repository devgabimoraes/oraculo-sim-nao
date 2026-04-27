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
            string caminho = @"C:\Users\DELL\Documents\Cursos\Programacao\Projeto radiestesia\dados\perguntas_brutas.json";
            // o @ garante que o \ seja lido

            // 1. Lendo o arquivo
            string json = File.ReadAllText(caminho);

            // 2. Deserializando (Transformando texto em objetos)
            List<Pergunta> perguntas = JsonSerializer.Deserialize<List<Pergunta>>(json);

            bool usuarioQuerContinuar = true;

            while (usuarioQuerContinuar)
            { 
                Console.Clear();
                ExibirBoasVindas();

                // 3. Interação com o usuário
                Console.WriteLine("Digite uma palavra para buscar:");
                string termo = Console.ReadLine();

                // 4. Chamando a função de busca
                var resultado = BuscarPerguntas(perguntas, termo);

                // 5. Exibindo os resultados da busca
                Console.WriteLine($"\n--- Resultados Encontrados ({resultado.Count}) ---");
                foreach (var p in resultado)
                {
                    Console.WriteLine($"[{p.id}] - {p.pergunta} ({p.resposta.ToUpper()})");
                }

                // 6. Parte de Detalhe por ID
                Console.WriteLine("\n--- Busca por ID Direta ---");
                Console.Write("Para ver informações detalhadas (como links e origens), digite o ID. Caso contrário, digite 0 para nova busca: ");
                if (int.TryParse(Console.ReadLine(), out int idDigitado) && idDigitado > 0) // Transformamos o texto do teclado em número
                {    Pergunta perguntaEncontrada = BuscarPorId(perguntas, idDigitado); // Chamando a nova função que foi criada!

                    if (perguntaEncontrada != null)
                    {
                        Console.WriteLine($"\n[DETALHE]: {perguntaEncontrada.pergunta}");
                        Console.WriteLine($"[RESP]: {perguntaEncontrada.resposta.ToUpper()}");
                        Console.WriteLine($"\n[DATA]: {perguntaEncontrada.video}");

                        // Lógica da "Mãe" (Contexto)
                        if (perguntaEncontrada.relacionadaId != null)
                        {
                            var perguntaMae = BuscarPorId(perguntas, perguntaEncontrada.relacionadaId.Value);
                            // Aqui chamamos a função de buscar por ID usando o ID da mãe!

                            if (perguntaMae != null)
                            {
                                Console.WriteLine("\n--- IMPORTANTE ---");
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
                }

                Console.WriteLine("\nVocê quer procurar outra palavra (S/N)?");
                string opcao = Console.ReadLine().ToUpper();

                if (opcao != "S")
                {
                    usuarioQuerContinuar = false;
                }
            }

            Console.WriteLine("Obrigada pela sua busca! Até logo.");

        } // FIM do Main


        // --- GAVETA 2: AS FERRAMENTAS DE BUSCA (LÓGICA) ---

        static string RemoverAcentos(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return texto;

            // Normaliza o texto: separa a letra do acento (Ex: 'á' vira 'a' + '´')
            string textoNormalizado = texto.Normalize(System.Text.NormalizationForm.FormD);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            foreach (char c in textoNormalizado)
            {
                // Se o caractere não for um sinal de acentuação (NonSpacingMark), nós guardamos
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            // Devolve o texto "achatado" e sem espaços extras nas pontas
            return sb.ToString().Normalize(System.Text.NormalizationForm.FormC).Trim();
        }

        static List<Pergunta> BuscarPerguntas(List<Pergunta> lista, string termo)
        {
            string termoLimpo = RemoverAcentos(termo); //limpando o termo digitado antes de fazer a busca

            return lista
                .Where(p => RemoverAcentos(p.pergunta).Contains(termoLimpo, StringComparison.OrdinalIgnoreCase))
                // Filtra a lista onde a pergunta contém o termo limpo (ignorando maiúsculas/minúsculas)
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