using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Moonshine.Aggregator.Rss;
using Moonshine.Aggregator.News;
using System.Web;

namespace Moonshine.Aggregator
{
    public static class Aggregator
    {
        public static List<News.News> Aggregate()
        {
            var news = new List<News.News>();

            using (var stream = new StreamReader(HttpContext.Current.Server.MapPath("~/App_Data/RssFeeds.json")))
            {
                string json = stream.ReadToEnd();

                var deserializer = new JavaScriptSerializer();
                var rssFeeds = deserializer.Deserialize<List<RssFeed>>(json);

                var lockMe = new object();
                Parallel.ForEach(rssFeeds, feed =>
                {
                    RssManager.Read(feed);
                    Parallel.ForEach(feed.RssItems, item =>
                    {
                        var _news = NewsManager.CreateNews(item, feed.Rules);
                        lock (lockMe)
                        {
                            news.Add(_news);
                        }
                    });
                });
            }

            return news;
        }
    }
}
