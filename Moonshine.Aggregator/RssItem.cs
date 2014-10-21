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
        public string Link { get; set; }
        public string Description { get; set; }
        public DateTime PubDate { get; set; }
        public string ImageUrl { get; set; } 

        public RssItem(string title, string link, string description, DateTime pubDate, string imageUrl)
        {
            Title = title;
            Link = link;
            Description = description;
            PubDate = pubDate;
            ImageUrl = imageUrl;
        }
    }
}
