<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<nl.jorncruijsen.jbs.transactions.BankRecord>>" %>
<%@ Import Namespace="nl.jorncruijsen.jbs.transactions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Transaction overview</h2>
    <table class="data">
        <tr><th>Datum</th><th>Hoeveelheid</th><th>Tegenrekening</th><th>Beschrijving</th></tr>
        <% foreach (BankRecord record in Model)
           {
               string type = record.Type.Equals(BankRecord.TRANSACTION_TYPE.CREDIT) ? "gain" : "gone";
               %>
            <tr class="<%: type %>">
                <td><%: record.ExecutionDate.ToString("dd-MM-yyyy")%></td>
                <td><%: string.Format("{0:C}", record.Amount) %></td>
                <td><a href="Home/Details?name=<%: record.Name %>"><%: record.Name%></a></td>
                <td><%: record.Description1%></td>
            </tr>
        <% } %>
    </table>
</asp:Content>
