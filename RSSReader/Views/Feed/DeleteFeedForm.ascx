<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<RSSReader.Models.Feed>" %>
Are you sure you want to delete the feed '<span class="feedName"><%=Html.Encode(Model.Name) %></span>'?
<%
    using (Html.BeginForm())
    {
        %>
        <input type="submit" value="OK" name="deleteButton" />
        <%=Html.ActionLink("cancel", "Index", null, new { @class = "cancel" })%>
        <%
    }
%>