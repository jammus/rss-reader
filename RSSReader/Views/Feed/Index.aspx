<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<RSSReader.ViewModels.FeedReaderViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	My Feeds : RSSReader
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="span-24 reader">
            <div class="span-5" id="feedListPanel">
                <% Html.RenderPartial("FeedList"); %>
            </div>
            <div class="span-19 last">
            <div class="span-19 last" id="channelPanel">
                
                <%
                    if (Model.Feeds.Count == 0)
                    { 
                    %>
                    <% Html.RenderPartial("BlankState"); %>
                    <%
                    }
                    else
                    {


                        Html.RenderPartial("ChannelPanel", Model.NewsItems);

                    }
                %>
                
            </div>
                <div class="span-19 last" id="displayPanel">
                    <div class="displayFrameContainer">
                        <iframe src="">
                        
                        </iframe>
                    </div>
                </div>
            </div>
            <div class="modalContainer" id="addFeedModal">
                <% Html.RenderPartial("AddFeedForm", Model.NewFeed); %>
            </div>
            <div class="modalContainer" id="deleteFeedModal">
                <% Html.RenderPartial("DeleteFeedForm", Model.NewFeed); %>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="/Scripts/Feed/FeedViewer.js"></script>
</asp:Content>
