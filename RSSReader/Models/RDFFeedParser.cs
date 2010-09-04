using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace RSSReader.Models
{
    public class RDFFeedParser : FeedParser
    {
        public RDFFeedParser(XmlDocument xmlDoc)
            : base(xmlDoc)
        {
            namespaceManager = new XmlNamespaceManager(XmlDoc.NameTable);
            namespaceManager.AddNamespace("rdf", "http://www.w3.org/1999/02/22-rdf-syntax-ns#");
            namespaceManager.AddNamespace("dc", "http://purl.org/dc/elements/1.1/");
            namespaceManager.AddNamespace("rss", "http://purl.org/rss/1.0/");

            headerSelector = "/rdf:RDF/rss:channel";
            nameSelector = "rss:title";

            itemSelector = "/rdf:RDF/rss:item";
            headLineSelctor = "rss:title";
            summarySelector = "rss:description";
            datePublishedSelector = "dc:date";
            urlSelector = "rss:link";
        }
    }
}
