using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace ProjetoRadiestesia
{
	class BuscadorService
	{
		// Lista que vai ser usada pelo buscador
		private List<Pergunta> _lista;

		// O "Construtor" serve para a gente entregar as perguntas para o buscador
		public BuscadorService(List<Pergunta> listaOriginal)
		{
            // A nossa lista privada recebe uma NOVA lista, 
            // copiada elemento por elemento da lista original.
            _lista = new List<Pergunta>(listaOriginal);
        }

		// GAVETA 2
		public string RemoverAcentos(string texto)
		{
			if (string.IsNullOrWhiteSpace(texto)) return texto;

            // Normaliza o texto: separa a letra do acento (Ex: 'á' vira 'a' + '´')
            string textoNormalizado = texto.Normalize(NormalizationForm.FormD);
			StringBuilder sb = new StringBuilder();
			
			foreach (char c in textoNormalizado)
            {   // Se o caractere não for um sinal de acentuação (NonSpacingMark), nós guardamos
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
				{
					sb.Append(c);
				}
			}
            // Devolve o texto "achatado" e sem espaços extras nas pontas
            return sb.ToString().Normalize(NormalizationForm.FormC).Trim();
		}

		public List<Pergunta> BuscarPerguntas(string termo)
		{
			string termoLimpo = RemoverAcentos(termo); //limpando o termo digitado antes de fazer a busca

            return _lista
                // Filtra a lista onde a pergunta contém o termo limpo (ignorando maiúsculas/minúsculas)
                .Where(p => RemoverAcentos(p.pergunta).Contains(termoLimpo, StringComparison.OrdinalIgnoreCase))
				.ToList();
		}

		public Pergunta BuscarPorId(int idProcurado)
		{
            // O .FirstOrDefault retorna o primeiro item que encontrar com esse ID
            // Se não encontrar nada, ele retorna null
            return _lista.FirstOrDefault(p => p.id == idProcurado);
		}

		public List<Pergunta> BuscarRelacionadas(int idDaPerguntaAtual)
		{
			return _lista
				.Where(p => p.relacionadaId == idDaPerguntaAtual).ToList();
		}

	}
}