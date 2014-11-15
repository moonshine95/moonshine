using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Moonshine.Aggregator.News
{
    public class News
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Category Category { get; set; }

        public News()
        {

        }
    }
}
