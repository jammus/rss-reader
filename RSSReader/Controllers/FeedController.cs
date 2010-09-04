using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Xml;

using RSSReader.Models;
using RSSReader.ViewModels;

namespace RSSReader.Controllers
{
    [Authorize]
    public class FeedController : Controller
    {
        private FeedService FeedService;
        private IFeedLoader FeedLoader;

        public FeedController() : this(new FeedService(), new RemoteFeedLoader()) { }

        public FeedController(FeedService feedService) : this(feedService, new RemoteFeedLoader()) { }

        public FeedController(FeedService feedService, IFeedLoader feedLoader)
        {
            FeedService = feedService;
            FeedLoader = feedLoader;
        }

        public ActionResult Index()
        {
            return View(CreateViewModel(null, HttpContext.User.Identity.Name));
        }

        public ActionResult View(int? Id)
        {
            var model = CreateViewModel(Id, HttpContext.User.Identity.Name);
            if (Request.IsAjaxRequest())
            {
                return PartialView("ChannelPanel", model.NewsItems);
            }
            else
            {
                return View("Index", model);
            }
        }

        public ActionResult List()
        {
            var model = CreateViewModel(null, HttpContext.User.Identity.Name);
            if (Request.IsAjaxRequest())
            {
                return PartialView("FeedList", model);
            }
            else
            {
                return View("Index", model);
            }
        }

        public ActionResult Add()
        {
            return View(new Feed());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Add(FormCollection formCollection)
        {
            string feedUrl = formCollection["Url"];
            XmlDocument xmlDoc = FeedLoader.LoadXML(feedUrl);
            Feed feed = FeedService.CreateFeedFromXmlDoc(xmlDoc, HttpContext.User.Identity.Name);

            if (Request.IsAjaxRequest())
            {
                return Json(new { success = (feed != null) });
            }
            else
            {
                if (feed != null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    feed = new Feed()
                    {
                        Url = feedUrl
                    };
                    return View(feed);
                }
            }
        }

        public ActionResult Delete(int Id)
        {
            Feed feed = FeedService.GetUsersFeed(Id, HttpContext.User.Identity.Name);
            if (feed != null)
            {
                return View(feed);
            }
            else
            {
                return View("NotFound");
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(int Id, string buttonName)
        {
            Feed feed = FeedService.GetUsersFeed(Id, HttpContext.User.Identity.Name);
            FeedService.Delete(feed);
            if (Request.IsAjaxRequest())
            {
                return Json(new { success = true });
            }
            else
            {
                return RedirectToAction("Index"); 
            }
        }

        private FeedReaderViewModel CreateViewModel(int? Id, string userName)
        {
            FeedReaderViewModel viewModel = new FeedReaderViewModel();
            viewModel.Feeds = FeedService.GetUsersFeeds(userName)
                .OrderBy(f => f.Name)
                .ToList();


            viewModel.CurrentFeed = viewModel.Feeds.Find(f => f.FeedId == Id);
            if (viewModel.CurrentFeedId == 0 && viewModel.Feeds.Count > 0) // if feed wasn't specified or found then set current feed to first in list
            {
                viewModel.CurrentFeed = viewModel.Feeds[0];
            }

            if (viewModel.CurrentFeedId > 0)
            {
                XmlDocument xmlDoc = FeedLoader.LoadXML(viewModel.CurrentFeed.Url);
                if (xmlDoc != null)
                {
                    RSSFeedReader feedReader = new RSSFeedReader(xmlDoc);
                    viewModel.NewsItems = feedReader.ReadItems();
                }
            }

            return viewModel;
        }
    }
}
