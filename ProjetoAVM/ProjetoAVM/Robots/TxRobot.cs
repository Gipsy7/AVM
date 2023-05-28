using WikiClientLibrary.Client;
using WikiClientLibrary.Pages;
using WikiClientLibrary.Sites;
using System.Text.RegularExpressions;
using WikiClientLibrary.Generators;
using System.Xml;
using HtmlAgilityPack;
using ProjetoAVM.Utils;
using WikiClientLibrary;
using WikiClientLibrary.Files;
using PragmaticSegmenterNet;
using ProjetoAVM.Entities;

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
            await ConnectToWikiPedia();
            await FetchContentFromWikipedia();
            _content.SourceContentSanitized = SanitizeContent(_content.SourceContentOriginal.ToString());
            _content.Sentences.Text = BreakContentIntoSentences(_content.SourceContentSanitized);
        }

        public async Task FetchContentFromWikipedia()
        {
            var page = new WikiPage(_site, _content.SerachTerm.ToString());
            await page.RefreshAsync(PageQueryOptions.FetchContent);
            _content.SourceContentOriginal = page.Content;
        }

        public string SanitizeContent(string content)
        {
             var result = WikipediaContentFilter.FilterContentWikipedia(content);
            return result;
        }

        public string[] BreakContentIntoSentences(string content)
        {
            IReadOnlyList<string> result = Segmenter.Segment(content,Language.English);
            var result1 = result.ToArray();
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
    }
}
