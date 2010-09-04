<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    RSSReader
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="splashcontainer">
        <div class="container">
            <div class="span-24 splash">
            <%
                if (!Request.IsAuthenticated)
                {
                    %>
                    <%=Html.ActionLink("Register now", "Register", "Account")%> or <%=Html.ActionLink("log in", "LogOn", "Account")%> to get started.
                    <%
                }
                else
                {
                    %>
                    Welcome back, <%=HttpContext.Current.User.Identity.Name%>. You can <%=Html.ActionLink("view your feeds", "Index", "Feed") %>.
                    <%
                }
                %>
            </div>
        </div>
    </div>
    <div class="container featuresetc">
        <div class="span-8">
            <h3>About</h3>
            RSSReader lets you keep up with your favourite websites and news feeds from inside your browser and access them anytime you're on the web.
            RSSReader was written over a weekend as part of an coding test. It is built on the ASP.NET MVC Framework using C#, jQuery and MSSQL.
        </div>
        <div class="span-8">
            <h3>Features</h3>
            RSSReader has a host of useful features. Including:
            <ul>
                <li>Subscribe to feeds</li>
                <li>Unsubscribe from feeds</li>
                <li>Read feeds</li>
                <li>That's it</li>
            </ul>
        </div>
        <div class="span-8 last">
            <h3>Pricing</h3>
            RSSReader is 100% free. Obviously we'll plaster annoying Flash ads all over it once we get the traffic, but for now you're safe.
        </div>
    </div>
</asp:Content>
