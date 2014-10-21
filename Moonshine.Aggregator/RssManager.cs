using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Moonshine.Aggregator
{
    public static class RssManager
    {
        public static RssFeed Read(Uri uri)
        {
            WebRequest request = WebRequest.Create(uri);
            WebResponse response = request.GetResponse();

            var xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(response.GetResponseStream());
                Console.WriteLine(xmlDoc.InnerXml);
            }
            catch
            {
            }

            return new RssFeed();
        }
    }
}
