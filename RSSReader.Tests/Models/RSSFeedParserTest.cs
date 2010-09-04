using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RSSReader.Models;
using RSSReader.Tests.Fakes;

namespace RSSReader.Tests.Models
{
    [TestClass]
    public class RSSFeedParserTest
    {

        [TestMethod]
        public void ReadItems_Should_Return_39_News_Items_From_BBC_Feed()
        {
            // Arrange
            XmlDocument xmlDoc = FakeXMLFeed.GetFakeXMLFeed("bbc");
            RSSFeedParser rssFeedParser = new RSSFeedParser(xmlDoc);

            // Act
            var items = rssFeedParser.ReadItems();

            // Assert
            Assert.AreEqual(39, items.Count);
        }

        [TestMethod]
        public void RDFFeedParser_should_generate_valid_Feed_details_for_BBC_feed()
        {
            // Arrange
            XmlDocument xmlDoc = FakeXMLFeed.GetFakeXMLFeed("bbc");
            RSSFeedParser rssFeedParser = new RSSFeedParser(xmlDoc);

            // Act
            Feed feed = rssFeedParser.ReadFeedDetails();

            // Assert
            Assert.AreEqual("BBC News | News Front Page | UK Edition", feed.Name);
        }

        [TestMethod]
        public void ReadItems_Should_News_Items_With_Headline_Date_And_Link_From_BBC_Feed()
        {
            // Arrange
            RSSFeedParser rssFeedParser = new RSSFeedParser(FakeXMLFeed.GetFakeXMLFeed("bbc"));

            // Act
            var item = rssFeedParser.ReadItems()[0];

            // Assert
            Assert.IsNotNull(item.Headline);
            Assert.IsNotNull(item.Url.AbsoluteUri);
            Assert.IsNotNull(item.Summary);
            Assert.AreNotEqual(DateTime.MinValue, item.DatePublished);
        }

        [TestMethod]
        public void ReadItems_Should_News_Items_With_Headline_Date_And_Link_From_Dilbert_Feed()
        {
            // Added for ticket 15

            // Arrange
            XmlDocument xmlDoc = FakeXMLFeed.GetFakeXMLFeed("dilbert");
            RSSFeedParser rssFeedParser = new RSSFeedParser(xmlDoc);

            // Act
            var item = rssFeedParser.ReadItems()[0];

            // Assert
            Assert.IsNotNull(item.Headline, "Expected Headline");
            Assert.IsNotNull(item.Url, "Expected Url");
            Assert.IsNotNull(item.Summary, "Expected Summary");
            Assert.AreNotEqual(DateTime.MinValue, item.DatePublished);
        }

        [TestMethod]
        public void RDFFeedParser_should_generate_valid_Feed_details_for_Hacker_News_feed()
        {
            // Arrange
            XmlDocument xmlDoc = FakeXMLFeed.GetFakeXMLFeed("hackernews");
            RSSFeedParser rssFeedParser = new RSSFeedParser(xmlDoc);

            // Act
            Feed feed = rssFeedParser.ReadFeedDetails();

            // Assert
            Assert.AreEqual("Hacker News", feed.Name);
        }

        [TestMethod]
        public void ReadItems_Should_News_Items_With_Headline_Date_And_Link_From_Hacker_News_Feed()
        {
            // Added for ticket 15

            // Arrange
            XmlDocument xmlDoc = FakeXMLFeed.GetFakeXMLFeed("hackernews");
            RSSFeedParser rssFeedParser = new RSSFeedParser(xmlDoc);

            // Act
            var item = rssFeedParser.ReadItems()[0];

            // Assert
            Assert.IsNotNull(item.Headline, "Expected Headline");
            Assert.IsNotNull(item.Url, "Expected Url");
            Assert.IsNotNull(item.Summary, "Expected Summary");
            Assert.AreNotEqual(DateTime.MinValue, item.DatePublished);
        }
    }
}
