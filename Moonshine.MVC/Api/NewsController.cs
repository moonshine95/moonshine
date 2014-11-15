using Moonshine.Aggregator.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Moonshine.Aggregator;

namespace Moonshine.MVC.Api
{
    public class NewsController : ApiController
    {
        // GET api/news
        List<News> news = Aggregator.Aggregator.Aggregate();

        public object Get()
        {
            object objForm = new { news };
            return objForm;
        }

        // GET api/news/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/news
        public void Post([FromBody]string value)
        {
        }

        // PUT api/news/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/news/5
        public void Delete(int id)
        {
        }
    }
}
