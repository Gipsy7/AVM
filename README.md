# AVM — Automatizador de Vídeos

Projeto open source para gerar vídeos automaticamente a partir de um tema. Nesta fase, o programa coleta e prepara o **roteiro**: busca o conteúdo na Wikipédia, limpa o texto, divide em frases e extrai entidades com processamento de linguagem natural.

> Projeto pessoal (2023), em andamento. A etapa de texto está implementada; as etapas de imagens, montagem e publicação do vídeo ainda não.

## Como funciona

A aplicação é um orquestrador de "robôs", cada um responsável por uma etapa:

1. **Entrada do usuário**: pede um termo de busca e um prefixo (`Who is`, `What is` ou `The history of`)
2. **Robô de texto** (`TxRobot`):
   - busca o artigo na Wikipédia em português
   - remove marcações, referências e trechos que não interessam
   - quebra o texto em frases
   - analisa as primeiras frases com a **Google Cloud Natural Language API** (entidades, sentimento e sintaxe)

```
Program → Orchestrator → UserInput → TxRobot → (próximos robôs: imagens, vídeo, upload)
```

## Tecnologias

- .NET 6 (aplicação de console)
- WikiClientLibrary (API da Wikipédia)
- PragmaticSegmenterNet (divisão em frases)
- Google Cloud Natural Language API
- Catalyst (NLP local, em testes)
- HtmlAgilityPack e AngleSharp

## Como executar

Pré-requisitos: SDK do .NET 6 e uma conta de serviço do Google Cloud com a Natural Language API ativada.

1. Baixe o JSON de credencial da conta de serviço.
2. Aponte o caminho do arquivo em `ProjetoAVM/ProjetoAVM/GoogleNL/GoogleNL.cs` (método `ConnectClientAsync`). Não faça commit desse arquivo.
3. Rode:
   ```bash
   dotnet run --project ProjetoAVM/ProjetoAVM
   ```

## Licença

[MIT](LICENSE).

---

Feito por **Mikael Francisco** · [Portfólio](https://mikaelfrancisco.vercel.app) · [LinkedIn](https://www.linkedin.com/in/mikael-francisco-a4300b180)
