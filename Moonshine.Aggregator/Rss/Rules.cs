using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moonshine.Aggregator.Rss
{
    public class Rules
    {
        public List<string> XpathsToRemove { get; set; }
        public Dictionary<string, string> XpathsToTransform { get; set; }
        public List<string> ArticleXpaths { get; set; }

        public Rules(List<string> articleXpaths, List<string> xpathToRemove, Dictionary<string, string> xpathsToTransform)
        {
            ArticleXpaths = articleXpaths;
            XpathsToRemove = xpathToRemove;
            XpathsToTransform = xpathsToTransform;
        }

        public Rules()
        {

        }
    }
}
