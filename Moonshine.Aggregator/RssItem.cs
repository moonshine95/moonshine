using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moonshine.Aggregator
{
    public class RssItem
    {
        public string Title { get; set; }
        public Uri Link { get; set; }
        public string Description { get; set; }
        public DateTime PubDate { get; set; }
        public Uri ImageUrl { get; set; } 

        public RssItem(string title, Uri link, string description, DateTime pubDate, Uri imageUrl)
        {
            Title = title;
            Link = link;
            Description = description;
            PubDate = pubDate;
            ImageUrl = imageUrl;
        }

        public RssItem()
        {

        }
    }
}
