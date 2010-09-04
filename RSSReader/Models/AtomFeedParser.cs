using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace RSSReader.Models
{
    public class AtomFeedParser : FeedParser
    {
        public AtomFeedParser(XmlDocument xmlDoc)
            : base(xmlDoc)
        {
            namespaceManager = new XmlNamespaceManager(XmlDoc.NameTable);
            namespaceManager.AddNamespace("rss", "http://www.w3.org/2005/Atom");

            headerSelector = "/rss:feed";
            nameSelector = "rss:title";

            itemSelector = "/rss:feed/rss:entry";
            headLineSelctor = "rss:title";
            summarySelector = "rss:summary";
            datePublishedSelector = "rss:updated";
            urlSelector = "rss:link[@rel='alternate']/@href";
        }
    }
}
