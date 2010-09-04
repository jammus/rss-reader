using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace RSSReader.Models
{
    public class UnknownFeedParser : FeedParser
    {
        public UnknownFeedParser(XmlDocument xmlDoc) : base(xmlDoc) {
            namespaceManager = new XmlNamespaceManager(xmlDoc.NameTable);
            itemSelector = "*/item";
            headLineSelctor = "title";
            summarySelector = "description";
            datePublishedSelector = "date";
            urlSelector = "link";
        }
    }
}
