using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Moonshine.Aggregator.Rss;
using Moonshine.Aggregator.News;
using System.Web;
using IronPython;
using IronPython.Hosting;
using System.Diagnostics;

namespace Moonshine.Aggregator
{
    public static class Aggregator
    {
        public static List<News.News> Aggregate()
        {
            var news = new List<News.News>();

            using (var stream = new StreamReader(HttpContext.Current.Server.MapPath("~/App_Data/RssFeeds.json")))
            {
                string json = stream.ReadToEnd();

                var deserializer = new JavaScriptSerializer();
                var rssFeeds = deserializer.Deserialize<List<RssFeed>>(json);

                var lockMe = new object();
                Parallel.ForEach(rssFeeds, feed =>
                {
                    RssManager.Read(feed);
                    Parallel.ForEach(feed.RssItems, item =>
                    {
                        var _news = NewsManager.CreateNews(item, feed.Rules);
                        lock (lockMe)
                        {
                            news.Add(_news);
                        }
                    });
                });
            }

            return news;
        }

        public static void test()
        {
            string importScript = "import sys" + Environment.NewLine +
                                  "sys.path.append( r\"{0}\" )" + Environment.NewLine +
                                  "from {1} import *";

            // python script to load
            string fullPath = @"c:\git\script2.py";

            var engine = Python.CreateEngine();
            var scope = engine.CreateScope();

            //string dir = Path.GetDirectoryName(@"C:\Python27\Lib\site-packages\");
            //ICollection<string> paths = engine.GetSearchPaths();
            //paths.Add(dir);
            //engine.SetSearchPaths(paths);

            // import the module
            string scriptStr = string.Format(importScript,
                                             Path.GetDirectoryName(fullPath),
                                             Path.GetFileNameWithoutExtension(fullPath));
            var importSrc = engine.CreateScriptSourceFromString(scriptStr, Microsoft.Scripting.SourceCodeKind.File);
            importSrc.Execute(scope);

            // now you ca execute one-line expressions on the scope e.g.
            string expr = "functionOfMyModule()";
            var result = engine.Execute(expr, scope);
        }
    }
}
