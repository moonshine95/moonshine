using Moonshine.Aggregator.Rss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moonshine.Aggregator.Clustering
{
    public class Document
    {
        public RssItem RssItem { get; set; } 
        public string[] Content { get; private set; }

        public Document(RssItem item)
        {
            string[] description = Helpers.Tokenize(item.Description);
            string[] title = Helpers.Tokenize(item.Title);
            Content = description.Concat(title).ToArray();

            RssItem = item;
        }

        public void Append(string content)
        {
            Content = Content.Concat(Helpers.Tokenize(content)).ToArray();
        }

        public string[] GetDistinctContent()
        {
            return Content.Distinct().ToArray();
        }
    }
}
