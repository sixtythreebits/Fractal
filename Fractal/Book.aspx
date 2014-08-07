<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Book.aspx.cs" Inherits="Fractal.Book" %>
<%@ MasterType VirtualPath="~/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="book">
        <div>
            <article>
                <h1><asp:Literal ID="CourseCaptionLiteral" runat="server" ViewStateMode="Disabled"></asp:Literal></h1>
                <p><asp:Literal ID="DescriptionLiteral" runat="server" ViewStateMode="Disabled"></asp:Literal></p>
            </article>
            <asp:Image ID="BookImage" runat="server" ViewStateMode="Disabled" ClientIDMode="Static" AlternateText="წიგნის დასახელება" />
        </div>
    </section>
    <section class="gray">
        <h2 class="title">
            <span><asp:Literal ID="SectionLiteral" runat="server" ViewStateMode="Disabled"></asp:Literal></span>
        </h2>

        <asp:PlaceHolder ID="SubscribePlaceHolder" runat="server" ViewStateMode="Disabled">
        <div class="bar-code">
            <div class="bar-code-form">
                <label>შეიყვანეთ კოდი იმისთვის, რომ მიიღოთ წვდომა გამოცდებთან</label>
                <asp:TextBox ID="KeyTextBox" runat="server"></asp:TextBox>                
                <asp:Button ID="ActivateButton" runat="server" CssClass="btn big" Text="გაგზავნა" OnClick="ActivateButton_Click" />                
                <asp:PlaceHolder ID="ErrorPlaceHolder" runat="server" Visible="false">
                <label class="error"><asp:Literal ID="ErrorMessageLiteral" runat="server"></asp:Literal></label>
                </asp:PlaceHolder>
            </div>

            <div class="help">
                <h3>
                    <span>სად ვიპოვო კოდი?</span>
                </h3>

                <div class="cl">
                    <span class="img">
                        <img src="/images/0/bar-code.jpg" alt="" />
                    </span>
                    <p>
                        წიგნის ყდის უკანა მხარეს მდებარეობს კოდი XX-XXX-XXX- ტიპის. იმისათვის, რომ მიიღოთ წვდომა გამოცდებთან, შეიყვანეთ ის
                    </p>
                </div>

            </div>

        </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="QuizzesPlaceHolder" runat="server" ViewStateMode="Disabled">
        <div class="grid course">
            <ul>
                <li class="head">
                    <span>დასახელება</span>
                    <span>მაქს. ქულა</span>
                    <span>შენი ქულა</span>
                    <span>ჩაბარების თარიღი</span>
                </li>
                <asp:Repeater ID="QuizzesRepeater" runat="server">
                    <ItemTemplate>
                        <li>
                            <span class="name">
                                <span>
                                    <a href="/quiz/<%#Eval("ID") %>"><%#Eval("Caption") %></a>
                                </span>
                            </span>
                            <span><%#Eval("MaxScore") %></span>
                            <span><%#Eval("StudentScore") %></span>
                            <span><%#Eval("Date") %></span>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>                
            </ul>
        </div>
        </asp:PlaceHolder>
    </section>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsPlaceHolder" runat="server">
</asp:Content>
