<%@ Page Title="ჩვენს შესახებ | Fractal" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="about.aspx.cs" Inherits="Fractal.about" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<section class="cnt">
        <article>
            <h1>ჩვენს შესახებ</h1>
            <p>პირველად ჩვენი სახელმძღვანელო გამოიცა 2005 წელს. მასში ყოველწლიურად ხდებოდა სიახლეების შეტანა. სახელმძღვანელო განკუთვნილია როგორც აბიტურიენტებისათვის, ასევე დამამთავრებელი კლასის მოსწავლეებისათვის. 2014 წელს გადავწყვიტეთ შეგვექმნა ჩვენი ვებ–გვერდი, რომელიც სახელმზღვანელოს მფლობელებს საშუალებას მისცემს დამატებით გაიაროს ტესტირება გავლილი მასალის შესაბამისად.</p>
        </article>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsPlaceHolder" runat="server">
<script>
    $("header nav li").eq(1).addClass("active");
</script>
</asp:Content>
