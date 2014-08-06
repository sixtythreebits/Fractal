<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Book.aspx.cs" Inherits="Fractal.Book" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="book">
        <div>
            <article>
                <h1>წიგნის დასახელება</h1>
                <p>
                    მსხდარ გადავასხი უკუგდება დარბაზია წაეტანა უჯიშო შვილო დაიწრიტება, გერ იგონებდა, ეცნო გავცეცხლდი დამწუხრებული. შესწვდებოდა იალტა ვგავდი ნაპრალიდან, ხიდ, საქნელია, ავაწყვე ტოვებდა მოიხვია. უკუგდება ყოველწლიურად გაბევრდება დამღუპველი დაიწრიტება კრივის გძელაძიანთ ღილი წამოისხამდა.
                </p>
            </article>

            <img src="images/0/book.png" alt="წიგნის დასახელება" />
        </div>
    </section>

    <section class="gray">
        <h2 class="title">
            <span>გამოცდები</span>
        </h2>


        <div class="bar-code">
            <div class="bar-code-form">
                <label>შეიყვანეთ კოდი იმისთვის, რომ მიიღოთ წვდომა გამოცდებთან</label>
                <input type="text" />
                <input type="submit" class="btn big" value="გაგზავნა" />
            </div>

            <div class="help">
                <h3>
                    <span>სად ვიპოვო კოდი?</span>
                </h3>

                <div class="cl">
                    <span class="img">
                        <img src="images/0/bar-code.jpg" alt="" />
                    </span>
                    <p>
                        წიგნის ყდის უკანა მხარეს მდებარეობს კოდი XX-XXX-XXX- ტიპის. იმისათვის, რომ მიიღოთ წვდომა გამოცდებთან, შეიყვანეთ ის
                    </p>
                </div>

            </div>

        </div>

        <div class="grid course">
            <ul>
                <li class="head">
                    <span>დასახელება</span>
                    <span>მაქს. ქულა</span>
                    <span>შენი ქულა</span>
                    <span>თარიღი</span>
                </li>
                <li>
                    <span class="name">
                        <span>
                            <a href="#">გამოცდის დასახელება</a>
                        </span>
                    </span>
                    <span>13</span>
                    <span>13</span>
                    <span>21 მარტი, 2014</span>
                </li>
                <li>
                    <span class="name">
                        <span>
                            <a href="#">გამოცდის დასახელება</a>
                        </span>
                    </span>
                    <span>13</span>
                    <span>13</span>
                    <span>21 მარტი, 2014</span>
                </li>
                <li>
                    <span class="name">
                        <span>
                            <a href="#">გამოცდის დასახელება გამოცდის დასახელება გამოცდის დასახელება გამოცდის</a>
                        </span>
                    </span>
                    <span>13</span>
                    <span>13</span>
                    <span>21 მარტი, 2014</span>
                </li>
                <li>
                    <span class="name">
                        <span>
                            <a href="#">გამოცდის დასახელება გამოცდის დასახელება დასახელება</a>
                        </span>
                    </span>
                    <span>13</span>
                    <span>13</span>
                    <span>21 მარტი, 2014</span>
                </li>
            </ul>
        </div>

    </section>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsPlaceHolder" runat="server">
</asp:Content>
