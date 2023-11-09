using WikiClientLibrary.Client;
using WikiClientLibrary.Pages;
using WikiClientLibrary.Sites;
using ProjetoAVM.Utils;
using PragmaticSegmenterNet;
using ProjetoAVM.Entities;
using Mosaik.Core;
using Catalyst;
using Language = Mosaik.Core.Language;
using Catalyst.Models;
using Google.Protobuf;
using Newtonsoft.Json;
using HtmlAgilityPack;

namespace ProjetoAVM.Robots
{
    public class TxRobot
    {
        private WikiSite _site;
        private Content _content;
        public TxRobot(Content content)
        {
            _content = content;
            _content.Sentences = new Sentences();
        }

        public async Task StartTxRobot()
        {
            //Catch content
            await ConnectToWikiPedia();
            await FetchContentFromWikipedia();

            //Organize content
            _content.SourceContentSanitized = SanitizeContent(_content.SourceContentOriginal?.ToString());
            _content.Sentences.Text = BreakContentIntoSentences(_content.SourceContentSanitized);

            //Extract properties
            await GoogleNL.GoogleNL.asyncGoogleNL();
            await AnalyzeSentencesAsync(_content.Sentences.Text);
            //GetParametersNLP(_content.Sentences.Text);
        }

        public async Task FetchContentFromWikipedia()
        {
            var page = new WikiPage(_site, _content.SerachTerm.ToString());
            await page.RefreshAsync(PageQueryOptions.FetchContent);
            _content.SourceContentOriginal = page.Content;
        }

        public string? SanitizeContent(string? content)
        {
            if (string.IsNullOrEmpty(content)) return "";
            
            var result = WikipediaContentFilter.FilterContentWikipedia(content);
            return string.IsNullOrEmpty(result) ? "" : result;
        }

        public string[] BreakContentIntoSentences(string? content)
        {
            if (string.IsNullOrEmpty(content)) return new string[1];
            IReadOnlyList<string> result = Segmenter.Segment(content, PragmaticSegmenterNet.Language.English);
            return result.ToArray();
        }

        public async Task ConnectToWikiPedia()
        {
            var client = new WikiClient
            {
                ClientUserAgent = "WCLQuickStart/1.0 (Gipsy)",
            };

            _site = new WikiSite(client, "https://pt.wikipedia.org/w/api.php");

            await _site.Initialization;
        }

        public async Task GetParametersNLPAsync(string text)
        {
            Catalyst.Models.Portuguese.Register();

            Storage.Current = new DiskStorage("catalyst-models");

            var npl = await Pipeline.ForAsync(Language.Portuguese);
            var doc = new Document(text, Language.Portuguese);
            var result = npl.ProcessSingle(doc);

            Console.WriteLine(result.ToJson());
        }

        public async Task AnalyzeSentencesAsync(string[] text)
        {
            int maxSentences = text.Length > 10 ? 10 : text.Length;

            if (string.IsNullOrEmpty(text[0])) maxSentences = 0;

            for (int i = 0; i < maxSentences; i++)
            {
                var response = await GoogleNL.GoogleNL.GoogleAnalyzeTextAsync(text[i]);

                var teste = JsonConvert.DeserializeObject(response.Entities.ToString());

                Console.WriteLine("*================================================================================================*");
                Console.WriteLine(text[i]);
                Console.WriteLine("*================================================================================================*");
            }

        }
    }
}
