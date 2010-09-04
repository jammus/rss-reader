using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using RSSReader.Models;
using RSSReader.Tests.Fakes;

namespace RSSReader.Tests.Models
{
    [TestClass]
    public class RSSFeedReaderTest
    {
        [TestMethod]
        public void ReadItems_Should_Return_0_News_Items_From_Unknown_Feed_Type()
        {
            // Arrange
            XmlDocument xmlDoc = FakeXMLFeed.GetFakeXMLFeed("unknown");
            RSSFeedReader rssFeedReader = new RSSFeedReader(xmlDoc);

            // Act
            var items = rssFeedReader.ReadItems();

            // Assert
            Assert.AreEqual(0, items.Count);
        }
    }
}
