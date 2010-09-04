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
    public class RDFFeedParserTest
    {
        [TestMethod]
        public void RDFFeedParser_should_generate_valid_Feed_details_for_Slashdot()
        {
            // Arrange
            XmlDocument xmlDoc = FakeXMLFeed.GetFakeXMLFeed("slashdot");
            var rssFeedParser = new RDFFeedParser(xmlDoc);

            // Act
            Feed feed = rssFeedParser.ReadFeedDetails();

            // Assert
            Assert.AreEqual("Slashdot", feed.Name);
        }

        [TestMethod]
        public void ReadItems_Should_Return_15_News_Items_From_Slashdot_Feed()
        {
            // Arrange
            XmlDocument xmlDoc = FakeXMLFeed.GetFakeXMLFeed("slashdot");
            var rssFeedParser = new RDFFeedParser(xmlDoc);

            // Act
            var items = rssFeedParser.ReadItems();

            // Assert
            Assert.AreEqual(15, items.Count);
        }

        [TestMethod]
        public void ReadItems_Should_News_Items_With_Headline_Date_And_Link_From_Slashdot_Feed()
        {
            // Arrange
            RDFFeedParser rssFeedParser = new RDFFeedParser(FakeXMLFeed.GetFakeXMLFeed("slashdot"));

            // Act
            var item = rssFeedParser.ReadItems()[0];

            // Assert
            Assert.IsNotNull(item.Headline);
            Assert.IsNotNull(item.Url.AbsoluteUri);
            Assert.IsNotNull(item.Summary);
            Assert.AreNotEqual(DateTime.MinValue, item.DatePublished);
        }
    }
}
