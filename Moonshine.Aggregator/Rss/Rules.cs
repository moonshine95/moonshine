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
        public string ArticleXpath { get; set; }

        public Rules(string articleXpath, List<string> xpathToRemove, Dictionary<string, string> xpathsToTransform)
        {
            ArticleXpath = articleXpath;
            XpathsToRemove = xpathToRemove;
            XpathsToTransform = xpathsToTransform;
        }

        public Rules()
        {

        }
    }
}
