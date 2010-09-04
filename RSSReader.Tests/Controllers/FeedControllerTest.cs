using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using Moq;
using RSSReader.Models;
using RSSReader.ViewModels;
using RSSReader.Controllers;
using RSSReader.Tests.Fakes;

namespace RSSReader.Tests.Controllers
{
    [TestClass]
    public class FeedControllerTest
    {
        FeedController CreateFeedController()
        {
            var feedService = new FeedService(new FakeFeedRepository());
            var feedLoader = new FakeFeedLoader();
            return new FeedController(feedService, feedLoader);
        }

        FeedController CreateFeedControllerAs(string userName)
        {
            var mock = new Mock<ControllerContext>();
            mock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns(userName);
            mock.SetupGet(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(true);
            mock.SetupGet(p => p.HttpContext.Request["X-Requested-With"]).Returns("");
            
            var controller = CreateFeedController();
            controller.ControllerContext = mock.Object;

            return controller;
        }

        FeedController CreateAjaxRequestFeedControllerAs(string userName)
        {
            var mock = new Mock<ControllerContext>();
            mock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns(userName);
            mock.SetupGet(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(true);
            mock.SetupGet(p => p.HttpContext.Request["X-Requested-With"]).Returns("XMLHttpRequest");

            var controller = CreateFeedController();
            controller.ControllerContext = mock.Object;

            return controller;
        }

        [TestMethod]
        public void Index_Should_Return_A_FeedReaderViewModel_For_Logged_In_User()
        {
            // Arrange
            var controller = CreateFeedControllerAs("anyuser");

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(FeedReaderViewModel));
        }

        [TestMethod]
        public void Index_Should_Return_A_FeedReaderViewModel_With_5_Feeds_For_User_jammus()
        {
            // Arrange
            var controller = CreateFeedControllerAs("jammus");

            // Act
            var result = controller.Index() as ViewResult;
            var model = result.ViewData.Model as FeedReaderViewModel;

            // Assert
            Assert.AreEqual(5, model.Feeds.Count);
        }

        [TestMethod]
        public void Add_Action_Should_Return_A_View_For_Logged_In_User()
        {
            // Arrange
            var controller = CreateFeedControllerAs("jammus");

            // Act
            var result = controller.Add() as ViewResult;
            var model = result.ViewData.Model;

            // Assert
            Assert.IsInstanceOfType(model, typeof(Feed));
        }

        [TestMethod]
        public void Add_Action_Should_Return_To_Index_When_Successful()
        {
            // Arrange
            var controller = CreateFeedControllerAs("jammus");
            FormCollection formCollection = new FormCollection()
            {
                {"Url", "http://rss.slashdot.org/Slashdot/slashdot"}
            };
            controller.ValueProvider = formCollection.ToValueProvider();

            // Act
            var result = controller.Add(formCollection) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Index", result.RouteValues["Action"]);

        }

        [TestMethod]
        public void Add_Action_Should_Add_New_Feed_Called_Slashdot_When_Successful()
        {
            // Arrange
            var controller = CreateFeedControllerAs("jammus");
            FormCollection formCollection = new FormCollection()
            {
                {"Url", "http://rss.slashdot.org/Slashdot/slashdot"}
            };
            controller.ValueProvider = formCollection.ToValueProvider();

            // Act
            var addResult = controller.Add(formCollection);
            var indexResult = controller.Index() as ViewResult;
            var model = indexResult.ViewData.Model as FeedReaderViewModel;
            var feed = model.Feeds.OrderByDescending(f => f.FeedId).First();

            // Assert
            Assert.AreEqual(6, model.Feeds.Count);
            Assert.AreEqual("Slashdot", feed.Name);
        }

        [TestMethod]
        public void Add_Action_Should_Return_View_When_Unsuccessful()
        {
            // Arrange
            var controller = CreateFeedControllerAs("jammus");
            FormCollection formCollection = new FormCollection()
            {
                {"Url", "http://www.invalidurl.org/404.xml"}
            };
            controller.ValueProvider = formCollection.ToValueProvider();

            // Act
            var result = controller.Add(formCollection) as ViewResult;

            // Assert
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(Feed));
        }

        [TestMethod]
        public void Delete_Action_Should_Return_View_For_Valid_Feed()
        {
            // Arrange
            var controller = CreateFeedControllerAs("jammus");

            // Act
            var result = controller.Delete(1) as ViewResult;

            // Assert
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(Feed));
        }

        [TestMethod]
        public void Delete_Action_Should_Return_NotFound_For_Invalid_Feed()
        {
            // Arrange
            var controller = CreateFeedControllerAs("jammus");

            // Act
            var result = controller.Delete(-1) as ViewResult;

            // Assert
            Assert.AreEqual("NotFound", result.ViewName);
        }

        [TestMethod]
        public void Delete_Action_Should_Redirect_To_Index_When_Successful()
        {
            // Arrange
            var controller = CreateFeedControllerAs("jammus");

            // Act
            var result = controller.Delete(1, "Delete") as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Index", result.RouteValues["Action"]);
        }

        [TestMethod]
        public void Delete_Action_Should_Reduce_Number_Of_Feeds_By_One_When_Successful()
        {
            // Arrange
            var controller = CreateFeedControllerAs("jammus");

            // Act
            var deleteResult = controller.Delete(1, "Delete");
            var indexResult = controller.Index() as ViewResult;
            var model = indexResult.ViewData.Model as FeedReaderViewModel;

            // Assert
            Assert.AreEqual(4, model.Feeds.Count);
        }

        [TestMethod]
        public void Index_Should_Default_CurrentFeed_To_First_Feed_Alphabetically_When_Feeds_Exist_But_None_Is_Specified()
        {
            // Arrange
            var controller = CreateFeedControllerAs("jammus");

            // Act
            var result = controller.Index() as ViewResult;
            var model = result.ViewData.Model as FeedReaderViewModel;

            // Assert
            Assert.AreEqual("Blog - Stack Overflow", model.CurrentFeed.Name);
            Assert.AreEqual(3, model.CurrentFeedId);
        }

        [TestMethod]
        public void Index_Should_Have_No_CurrentFeed_When_No_Feeds_Exist()
        {
            // Arrange
            var controller = CreateFeedControllerAs("newuser");

            // Act
            var result = controller.Index() as ViewResult;
            var model = result.ViewData.Model as FeedReaderViewModel;

            // Assert
            Assert.IsNull(model.CurrentFeed);
            Assert.AreEqual(0, model.CurrentFeedId);
        }

        [TestMethod]
        public void View_Should_Return_Index_View()
        {
            // Arrange
            var controller = CreateFeedControllerAs("jammus");

            // Act
            var result = controller.View(null) as ViewResult;
            
            // Assert
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void View_Should_Set_Current_Feed_To_Specified_Feed_If_It_Exist()
        {
            // Arrange
            var controller = CreateFeedControllerAs("jammus");

            // Act
            var result = controller.View(4) as ViewResult;
            var model = result.ViewData.Model as FeedReaderViewModel;

            // Assert
            Assert.AreEqual(4, model.CurrentFeedId);
        }

        [TestMethod]
        public void View_Should_Set_Current_Feed_To_First_Alphabetically_If_Non_Is_Specified()
        {
            // Arrange
            var controller = CreateFeedControllerAs("jammus");

            // Act
            var result = controller.View(null) as ViewResult;
            var model = result.ViewData.Model as FeedReaderViewModel;

            // Assert
            Assert.AreEqual(3, model.CurrentFeedId);
        }

        [TestMethod]
        public void View_Should_Set_CurrentFeed_To_First_Alphabetically_If_Invalid_Feed_Is_Specified()
        {
            // Arrange
            var controller = CreateFeedControllerAs("jammus");

            // Act
            var result = controller.View(6) as ViewResult; // Feed 6 belongs to Barry
            var model = result.ViewData.Model as FeedReaderViewModel;

            // Assert
            Assert.AreEqual(3, model.CurrentFeedId);
        }

        [TestMethod]
        public void View_Should_Return_10_Items_For_Brent_Ozar_Feed()
        {
            // Arrange
            var controller = CreateFeedControllerAs("jammus");

            // Act
            var result = controller.View(1) as ViewResult;
            var model = result.ViewData.Model as FeedReaderViewModel;

            // Assert
            Assert.AreEqual(10, model.NewsItems.Count);
        }

        [TestMethod]
        public void Async_Call_To_View_Action_Should_Return_ChannelPanel_Partial_View()
        {
            // Arrange
            var controller = CreateAjaxRequestFeedControllerAs("jammus");

            // Act
            var result = controller.View(2) as PartialViewResult;
            
            // Assert
            Assert.AreEqual("ChannelPanel", result.ViewName);
        }

        [TestMethod]
        public void Async_Call_To_Add_Action_Should_Return_True_When_Successful()
        {
            // Arrange
            var controller = CreateAjaxRequestFeedControllerAs("jammus");
            FormCollection formCollection = new FormCollection()
            {
                {"Url", "http://rss.slashdot.org/Slashdot/slashdot"}
            };
            controller.ValueProvider = formCollection.ToValueProvider();

            // Act
            var result = controller.Add(formCollection) as JsonResult;
            IDictionary<string, object> data = new System.Web.Routing.RouteValueDictionary(result.Data);

            // Assert
            Assert.AreEqual(true, data["success"]);
        }

        [TestMethod]
        public void Async_Call_To_Add_Action_Should_Return_False_When_Successful()
        {
            // Arrange
            var controller = CreateAjaxRequestFeedControllerAs("jammus");
            FormCollection formCollection = new FormCollection()
            {
                {"Url", "http://www.invalidurl.org/404.xml"}
            };
            controller.ValueProvider = formCollection.ToValueProvider();

            // Act
            var result = controller.Add(formCollection) as JsonResult;
            IDictionary<string, object> data = new System.Web.Routing.RouteValueDictionary(result.Data);

            // Assert
            Assert.AreEqual(false, data["success"]);
        }

        [TestMethod]
        public void List_Action_Should_Return_Index_View()
        {
            // Arrange
            var controller = CreateFeedControllerAs("jammus");

            // Act
            var result = controller.List() as ViewResult;

            // Assert
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void Async_Call_To_List_Action_Should_Return_FeedList_PartialView()
        {
            // Arrange
            var controller = CreateAjaxRequestFeedControllerAs("jammus");
            
            // Act
            var result = controller.List() as PartialViewResult;

            // Assert
            Assert.AreEqual("FeedList", result.ViewName);
        }

        [TestMethod]
        public void Async_Call_To_Delete_Action_Should_Return_True_For_Success()
        {
            // Arrange
            var controller = CreateAjaxRequestFeedControllerAs("jammus");
            
            // Act
            var result = controller.Delete(1, "Delete") as JsonResult;
            IDictionary<string, object> data = new System.Web.Routing.RouteValueDictionary(result.Data);

            // Assert
            Assert.AreEqual(true, data["success"]);
        }
    }
}
