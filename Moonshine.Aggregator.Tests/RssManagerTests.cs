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
            
            List<string> xpaths = new List<string>();
            xpaths.Add("//div[@class=\"encadre\"]");
            xpaths.Add("//div[@class=\"signature\"]");

            Dictionary<string, string> xpaths2 = new Dictionary<string, string>();
            xpaths2.Add("//a", "None");
            xpaths2.Add("//h2[@class=\"intertitre\"]", "h2");

            var rules = new Rules("//div[@class=\"contenu_article\"]", xpaths, xpaths2);

            Logger.Log(feed, "LES ECHOS - Politique", rules);

            //var feed2 = RssManager.Read(new Uri("http://syndication.lesechos.fr/rss/rss_france.xml"));
            //Logger.Log(feed2, "LES ECHOS - Economie", "//div[@class=\"contenu_article\"]");

            //var feed3 = RssManager.Read(new Uri("http://syndication.lesechos.fr/rss/rss_monde.xml"));
            //Logger.Log(feed3, "LES ECHOS - Monde", "//div[@class=\"contenu_article\"]");

            //var feed4 = RssManager.Read(new Uri("http://syndication.lesechos.fr/rss/rss_tech_medias.xml"));
            //Logger.Log(feed4, "LES ECHOS - Medias", "//div[@class=\"contenu_article\"]");
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
