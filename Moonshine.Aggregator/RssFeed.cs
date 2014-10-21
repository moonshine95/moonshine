using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moonshine.Aggregator
{
    public class RssFeed
    {
        public string Title { get; set; }
        public Uri Link { get; set; }
        public string Description { get; set; }
        public DateTime PubDate { get; set; }
        public List<RssItem> RssItems { get; set; }

        public RssFeed(string title, Uri link, string description, DateTime pubDate)
        {
            Title = title;
            Link = link;
            Description = description;
            PubDate = pubDate;
            RssItems = new List<RssItem>();
        }

        public RssFeed()
        {

        }
    }
}
