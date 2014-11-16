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

                var _list = new List<RssFeed>();
                foreach (var feed in rssFeeds)
                {
                    RssManager.Read(feed);
                    _list.Add(feed);
                    //myFeed = feed;
                    //break;
                }

                // CREATION D'UN CORPUS GRACE A UNE LISTE DE RSS FEED
                Corpus corpus = new Corpus(_list);

                // DEFINITION DES TOP KEYWORDS DU CORPUS
                string[] topKeywords = {};
                int id = 0;
                foreach (var doc in corpus.Documents)
                {
                    //Console.WriteLine(" === " + id + " === ");
                   // Console.WriteLine(string.Join(" ", doc.Content));
                    id++;
                    topKeywords = topKeywords.Concat(Helpers.TopKeywords(4, doc, corpus)).ToArray();
                }
                topKeywords = topKeywords.Distinct().ToArray();

                // CREATION DES FEATURES VECTORS
                List<double[]> vectors = Helpers.FeatureVectors(topKeywords, corpus);

                // CREATION DE LA MATRICE DE SIMILARITE
                List<double[]> matrix = new List<double[]>();
                for (int i = 0; i < corpus.Documents.Count; i++)
                {
                    double[] vector = {};
                    for (int j = 0; j < corpus.Documents.Count; j++)
                    {
                        //Console.WriteLine(Helpers.CosineSimilarity(vectors.ElementAt(i).ToList(), vectors.ElementAt(j).ToList()));
                        var value = Helpers.CosineSimilarity(vectors.ElementAt(i).ToList(), vectors.ElementAt(j).ToList());
                        vector = vector.Concat(new List<double> { value }).ToArray();
                        //_mat.Add(Helpers.CosineSimilarity(vectors.ElementAt(i).ToList(), vectors.ElementAt(j).ToList()));
                    }
                    matrix.Add(vector);
                }
                
                var clusters = Helpers.Linkage(matrix, (double)1);
                foreach (var cluster in clusters)
                {
                    var ids = cluster.Value;
                    Console.WriteLine("======");
                    for (int h = 0; h < corpus.Documents.Count; h++)
                    {
                        if (ids.Contains(h))
                        {
                            Console.WriteLine(string.Join(" ", corpus.Documents[h].Content));
                        }
                    }
                }


            }
            
        }

    }
}
