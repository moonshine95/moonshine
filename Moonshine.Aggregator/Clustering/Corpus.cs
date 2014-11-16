using Moonshine.Aggregator.Rss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moonshine.Aggregator.Clustering
{
    public class Corpus
    {
        public List<RssFeed> RssFeeds { get; set; }
        public List<Document> Documents { get; set; }

        public Corpus(List<RssFeed> feeds)
        {
            Documents = new List<Document>();
            foreach (var feed in feeds)
            {
                foreach (var item in feed.RssItems)
                {
                    Documents.Add(new Document(item));
                }
            }

            RssFeeds = feeds;
        }
    }
}
