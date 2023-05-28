using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WikiClientLibrary.Client;
using WikiClientLibrary.Pages;

namespace ProjetoAVM.Utils
{
    public class WikipediaContentFilter
    {
        private static readonly Regex Pattern1 = new Regex(@"\{\{Infobox", RegexOptions.Compiled);
        private static readonly Regex Pattern2 = new Regex(@"\{\{Authority control", RegexOptions.Compiled);
        private static readonly Regex Pattern3 = new Regex(@"\[\[Category:", RegexOptions.Compiled);

        public static string FilterContent(string content)
        {
            // Remove padrões específicos
            content = Pattern1.Replace(content, string.Empty);
            content = Pattern2.Replace(content, string.Empty);
            content = Pattern3.Replace(content, string.Empty);

            // Remove tags específicas
            content = RemoveTags(content, "ref");
            content = RemoveTags(content, "gallery");
            // Adicione mais tags que você deseja remover, se necessário

            // Remove múltiplos espaços em branco
            content = Regex.Replace(content, @"\s+", " ");

            return content;
        }

        public static string RemoveTags(string text, string tagName)
        {
            var pattern = $@"<{tagName}.*?>.*?</{tagName}>";
            return Regex.Replace(text, pattern, string.Empty);
        }

        public static string RemoveMarkup(string text)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(text);

            // Remover marcações wiki
            var italicNodes = htmlDocument.DocumentNode.DescendantsAndSelf().Where(n => n.Name == "i" || n.Name == "em");
            foreach (var node in italicNodes.ToList())
            {
                node.ParentNode.ReplaceChild(HtmlNode.CreateNode(node.InnerHtml), node);
            }

            var boldNodes = htmlDocument.DocumentNode.DescendantsAndSelf().Where(n => n.Name == "b" || n.Name == "strong");
            foreach (var node in boldNodes.ToList())
            {
                node.ParentNode.ReplaceChild(HtmlNode.CreateNode(node.InnerHtml), node);
            }

            var linkNodes = htmlDocument.DocumentNode.DescendantsAndSelf().Where(n => n.Name == "a");
            foreach (var node in linkNodes.ToList())
            {
                node.ParentNode.ReplaceChild(HtmlNode.CreateNode(node.InnerHtml), node);
            }

            var templateNodes = htmlDocument.DocumentNode.DescendantsAndSelf().Where(n => n.Name.StartsWith("template", StringComparison.OrdinalIgnoreCase));
            foreach (var node in templateNodes.ToList())
            {
                node.ParentNode.ReplaceChild(HtmlNode.CreateNode(node.InnerHtml), node);
            }

            var refNodes = htmlDocument.DocumentNode.DescendantsAndSelf().Where(n => n.Name == "ref");
            foreach (var node in refNodes.ToList())
            {
                node.ParentNode.RemoveChild(node);
            }

            // Remover tags HTML
            var htmlTags = htmlDocument.DocumentNode.Descendants().Where(n => n.NodeType == HtmlNodeType.Element);
            foreach (var tag in htmlTags.ToList())
            {
                tag.ParentNode.RemoveChild(tag);
            }

            return htmlDocument.DocumentNode.InnerText;
        }

        public static string RemoverMarcacoesETags(string texto)
        {
            // Remover as marcações (três aspas)
            //texto = Regex.Replace(texto, @"Info/.*?\}", "");
            //texto = Regex.Replace(texto, @"dtlink.*?\}", "");
            texto = Regex.Replace(texto, @"\[\[(?:[^|\]]+\|)?([^|\]]+)\]\]", "$1");
            texto = Regex.Replace(texto, @"\*.*?\*\*", ",");
            texto = Regex.Replace(texto, @"\[.*?\]|\[\[.*?\]\]|\{\{.*?\}\}|<ref>(.*?)</ref>|\{\|.*?\|\}|\(.*?\)|:\*.*|<[^>]+/>|<ref.*?>.*?<\/ref>|\|.*?\|", "");
            texto = Regex.Replace(texto, @"[\[\]\(\)'']+", "");
            texto = Regex.Replace(texto, @"\*\*?|&nbsp;|;|µg_RE|µg|UI|_ATE", ",");
            texto = Regex.Replace(texto, @"(?<=\d),", ", ");
            texto = Regex.Replace(texto, @"  +", "");
            texto = Regex.Replace(texto, @"\s*([,:.])", "$1 ");
            texto = Regex.Replace(texto, @"\s+", " ");
            texto = Regex.Replace(texto, @"([^\w\s])\1+", "$1");
            texto = Regex.Replace(texto, @"(?<=\W)\W", "");

            return texto;
        }

        public static string RemoveBlankLinesAndMarkdown(string text)
        {
            var allLines = text.Split('\n').Where(s => !string.IsNullOrWhiteSpace(s) && !s.StartsWith("=")).ToArray();
            return string.Join("", allLines);
        }

        public static string FilterText(string wikipediaContent)
        {
            // Remove todas as marcações HTML
            string textOnly = Regex.Replace(wikipediaContent, "<.*?>", "");

            // Remova padrões específicos ou seções indesejadas usando regex
            string filteredContent = Regex.Replace(textOnly, @"\{\{.*?\}\}|\[\[.*?\]\]", "");

            return filteredContent;
        }

        public static string FilterContentWikipedia(string wikipediaContent)
        {
            wikipediaContent = RemoveBlankLinesAndMarkdown(wikipediaContent);
            wikipediaContent = FilterContent(wikipediaContent);
            wikipediaContent = RemoverMarcacoesETags(wikipediaContent);
            wikipediaContent = RemoveMarkup(wikipediaContent);
            wikipediaContent = FilterContent(wikipediaContent);

            return wikipediaContent;
        }
    }
}
