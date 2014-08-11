<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard/DashboardMaster.master" AutoEventWireup="true" CodeBehind="QuizResults.aspx.cs" Inherits="Fractal.Dashboard.QuizResults" %>
<%@ MasterType VirtualPath="~/Dashboard/DashboardMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DashboardHeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DashboardContentPlaceHolder" runat="server">

    <h2>ჩაბარებული ტესტები</h2>
    <div class="grid">
        <ul>
            <li class="head">
                <span>დასახელება</span>
                <span>მაქს. ქულა</span>
                <span>შენი ქულა</span>
                <span>თარიღი</span>
            </li>
            <asp:Repeater ID="QuizzesRepeater" runat="server" ValidateRequestMode="Disabled">
                <ItemTemplate>
                    <li>
                        <span class="name">
                            <span><%#Eval("Caption") %></span>
                        </span>
                        <span><%#Eval("MaxScore") %></span>
                        <span><%#Eval("StudentScore") %></span>
                        <span><%#Eval("CRTime") %></span>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="DashboardScriptsPlaceHolder" runat="server">
</asp:Content>
