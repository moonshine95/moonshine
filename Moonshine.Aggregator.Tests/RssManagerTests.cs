using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Web.Script.Serialization;
using System.Collections.Generic;

namespace Moonshine.Aggregator.Tests
{
    [TestClass]
    public class RssManagerTests
    {
        [TestMethod]
        public void Verify_Read_Method()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Moonshine.Aggregator\\data\\rssFeeds.json");
            using (StreamReader stream = new StreamReader(path))
            {
                string json = stream.ReadToEnd();

                var deserializer = new JavaScriptSerializer();
                List<string> listOfRssFeeds = deserializer.Deserialize<List<string>>(json);

                foreach (var url in listOfRssFeeds)
                {
                    Assert.IsNotNull(RssManager.Read(new Uri(url)));
                }
            }
        }
    }
}
