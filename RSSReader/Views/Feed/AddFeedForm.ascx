<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<RSSReader.Models.Feed>" %>
<% using (Html.BeginForm("Add", "Feed"))
   {
    %>
    <label>
        Feed url:
        <%=Html.TextBox("Url")%>
    </label>
    <input type="submit" value="Add" />
    <div class="addFailure">
        There was a problem adding that feed. Please check the URL and try again.
    </div>
    <%=Html.ActionLink("cancel", "Index", null, new { @class = "cancel" })%>
    
    <%
   };
%>