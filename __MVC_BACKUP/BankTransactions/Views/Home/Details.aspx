<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<nl.jorncruijsen.jbs.transactions.BankRecord>>" %>
<%@ Import Namespace="nl.jorncruijsen.jbs.transactions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Details</h2>

    <h4>Summary</h4>
    <p>Total amount: EUR. <%: Model.Sum(r => r.Amount) %></p>

    <h4>All transactions</h4>
    <table class="data">
        <tr><th>Datum</th><th>Hoeveelheid</th><th>Tegenrekening</th><th>Beschrijving</th></tr>
        <% for (int i = 0; i < Model.Count(); i++)
           {
               BankRecord record = Model.ElementAt(i); 
               string type = record.Type.Equals(BankRecord.TRANSACTION_TYPE.CREDIT) ? "gain" : "gone";
               %>
            <tr class="<%: type %>">
                <td><%: record.ExecutionDate.ToString("dd-MM-yyyy") %></td>
                <td><%: string.Format("{0:C}", record.Amount) %></td>
                <td><a href="Home/Details?name=<%: record.Name %>"><%: record.Name%></a></td>
                <td><%: record.Description1%></td>
            </tr>
        <% } %>
    </table>

</asp:Content>

