using Google.Api.Gax.Grpc;
using Google.Api.Gax.Grpc.Rest;
using Google.Cloud.Language.V1;
using Grpc.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAVM.GoogleNL
{
    public static class GoogleNL
    {
        private static GoogleUserEntity _googleUserEntity;
        private static LanguageServiceClient _client;
        public static async Task asyncGoogleNL()
        {
            await ConnectClientAsync();
        }

        public static async Task ConnectClientAsync()
        {
            string path = "K:\\Projetos\\AVM\\ProjetoAVM\\ProjetoAVM\\GoogleNL\\credential.json";
            var content = File.ReadAllText(path);

            _client = await new LanguageServiceClientBuilder
            {
                JsonCredentials = content
            }.BuildAsync();
        }

        public static async Task<AnnotateTextResponse>  GoogleAnalyzeTextAsync(string? text)
        {
            Document document= Document.FromPlainText(text);

            var request = new AnnotateTextRequest.Types.Features
            {
                ClassifyText = default,
                ClassificationModelOptions = default,
                ExtractDocumentSentiment= true,
                ExtractEntities = true,
                ModerateText= true,
                ExtractSyntax= true
            };

            return await _client.AnnotateTextAsync(document,request);
        }
    }
}
