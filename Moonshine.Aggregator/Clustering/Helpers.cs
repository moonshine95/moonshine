using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moonshine.Aggregator.Clustering
{
    public static class Helpers
    {
        private static char[] PUNCK = new char[] { ' ', '.', '?', ',', '\'', '(', ')', '\"', ':' };

        public static string[] Tokenize(string str)
        {
            var html = new HtmlDocument();
            html.LoadHtml(str);
            str = html.DocumentNode.InnerText;
            var _str = str.Split(PUNCK, StringSplitOptions.RemoveEmptyEntries);
            return _str.Select(s => s.ToLowerInvariant()).ToArray();
        }

        public static int Frequency(string word, Document document)
        {
            int count = 0;
            foreach (var _word in document.Content)
            {
                if (_word.Equals(word))
                {
                    count++;
                }
            }
            return count;
        }

        public static int WordCount(Document document)
        {
            return document.Content.Length;
        }

        public static int NumberOfDocContaining(string word, Corpus corpus)
        {
            int count = 0;
            foreach (var doc in corpus.Documents)
            {
                if (Frequency(word, doc) > 0)
                {
                    count++;
                }
            }
            return count;
        }

        public static double TermFrequency(string word, Document document)
        {
            return (double)Frequency(word, document) / document.Content.Length;
        }
        
        public static double InverseDocumentFrequency(string word, Corpus corpus)
        {
            return Math.Log(corpus.Documents.Count()) / NumberOfDocContaining(word, corpus);
        }

        public static double TfIdf(string word, Document document, Corpus corpus)
        {
            return TermFrequency(word, document) * InverseDocumentFrequency(word, corpus);
        }
 
        public static string[] TopKeywords(int count, Document document, Corpus corpus)
        {
            var dict = new Dictionary<string, double>();
            foreach (var word in document.GetDistinctContent())
            {
                dict.Add(word, TfIdf(word, document, corpus));
            }
            
            return dict.OrderByDescending(entry => entry.Value)
                               .Take(count)
                               .ToDictionary(pair => pair.Key, pair => pair.Value)
                               .Keys
                               .ToArray();
        }

        public static List<double[]> FeatureVectors(string[] topKeywords, Corpus corpus)
        {
            var featureVectors = new List<double[]>();

            foreach (var document in corpus.Documents)
            {
                var vector = new List<double>();
                foreach (var keyword in topKeywords)
                {
                    if (document.Content.Contains(keyword))
                    {
                        vector.Add(TfIdf(keyword, document, corpus));
                    }
                    else
                    {
                        vector.Add((double)0);
                    }
                }
                featureVectors.Add(vector.ToArray());
            }

            return featureVectors;
        }

        //http://stackoverflow.com/questions/7560760/cosine-similarity-code-non-term-vectors
        public static double CosineSimilarity(List<double> V1, List<double> V2)
        {
            double sim = 0.0d;
            int N = 0;
            N = ((V2.Count < V1.Count) ? V2.Count : V1.Count);
            double dot = 0.0d;
            double mag1 = 0.0d;
            double mag2 = 0.0d;
            for (int n = 0; n < N; n++)
            {
                dot += V1[n] * V2[n];
                mag1 += Math.Pow(V1[n], 2);
                mag2 += Math.Pow(V2[n], 2);
            }

            return dot / (Math.Sqrt(mag1) * Math.Sqrt(mag2));
        }

        public static double EuclideanDistance(double[] v1, double[] v2)
        {
            double dist = 0;
            for (int i = 0; i < v1.Length; i++)
            {
                dist = dist + Math.Pow(v2[i] - v1[i], 2);
            }
            return Math.Sqrt(dist);
        }

        public static Dictionary<int, List<int>> Linkage(List<double[]> matrix, double threshold)
        {
            int id = 0;
            var clusters = new List<Cluster>();
            foreach (var vector in matrix)
            {
                clusters.Add(new Cluster(vector, id));
                id++;
            }

            var temp = new Dictionary<int, List<int>>();
            while (clusters.Count != 1)
            {
                Cluster clusterA = null;
                Cluster clusterB = null;
                double distMin = 1000;
                for (int i = 0; i < clusters.Count; i++)
                {
                    for (int j = 0; j < i + 1; j++)
                    {
                        if (i != j)
                        {
                            double dist = clusters.ElementAt(i).DistanceTo(clusters.ElementAt(j));
                            if (dist < distMin)
                            {
                                clusterA = clusters.ElementAt(i);
                                clusterB = clusters.ElementAt(j);
                                distMin = dist;
                            }
                        }
                    }
                }
                if (clusterA != null & clusterB != null)
                {
                    //Console.WriteLine("dist : " + distMin + " / idA : " + clusterA.Id + " / idB : " + clusterB.Id  );
                    var _clust = new Cluster(clusterA, clusterB, distMin);
                    clusters.Add(_clust);
                    if (distMin < threshold)
                    {
                        if (clusterB.Id > 0 && clusterA.Id > 0)
                        {
                            temp.Add(_clust.Id, new List<int> { clusterA.Id, clusterB.Id });
                        }
                        if (clusterA.Id < 0)
                        {
                            var _list = temp[clusterA.Id];
                            _list.Add(clusterB.Id);
                            temp[clusterA.Id] = _list;
                        }
                        if (clusterB.Id < 0)
                        {
                            var _list = temp[clusterB.Id];
                            _list.Add(clusterA.Id);
                            temp[clusterB.Id] = _list;
                        }
                    }
                    clusters.Remove(clusterA);
                    clusters.Remove(clusterB);
                }
            }

            return temp;
        }
    }
}
