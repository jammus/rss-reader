<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<RSSReader.Models.NewsItem>>" %>
<div class="channelPanelContainer">
    <div class="channelPanelBody">
        <%
        foreach(var newsItem in Model)
        {
            %>
            <div class="newsItem">
                <div class="span-12">
                <a href="<%=Html.Encode(newsItem.Url)%>" class="newsItem"><%=Html.Encode(newsItem.Headline) %></a>
                </div>
                <div class="span-6 last date">
                    <%=Html.Encode(newsItem.DatePublished.ToShortDateString())%>
                    <%=Html.Encode(newsItem.DatePublished.ToShortTimeString())%>
                </div>
            </div>
            <%
        }
        %>
    </div>
</div>