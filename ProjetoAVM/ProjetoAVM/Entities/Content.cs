using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAVM.Entities
{
    public class Content
    {
        public string? SerachTerm { get; set; }
        public string? Prefix { get; set; }
        public string? SourceContentOriginal { get; set; }
        public string? SourceContentSanitized { get; set; }
        public Sentences Sentences { get; set; }

        public Content()
        {

        }
        public Content(string? serachTerm, string? prefix, string? sourceContentOriginal, string? sourceContentSanitized, Sentences sentences)
        {
            SerachTerm = serachTerm;
            Prefix = prefix;
            SourceContentOriginal = sourceContentOriginal;
            SourceContentSanitized = sourceContentSanitized;
            Sentences = sentences;
        }
    }

    public class Sentences
    {
        public string[]? Text { get; set; }
        public string[]? Keywords { get; set; }
        public string[]? Images { get; set; }

        public Sentences()
        {
        }
        public Sentences(string[] text, string[] keywords, string[] images)
        {
            Text = text;
            Keywords = keywords;
            Images = images;
        }
    }
}
