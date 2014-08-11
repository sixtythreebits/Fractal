<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard/DashboardMaster.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Fractal.Dashboard.Default" %>
<%@ MasterType VirtualPath="~/Dashboard/DashboardMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DashboardHeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DashboardContentPlaceHolder" runat="server">
    <h1>ჩემი პროფილი</h1>
    <h3>წიგნები</h3>
    <div class="grid col2">
        <ul>
            <li class="head">
                <span>დასახელება</span>
                <span>აქტიურია</span>
            </li>
            <asp:Repeater ID="CoursesRepeater" runat="server" ViewStateMode="Disabled">
                <ItemTemplate>
                    <li>
                        <span class="name">
                            <span>
                                <a href="/book/<%#Eval("Slug") %>/"><%#Eval("Caption") %></a>
                            </span>
                        </span>
                        <span><%#Eval("CRTime") %> - მდე</span>
                    </li>
                </ItemTemplate>
            </asp:Repeater>            
        </ul>
    </div>

    <h3>ტესტები</h3>
    <div class="grid">
        <ul>
            <li class="head">
                <span>დასახელება</span>
                <span>მაქს. ქულა</span>
                <span>წიგნი</span>
                <span>თარიღი</span>
            </li>
            <asp:Repeater ID="QuizzesRepeater" runat="server" ValidateRequestMode="Disabled">
                <ItemTemplate>
                    <li>
                        <span class="name">
                            <span>
                                <a href="/book/<%#Eval("Slug") %>/quiz/<%#Eval("ID") %>/"><%#Eval("Caption")%></a>
                            </span>
                        </span>
                        <span>13</span>
                        <span><a href="/book/<%#Eval("Slug") %>/"><%#Eval("CourseCaption")%></a></span>
                        <span><%#Eval("ExpDate") %></span>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="DashboardScriptsPlaceHolder" runat="server">
</asp:Content>
