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
            using (var stream = new StreamReader("..//..//..//Moonshine.Aggregator//Resources//RssFeeds.json"))
            {
                string json = stream.ReadToEnd();

                var deserializer = new JavaScriptSerializer();
                var rssFeeds = deserializer.Deserialize<List<RssFeed>>(json);

                foreach (var feed in rssFeeds)
                {
                    RssManager.Read(feed);
                    Logger.Log(feed);
                }
            }
        }
    }
}
