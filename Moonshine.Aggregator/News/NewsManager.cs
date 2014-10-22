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

namespace Moonshine.Aggregator.News
{
    public static class NewsManager
    {
        public static void generateNews(RssFeed rssFeed, RssItem rssItem)
        {
            var url = rssItem.Link.ToString();

            HtmlWeb client = new HtmlWeb();
            HtmlDocument htmlDoc = client.Load(url);

            string xpath = findNewsXPath(rssFeed);
            var newsNode = htmlDoc.DocumentNode.SelectSingleNode(xpath);

            Console.WriteLine(newsNode.InnerHtml);
        }

        private static string findNewsXPath(RssFeed rssFeed)
        {
            string source = rssFeed.Link.ToString();

            using (var stream = new StreamReader("..//..//..//Moonshine.Aggregator//Resources//NewsXPath.json"))
            {
                string json = stream.ReadToEnd();

                var deserializer = new JavaScriptSerializer();
                Dictionary<string, string> xpaths = deserializer.Deserialize<Dictionary<string, string>>(json);
 
                foreach (var xpath in xpaths)
                {
                    if (source.Contains(xpath.Key))
                    {
                        return xpath.Value;
                    }
                }
            }

            return null;
        }
    }
}
