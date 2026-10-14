# Projeto Radiestesia: buscador de perguntas e respostas

Aplicação de consola em C# (.NET) que pesquisa e filtra um conjunto de dados próprio com mais de 2.000 registos em JSON. O dataset reúne perguntas com respostas "sim", "não" ou "indiferente", e foi escolhido como domínio para praticar modelação, organização e pesquisa de dados.

## O que faz

- Lê e desserializa os dados em JSON para objetos C#
- Pesquisa por **palavra-chave** e por **ID**
- Ignora acentos e maiúsculas/minúsculas na pesquisa
- Mostra o **contexto** de uma pergunta que depende de outra (relação pergunta → pergunta anterior)
- Valida as entradas numéricas do utilizador
- Usa tipos *nullable* para lidar com campos em falta

## Os dados

Dataset organizado por mim: transcrevi as perguntas de vídeos públicos do canal @juliajstudioo (Instagram) e estruturei os registos em JSON, com campos como pergunta, resposta, data do vídeo, origem e ligação a uma pergunta relacionada.

## Tecnologias

- C# / .NET (programação orientada a objetos, LINQ)
- JSON
- Git / GitHub
- HTML5 (página de apresentação do projeto)

## Estrutura do repositório

- `csharp/RadiestesiaApp`: aplicação de consola
- `dados/`: dataset em JSON (`perguntas_brutas.json`)
- `web/`: página HTML5 de apresentação do projeto

## Como executar

1. Abrir o ficheiro `.slnx` dentro de `csharp/RadiestesiaApp` no Visual Studio.
2. Executar o projeto (botão de iniciar).

**Limitação conhecida:** o caminho para o dataset é relativo à pasta onde o programa corre (`bin/Debug/net10.0`) e foi testado apenas no Windows, com o Visual Studio.

## Próximos passos

- Modelar a tabela em MySQL e importar os dados (em curso)
- Explorar o dataset com Python (pandas)
- Avaliar ligar a página web à base de dados

## Aprendizados

Projeto criado para praticar lógica de programação, POO e organização de dados, como parte da minha transição da biologia para a área de dados e desenvolvimento de software.
