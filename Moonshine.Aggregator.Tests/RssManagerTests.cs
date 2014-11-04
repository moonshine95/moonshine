using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using Moonshine.Aggregator.Rss;
using Moonshine.Aggregator.News;

namespace Moonshine.Aggregator.Tests
{
    [TestClass]
    public class RssManagerTests
    {
        [TestMethod]
        public void Verify_Read_Rss_Not_Null()
        {
            // Assert.IsNotNull(RssManager.Read(new Uri("http://syndication.lesechos.fr/rss/rss_politique.xml")));

            var feed = RssManager.Read(new Uri("http://syndication.lesechos.fr/rss/rss_politique.xml"));
            
            
            StreamWriter log = new StreamWriter("log.html");
            log.WriteLine("<html>");
            log.WriteLine("<head><meta charset=\"UTF-8\"></head>");
            log.WriteLine("<body>");
            log.WriteLine("<h1> FEED </h1>");
            log.WriteLine("<ul>");
            log.WriteLine("<li><u>{0}</u></li>", feed.Title);
            log.WriteLine("<li><i>{0}</i></li>", feed.Description);
            log.WriteLine("<li>{0}</li>", feed.Link);
            log.WriteLine("<li>{0}</li>", feed.PubDate);
            log.WriteLine("</ul>");
            foreach(var item in feed.RssItems)
            {
                log.WriteLine(" <blockquote> ");
                log.WriteLine("<h2> ITEM </h2>");
                log.WriteLine("<ul>");
                log.WriteLine("<li><u>{0}</u></li>", item.Title);
                log.WriteLine("<li><i>{0}</i></li>", item.Description);
                log.WriteLine("<li>{0}</li>", item.Link);
                log.WriteLine("<li>{0}</li>", item.PubDate);
                log.WriteLine("<li>{0}</li>", item.ImageUrl);
                log.WriteLine("</ul>");
                log.WriteLine(" <blockquote> ");
                log.WriteLine("<h3> ARTICLE </h3>");
                var news = NewsManager.CreateNews(item, "//p[@itemprop=\"articleBody\"]");
                log.WriteLine("<ul>");
                log.WriteLine("<li><u>{0}</u></li>", news.Title);
                log.WriteLine("<li>{0}</li>", news.Category);
                log.WriteLine("<li>{0}</li>", news.Content);
                log.WriteLine("</ul>");
                log.WriteLine(" </blockquote> ");
                //log.WriteLine("{0}", news.Image);
                log.WriteLine(" </blockquote> ");
            }
            log.WriteLine("</body>");
            log.WriteLine("</html>");
            log.Close();
        }

        [TestMethod]
        public void Verify_Read_Rss_Null()
        {
            Assert.IsNull(RssManager.Read(new Uri("http://www.google.fr")));
        }

        [TestMethod]
        public void Verify_Generate_News()
        {
            RssFeed rssFeed = RssManager.Read(new Uri("http://syndication.lesechos.fr/rss/rss_politique.xml"));
            //NewsManager.CreateNews(rssFeed, rssFeed.RssItems[0]);
        }
    }
}
