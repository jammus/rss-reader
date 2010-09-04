using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace RSSReader.Models
{
    public class RSSFeedReader
    {
        private FeedParser FeedParser;

        public RSSFeedReader(XmlDocument xmlDoc)
        {
            FeedParser = RSSFeedParserFactory(xmlDoc);
        }

        public Feed ReadFeedDetails()
        {
            return FeedParser.ReadFeedDetails();
        }

        public List<NewsItem> ReadItems()
        {
            return FeedParser.ReadItems();
        }

        private FeedParser RSSFeedParserFactory(XmlDocument xmlDoc)
        {
            string nodeName = xmlDoc.DocumentElement.Name;
            switch (nodeName)
            {
                case "rdf:RDF":
                    return new RDFFeedParser(xmlDoc);
                case "rss":
                    return new RSSFeedParser(xmlDoc);
                case "feed":
                    return new AtomFeedParser(xmlDoc);
                default:
                    return new UnknownFeedParser(xmlDoc);
            }
        }
    }
}
