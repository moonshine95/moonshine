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
            // string xpath = findNewsXPath(rssFeed);
            var newsHtml = htmlDoc.DocumentNode.SelectSingleNode(rules.ArticleXpath);
            foreach (var xpath in rules.XpathToRemove)
            {
                foreach (var node in newsHtml.SelectNodes(xpath))
                {
                    try
                    {
                        Console.WriteLine(node.InnerHtml);
                        node.Remove();
                    }
                    catch
                    {
                    }
                }
            }
            var newsContent = newsHtml.InnerHtml;

            Image image = null;
            /*
            if (rssItem.ImageUrl != null)
            {
                WebClient webClient = new WebClient();
                byte[] bytes = webClient.DownloadData(rssItem.ImageUrl);
                MemoryStream stream = new MemoryStream(bytes);
                image = Image.FromStream(stream);
            }*/

            var news = new News()
            {
                Title = rssItem.Title,
                Content = newsContent,
                Category = Category.Economy,
                Image = image
            };

            return news;
            /*
            FileStream file = File.Create("output.bin");
            var formatter = new BinaryFormatter();
            formatter.Serialize(file, news);
            file.Close();

            Console.WriteLine(newsContent);*/
        }

        private static string findNewsXPath(RssFeed rssFeed)
        {
            return "";
            /*string source = rssFeed.Link.ToString();

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

            return null;*/
        }
    }
}
