using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RSSReader.Models;
using RSSReader.Tests.Fakes;

namespace RSSReader.Tests.Models
{
    [TestClass]
    public class FeedServiceTest
    {
        [TestMethod]
        public void GetUserFeeds_Returns_5_Feeds_For_jammus()
        {
            // Arrange
            FeedService feedService = new FeedService(new FakeFeedRepository());

            // Act
            var feeds = feedService.GetUsersFeeds("jammus");

            // Assert
            Assert.AreEqual(5, feeds.Count);
        }

        [TestMethod]
        public void Save_New_Feed_Should_Increase_Total_Feeds_By_1()
        {
            // Arrange
            FeedService feedService = new FeedService(new FakeFeedRepository());
            Feed newFeed = new Feed()
            {
                Name = "New Feed",
                UserName = "jammus",
                Url = "http://www.example.com/feed/"
            };

            // Act            
            feedService.Save(newFeed);
            var feeds = feedService.GetUsersFeeds("jammus");

            // Assert
            Assert.AreEqual(6, feeds.Count);
        }

        [TestMethod]
        public void Save_Existing_Feed_Should_Update_Feed_And_Keep_Total_Feeds_Unchanged()
        {
            // Arrange
            FeedService feedService = new FeedService(new FakeFeedRepository());
            Feed existingFeed = feedService.GetUsersFeed(1, "jammus");

            // Act
            existingFeed.Name = "Updated feed";
            feedService.Save(existingFeed);
            existingFeed = feedService.GetUsersFeed(1, "jammus");
            var feeds = feedService.GetUsersFeeds("jammus");

            // Assert
            Assert.AreEqual("Updated feed", existingFeed.Name);
            Assert.AreEqual(5, feeds.Count);
        }

        [TestMethod]
        public void Delete_Feed_Should_Remove_Feed_From_DataStore()
        {
            // Arrange
            FeedService feedService = new FeedService(new FakeFeedRepository());
            Feed feed = feedService.GetUsersFeed(1, "jammus");

            // Act
            feedService.Delete(feed);
            feed = feedService.GetUsersFeed(1, "jammus");
            var feeds = feedService.GetUsersFeeds("jammus");

            // Assert
            Assert.IsNull(feed);
            Assert.AreEqual(4, feeds.Count);
        }
    }
}
