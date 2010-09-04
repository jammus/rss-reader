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
    public class AtomFeedParserTest
    {
        [TestMethod]
        public void RDFFeedParser_should_generate_valid_Feed_details_for_AtomTest_Feed()
        {
            // Arrange
            XmlDocument xmlDoc = FakeXMLFeed.GetFakeXMLFeed("atomtest");
            AtomFeedParser rssFeedParser = new AtomFeedParser(xmlDoc);

            // Act
            Feed feed = rssFeedParser.ReadFeedDetails();

            // Assert
            Assert.AreEqual("Example Feed", feed.Name);
        }

        [TestMethod]
        public void ReadItems_Should_Return_1_News_Items_From_AtomTest_Feed()
        {
            // Arrange
            XmlDocument xmlDoc = FakeXMLFeed.GetFakeXMLFeed("atomtest");
            AtomFeedParser rssFeedParser = new AtomFeedParser(xmlDoc);

            // Act
            var items = rssFeedParser.ReadItems();

            // Assert
            Assert.AreEqual(1, items.Count);
        }

        [TestMethod]
        public void ReadItems_Should_News_Items_With_Headline_Date_And_Link_From_Flimsy_Feed()
        {
            // Arrange
            XmlDocument xmlDoc = FakeXMLFeed.GetFakeXMLFeed("flimsy");
            AtomFeedParser rssFeedParser = new AtomFeedParser(xmlDoc);

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
