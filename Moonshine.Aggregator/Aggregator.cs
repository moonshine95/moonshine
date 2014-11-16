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
using IronPython;
using IronPython.Hosting;
using System.Diagnostics;
using Moonshine.Aggregator.Clustering;

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

                var _list = new List<RssFeed>();
                foreach (var feed in rssFeeds)
                {
                    RssManager.Read(feed);
                    _list.Add(feed);
                }

                Corpus corpus = new Corpus(_list);

                string[] topKeywords = { };
                foreach (var doc in corpus.Documents)
                {
                    topKeywords = topKeywords.Concat(Helpers.TopKeywords(4, doc, corpus)).ToArray();
                }
                topKeywords = topKeywords.Distinct().ToArray();

                List<double[]> vectors = Helpers.FeatureVectors(topKeywords, corpus);

                List<double[]> matrix = new List<double[]>();
                for (int i = 0; i < corpus.Documents.Count; i++)
                {
                    double[] vector = { };
                    for (int j = 0; j < corpus.Documents.Count; j++)
                    {
                        var value = Helpers.CosineSimilarity(vectors.ElementAt(i).ToList(), vectors.ElementAt(j).ToList());
                        vector = vector.Concat(new List<double> { value }).ToArray();
                    }
                    matrix.Add(vector);
                }

                var itemsToDelete = new List<RssItem>();
                var clusters = Helpers.Linkage(matrix, (double)1);
                foreach (var cluster in clusters)
                {
                    var ids = cluster.Value;
                    Console.WriteLine("======");
                    for (int h = 0; h < corpus.Documents.Count; h++)
                    {
                        if (ids.Contains(h))
                        {
                            if (ids.Count != 1)
                            {
                                itemsToDelete.Add(corpus.Documents[h].RssItem);
                                ids.RemoveAt(ids.IndexOf(h));
                            }
                        }
                    }
                }

                var lockMe = new object();
                foreach(var feed in _list)
                {
                    Parallel.ForEach(feed.RssItems, item =>
                    {
                        if (!itemsToDelete.Contains(item))
                        {
                            var _news = NewsManager.CreateNews(item, feed.Rules);
                            lock (lockMe)
                            {
                                news.Add(_news);
                            }
                        }
                    });
                }

            }

            return news;
        }
    }
}
