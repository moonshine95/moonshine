using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using Moonshine.Aggregator.Rss;
using Moonshine.Aggregator.News;
using Moonshine.Aggregator.Clustering;
using System.Linq;

namespace Moonshine.Aggregator.Tests
{
    [TestClass]
    public class RssManagerTests
    {
        [TestMethod]
        public void Verify_Read_Rss_Not_Null()
        {
            using (var stream = new StreamReader("..//..//..//Moonshine.Aggregator//Resources//RssFeeds.json"))
            {
                RssFeed myFeed = null;
                string json = stream.ReadToEnd();

                var deserializer = new JavaScriptSerializer();
                var rssFeeds = deserializer.Deserialize<List<RssFeed>>(json);

                foreach (var feed in rssFeeds)
                {
                    RssManager.Read(feed);
                    myFeed = feed;
                    break;
                }


                Corpus corpus = new Corpus(new List<RssFeed> { myFeed });

                string[] topKeywords = {};
                foreach (var doc in corpus.Documents)
                {
                    topKeywords = topKeywords.Concat(Helpers.TopKeywords(4, doc, corpus)).ToArray();
                }
                topKeywords = topKeywords.Distinct().ToArray();

                foreach (var vec in Helpers.FeatureVectors(topKeywords, corpus))
                {
                    //Console.WriteLine("===VECTOR===");
                    foreach (var elt in vec)
                    {
                       // Console.WriteLine(elt);
                    }
                }

                List<List<double>> mat = new List<List<double>>();
                var vectors = Helpers.FeatureVectors(topKeywords, corpus);
                for (int i = 0; i < corpus.Documents.Count; i++)
                {
                    List<double> _mat = new List<double>();
                    for (int j = 0; j < corpus.Documents.Count; j++)
                    {
                        //Console.WriteLine(Helpers.CosineSimilarity(vectors.ElementAt(i).ToList(), vectors.ElementAt(j).ToList()));
                        _mat.Add(Helpers.CosineSimilarity(vectors.ElementAt(i).ToList(), vectors.ElementAt(j).ToList()));
                    }
                    mat.Add(_mat);
                }

                var dict = Helpers.Linkage(mat);
                foreach (var elt in dict)
                {
                    Console.WriteLine("=====");
                    Console.WriteLine(string.Join(" ", corpus.Documents.ElementAt(elt.Key[0]).Content));
                    Console.WriteLine(string.Join(" ", corpus.Documents.ElementAt(elt.Key[1]).Content));
                }

            }
            
        }

    }
}
