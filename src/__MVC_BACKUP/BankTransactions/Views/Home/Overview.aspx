<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IOrderedEnumerable<IGrouping<string, nl.jorncruijsen.jbs.transactions.BankRecord>>>" %>

<%@ Import Namespace="nl.jorncruijsen.jbs.transactions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Overview
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Overview</h2>
    <% foreach (IGrouping<string, BankRecord> group in Model)
       {
    %>
           <h4><%: group.Key %></h4>
           <p>Total: <%: group.Sum(r => r.Type.Equals(BankRecord.TRANSACTION_TYPE.DEBIT) ? r.Amount * -1 : r.Amount) %></p>
    <table class="data">
        <tr><th>Datum</th><th>Hoeveelheid</th><th>Tegenrekening</th><th>Beschrijving</th></tr>

        <% foreach (BankRecord record in group)
           {
           string type = record.Type.Equals(BankRecord.TRANSACTION_TYPE.CREDIT) ? "gain" : "gone"; %>
        <tr class="<%: type %>">
            <td><%: record.ExecutionDate.ToString("dd-MM-yyyy") %></td>
            <td><%: string.Format("{0:C}", record.Amount) %></td>
            <td>
                <a href="Details?name=<%: record.Name %>">
                    <%: record.Name%></a>
            </td>
            <td>
                <%: record.Description1%>
            </td>
        </tr>
        <% } %>
    </table>
    <% } %>
</asp:Content>
