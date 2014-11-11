using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Moonshine.Aggregator.Rss
{
    public static class RssManager
    {
        public static void Read(RssFeed rssFeed)
        {
            WebRequest request = WebRequest.Create(rssFeed.Link);
            WebResponse response = request.GetResponse();

            var xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(response.GetResponseStream());
                XmlElement channelElement = xmlDoc["rss"]["channel"];
                if (channelElement == null)
                {
                    return;
                }

                rssFeed.Title = channelElement["title"].InnerText;
                rssFeed.Description = channelElement["description"].InnerText;
                rssFeed.PubDate = new DateTime();

                XmlNodeList itemElements = channelElement.GetElementsByTagName("item");
                foreach (XmlElement item in itemElements)
                {
                    Uri imageUrl = null;

                    XmlElement enclosureElement = item["enclosure"];
                    if (enclosureElement != null)
                    {
                        if (enclosureElement.GetAttribute("type").Equals("image/jpeg"))
                        {
                            imageUrl = new Uri(enclosureElement.GetAttribute("url"));
                        }
                    }

                    var rssItem = new RssItem()
                    {
                        Title = item["title"].InnerText,
                        Link = new Uri(item["link"].InnerText),
                        Description = item["description"].InnerText,
                        PubDate = new DateTime(),
                        ImageUrl = imageUrl
                    };

                    rssFeed.RssItems.Add(rssItem);
                }
            }
            catch
            {
                return;        
            }
        }
    }
}
