using Moonshine.Aggregator.News;
using Moonshine.Aggregator.Rss;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moonshine.Aggregator
{
    public static class Logger
    {
        public static void Log(RssFeed feed, string source)
        {
            StreamWriter log = new StreamWriter(String.Format("{0}.html", source));
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
            foreach (var item in feed.RssItems)
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
    }
}
