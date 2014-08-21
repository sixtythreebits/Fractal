<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard/DashboardMaster.master" AutoEventWireup="true" CodeBehind="BonusQuizzes.aspx.cs" Inherits="Fractal.Dashboard.BonusQuizzes" %>
<%@ MasterType VirtualPath="~/Dashboard/DashboardMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DashboardHeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DashboardContentPlaceHolder" runat="server">
<h2>ბონუს ტესტები</h2>
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
                        <asp:PlaceHolder ID="CaptionTextPlaceHolder" runat="server" ViewStateMode="Disabled" Visible='<%# ((int?)Eval("StudentScore"))>0 %>'>
                        <span><%#Eval("Caption") %></span>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder ID="CaptionLinkPlaceHolder" runat="server" ViewStateMode="Disabled" Visible='<%# ((int?)Eval("StudentScore")) == 0 %>'>
                            <span>
                            <a href="/quiz/<%#Eval("ID") %>/"><%#Eval("Caption")%></a>
                            </span>                        
                        </asp:PlaceHolder>
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
