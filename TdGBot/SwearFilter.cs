using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TdGBot
{
    internal class SwearFilter
    {
        public List<string> swearWords = new List<string>();
        public SwearFilter()
        {
            swearWords.Add("http");
            swearWords.Add("shit");
            swearWords.Add("bastard");
            swearWords.Add("coon");
        }
    }
}
