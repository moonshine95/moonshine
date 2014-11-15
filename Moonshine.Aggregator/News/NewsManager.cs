using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Moonshine.Aggregator.Rss;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;

namespace Moonshine.Aggregator.News
{
    public static class NewsManager
    {
        public static News CreateNews(RssItem rssItem, Rules rules)
        {
            var url = rssItem.Link.ToString();

            HtmlWeb client = new HtmlWeb();
            HtmlDocument htmlDoc = client.Load(url);

            string newsContent = GetNewsContent(rules, htmlDoc.DocumentNode);

            var news = new News()
            {
                Title = rssItem.Title,
                Content = newsContent,
                Category = rssItem.Category,
            };

            return news;
        }


        private static string GetNewsContent(Rules rules, HtmlNode node)
        {
            foreach (var articleXpath in rules.ArticleXpaths)
            {
                try
                {
                    var articleNode = node.SelectSingleNode(articleXpath);

                    foreach (var xpathToRemove in rules.XpathsToRemove)
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

                    foreach (var xpathToTransform in rules.XpathsToTransform)
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

                    return articleNode.InnerHtml;
                }
                catch
                {
                }
            }
                return String.Empty;
            
        }
    }
}
