using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moonshine.Aggregator.News
{
    public class Rules
    {
        private List<string> _xpathsToRemove;
        private string _articleXpath;

        public string ArticleXpath 
        {
            get
            {
                return _articleXpath;
            }        
        }

        public List<string> XpathToRemove
        {
            get
            {
                return _xpathsToRemove;
            }
        }

        public Rules(string ArticleXpath, List<string> XpathToRemove)
        {
            _articleXpath = ArticleXpath;
            _xpathsToRemove = XpathToRemove;
        }
    }
}
