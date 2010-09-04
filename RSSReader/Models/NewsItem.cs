using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RSSReader.Models
{
    public class NewsItem
    {
        public string Headline { get; set; }
        public string Summary { get; set; }
        public DateTime DatePublished { get; set; }
        public Uri Url { get; set; }
    }
}
