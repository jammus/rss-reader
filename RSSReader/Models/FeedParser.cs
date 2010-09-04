using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace RSSReader.Models
{
    public abstract class FeedParser
    {
        protected XmlNamespaceManager namespaceManager;
        protected XmlDocument XmlDoc;

        protected string headerSelector;
        protected string nameSelector;

        protected string itemSelector;
        protected string headLineSelctor;
        protected string summarySelector;
        protected string datePublishedSelector;
        protected string urlSelector;

        public FeedParser(XmlDocument xmlDoc)
        {
            XmlDoc = xmlDoc;
        }

        public Feed ReadFeedDetails()
        {
            XmlNode node = XmlDoc.SelectSingleNode(headerSelector, namespaceManager);
            if (node != null)
            {
                Feed feed = new Feed();
                feed.Name = ParseNode(node, nameSelector, "Unknown Feed");
                return feed;
            }
            else
            {
                return null;
            }
        }

        public List<NewsItem> ReadItems()
        {
            XmlNodeList nodes = XmlDoc.SelectNodes(itemSelector, namespaceManager);

            List<NewsItem> items = new List<NewsItem>();
            foreach (XmlNode node in nodes)
            {
                NewsItem newsItem = new NewsItem();
                newsItem.Headline = ParseNode(node, headLineSelctor, "");
                newsItem.Summary = ParseNode(node, summarySelector, "");
                newsItem.DatePublished = ParseDate(ParseNode(node, datePublishedSelector, DateTime.MinValue.ToString()));

                string uri = ParseNode(node, urlSelector, "");
                if (uri != "")
                {
                    newsItem.Url = new Uri(uri);
                }
                items.Add(newsItem);
            }

            return items;
        }

        private string ParseNode(XmlNode node, string selector, string defaultValue)
        {
            XmlNode childNode = node.SelectSingleNode(selector, namespaceManager);
            if (childNode != null)
            {
                return childNode.InnerText;
            }
            else
            {
                return defaultValue;
            }
        }

        private DateTime ParseDate(string dateString)
        {
            // Inelegant solution to ticket #15 (shipping soon)
            
            DateTime dateTime;

            // Replace non-standard timezones with offsets.
            // TODO: Is this even required? Local time may make the most sense
            dateString = dateString.Replace("EST", "");
            dateString = dateString.Replace("CEST", "");
            dateString = dateString.Replace("CST", "");
            dateString = dateString.Replace("PST", "");
            dateString = dateString.Replace("PDT", "");
            dateString = dateString.Replace("UTC", "");
            dateString = dateString.Replace("GMT", "");

            try
            {
                dateTime = DateTime.Parse(dateString);
            }
            catch
            {
                // if is something still wrong then just take the min date
                dateTime = DateTime.MinValue;
            }

            return dateTime;
        }
    }
}
