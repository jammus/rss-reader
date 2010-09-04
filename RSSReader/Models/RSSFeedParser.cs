using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace RSSReader.Models
{
    public class RSSFeedParser : FeedParser
    {
        public RSSFeedParser(XmlDocument xmlDoc)
            : base(xmlDoc)
        {
            namespaceManager = new XmlNamespaceManager(XmlDoc.NameTable);
            namespaceManager.AddNamespace("rss", "http://purl.org/rss/1.0/");

            headerSelector = "/rss/channel";
            nameSelector = "title";

            itemSelector = "/rss/channel/item";
            headLineSelctor = "title";
            summarySelector = "description";
            datePublishedSelector = "pubDate";
            urlSelector = "link";
        }
    }
}
