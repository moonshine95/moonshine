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

        public HtmlNode ApplyTo(HtmlNode node)
        {
            // Select article node
            var articleNode = node.SelectSingleNode(ArticleXpath);

            // Delete node
            foreach (var xpathToRemove in XpathsToRemove)
            {
                try
                {
                    foreach (var nodeToRemove in articleNode.SelectNodes(xpathToRemove))
                    {
                        try
                        {
                            Console.WriteLine(nodeToRemove.InnerHtml);
                            nodeToRemove.Remove();
                        }
                        catch
                        {
                        }
                    }
                }
                catch
                {
                }
            }

            // Transform node
            foreach (var xpathToTransform in XpathsToTransform)
            {
                try
                {
                    foreach (var nodeToTransform in articleNode.SelectNodes(xpathToTransform.Key))
                    {
                        try
                        {
                            if (xpathToTransform.Value == "None")
                            {
                                nodeToTransform.ParentNode.RemoveChild(nodeToTransform, true);
                            }
                            else
                            {
                                var newNode = HtmlNode.CreateNode(String.Format("<{0}>{1}</{0}>", xpathToTransform.Value, nodeToTransform.InnerHtml));
                                nodeToTransform.ParentNode.ReplaceChild(newNode, nodeToTransform);
                            }
                        }
                        catch
                        {
                        }
                    }
                }
                catch
                {
                }
            }

            return articleNode;
        }
    }
}
