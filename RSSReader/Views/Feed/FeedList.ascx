<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<RSSReader.ViewModels.FeedReaderViewModel>" %>
<div class="feeds">
    <% if (Model.Feeds.Count > 0)
       { 
    %>
    <ul>
        <% 
           string className;
           foreach (var feed in Model.Feeds)
           {
               className = "feed";
               if (feed.FeedId == Model.CurrentFeedId)
               {
                   className += " current";
               }
               %>
               <li><%=Html.ActionLink(feed.Name, "View", new { Id = feed.FeedId }, new { @class = className })%></li>
               <%
           }
        %>
    </ul>
    <% 
        }
    %>
</div>
<div class="controlPanel">
    <% 
    using (Html.BeginForm("Add", "Feed", FormMethod.Get,new { id = "addFeed" }))
    {
        %>
        <input type="submit" value="Subscribe" />
        <%
    };
    using (Html.BeginForm("Delete", "Feed", new {Id = Model.CurrentFeedId}, FormMethod.Get, new {id = "deleteFeed"}))
    {
        if (Model.CurrentFeedId > 0)
        {
           %>
           <input type="submit" value="Delete" />
           <%
        }
        else
        {
            %>
            <input type="submit" value="Delete" disabled="disabled" />
            <%
        }
    };
    %>
</div>