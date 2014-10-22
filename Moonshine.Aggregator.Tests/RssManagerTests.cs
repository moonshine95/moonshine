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
            Assert.IsNotNull(RssManager.Read(new Uri("http://syndication.lesechos.fr/rss/rss_politique.xml")));
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
            NewsManager.generateNews(rssFeed, rssFeed.RssItems[0]);
        }
    }
}
