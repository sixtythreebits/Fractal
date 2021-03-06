﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Fractal.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="plugins/jquery.bxslider/jquery.bxslider.css" rel="stylesheet" />
    <script src="plugins/jquery.bxslider/jquery.bxslider.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="home-top">
        <h2 class="title">
            წიგნები
            <span></span>
        </h2>
        <ul>
            <li>
                <img src="<%=AlgebraImage %>" alt="ალგებრა" width="600" />
                <a href="/book/algebra/">ალგებრა</a>
            </li>
            <li>
                <img src="<%=GeometryImage %>" alt="გეომეტრია" width="600" />
                <a href="/book/geometry/">გეომეტრია</a>
            </li>
        </ul>
    </div>

    <ul class="banners cl">
        <li>
            <a href="http://zoommer.ge" target="_blank"><img src="/images/banners/01.png" alt="" /></a>
        </li>
        <li>
            <a href="http://zoommer.ge" target="_blank"><img src="/images/banners/02.png" alt="" /></a>
        </li>
    </ul>

    <%--<section class="home gray hidden">
        <h2 class="title">
            <span>მთავარი უპირატესობები</span>
        </h2>
        <ul class="list col3">
            <li>
                <span><img src="images/icons/56/01.png" alt="" /></span>
                <article>
                    <h3>ლორემ იპსუმ დოლორ სიტ ამეტ</h3>
                    <p>
                        მსხდარ გადავასხი უკუგდება დარბაზია წაეტანა უჯიშო შვილო დაიწრიტება, გერ იგონებდა, ეცნო გავცეცხლდი დამწუხრებული. შესწვდებოდა იალტა ვგავდი ნაპრალიდან, ხიდ, საქნელია, ავაწყვე ტოვებდა მოიხვია. უკუგდება ყოველწლიურად გაბევრდება დამღუპველი დაიწრიტება კრივის გძელაძიანთ ღილი წამოისხამდა.
                    </p>
                </article>
                <a href="#" class="btn">დარეგისტრირდი</a>
            </li>
            <li>
                <span><img src="images/icons/56/02.png" alt="" /></span>
                <article>
                    <h3>ლორემ იპსუმ დოლორ სიტ ამეტ</h3>
                    <p>
                        მსხდარ გადავასხი უკუგდება დარბაზია წაეტანა უჯიშო შვილო დაიწრიტება, გერ იგონებდა, ეცნო გავცეცხლდი დამწუხრებული. შესწვდებოდა იალტა ვგავდი ნაპრალიდან, ხიდ, საქნელია, ავაწყვე ტოვებდა მოიხვია. უკუგდება ყოველწლიურად გაბევრდება დამღუპველი დაიწრიტება კრივის გძელაძიანთ ღილი წამოისხამდა. უკუგდება ყოველწლიურად გაბევრდება დამღუპველი დაიწრიტება კრივის გძელაძიანთ ღილი წამოისხამდა.
                    </p>
                </article>
                <a href="#" class="btn">დარეგისტრირდი</a>
            </li>
            <li>
                <span><img src="images/icons/56/03.png" alt="" /></span>
                <article>
                    <h3>ლორემ იპსუმ დოლორ სიტ ამეტ</h3>
                    <p>
                        მსხდარ გადავასხი უკუგდება დარბაზია წაეტანა უჯიშო შვილო დაიწრიტება, გერ იგონებდა, 
                    </p>
                </article>
                <a href="#" class="btn">დარეგისტრირდი</a>
            </li>
        </ul>
    </section>--%>


     <section class="home gray">
        <h2 class="title">
            <span>რას გთავაზობთ</span>
        </h2>

        <div class="wrap col2">
            <div>
                <ul>
                    <li>
                        <img src="images/icons/01.png" alt="" />
                        <h4>ტესტები</h4>
                        <p>ვებ–გვერდზე დარეგისტრირების შემდეგ თქვენ გექნებათ წვდომა დამატებით მასალაზე, რომელიც სახელმძღვანელოში არ არის შესული. თქვენს პროფაილში შეგროვდება თქვენს მიერ გავლილ ტესტებში მიღებული ქულები. ტესტების ვებ გვერდზე განთავსება მოხდება, გარკვეული პერიოდულობით, გავლილი მასალის შესაბამისად.</p>
                    </li>
                </ul>
            </div>
            <div>
                <ul>
                    <li>
                        <img src="images/icons/05.png" alt="" />
                        <h4>ბონუსი</h4>
                        <p>ორივე სახელმძღვანელოში არსებული მასალის გავლის შემდეგ, სასწავლო წლის ბოლოს, თქვენ გექნებათ წვდომა: <br /><br />
                        1) ეროვნული გამოცდების შესაბამისი თემატიკის და &nbsp;&nbsp;&nbsp;&nbsp;სირთულის ბილეთებზე;<br /><br />
                        2) ტესტებზე, რომლებიც დაგეხმარებათ საატესტატო &nbsp;&nbsp;&nbsp;&nbsp;გამოცდებისათვის მომზადებისას.<br />
                        </p>
                    </li>
                </ul>
            </div>
        </div>

    </section>


   <%-- <section class="home gray">
        <h2 class="title">
            <span>რას გთავაზობთ</span>
        </h2>

        <div class="wrap col2">
            <div>
                <ul>
                    <li>
                        <img src="images/icons/01.png" alt="" />
                        <h4>ტესტები</h4>
                        <p>
                            ვებ–გვერდზე დარეგისტრირების შემდეგ თქვენ გექნებათ წვდომა დამატებით მასალაზე, რომელის სახელმძღვანელოში არ არის შესული. თქვენს პროფაილში შეგროვდება თქვენს მიერ გავლილი ტესტებში მიღებული ქულები. ტესტები ვებ გვერდზე განთავსდება განვლილი მასალის შესაბამისად და თითოეული ტესტისათვის განსაზღვრული იქნება გარკვეული დრო.
                        </p>
                    </li>
                    <li>
                        <img src="images/icons/02.png" alt="" />
                        <h4>ბონუსი</h4>
                        <p>
                            ორივე სახელმძღვანელოში არსებული მასალის გავლის შემდეგ თქვენ გექნებათ წვდომა ბილეთებზე, რომლების მოიცავს მთელ მასალას. განთავსებული ბილეთები იქნება ეროვნული გამოცდების შესაბამისი სირთულის და თემატიკის.
                        </p>
                    </li>
                    <li>
                        <img src="images/icons/03.png" alt="" />
                        <h4>ლორემ იპსუმ</h4>
                        <p>
                            მსხდარ გადავასხი უკუგდება დარბაზია წაეტანა უჯიშო შვი ლო დაიწრიტება, გერ იგონებდა, ეცნო გავცეცხლდი დამწუ ხრებუ. შესწვდებოდა იალტა ვგავდი.
                        </p>
                    </li>
                </ul>
            </div>
            <div>
                <ul>
                    <li>
                        <img src="images/icons/04.png" alt="" />
                        <h4>ლორემ იპსუმ</h4>
                        <p>
                            მსხდარ გადავასხი უკუგდება დარბაზია წაეტანა უჯიშო შვი ლო დაიწრიტება, გერ იგონებდა, ეცნო გავცეცხლდი დამწუ ხრებუ. შესწვდებოდა იალტა ვგავდი.
                        </p>
                    </li>
                    <li>
                        <img src="images/icons/05.png" alt="" />
                        <h4>ლორემ იპსუმ</h4>
                        <p>
                            მსხდარ გადავასხი უკუგდება დარბაზია წაეტანა უჯიშო შვი ლო დაიწრიტება, გერ იგონებდა, ეცნო გავცეცხლდი დამწუ ხრებუ. შესწვდებოდა იალტა ვგავდი.
                        </p>
                    </li>
                    <li>
                        <img src="images/icons/06.png" alt="" />
                        <h4>ლორემ იპსუმ</h4>
                        <p>
                            მსხდარ გადავასხი უკუგდება დარბაზია წაეტანა უჯიშო შვი ლო დაიწრიტება, გერ იგონებდა, ეცნო გავცეცხლდი დამწუ ხრებუ. შესწვდებოდა იალტა ვგავდი.
                        </p>
                    </li>
                </ul>
            </div>
        </div>

    </section>--%>

    <%--<section class="home gray hidden">
        <h2 class="title">
            <span>ჩვენი გუნდი</span>
        </h2>
        <div class="wrap team">
            <ul class="list">
                <li>
                    <span><img src="images/0/team.jpg" alt="" /></span>
                    <h5>სახელი გვარი</h5>
                    <p>მსხდარ გადავასხი უკუგდება დარბაზია წაეტანა უჯიშო შვი ლო დაიწრიტება, გერ იგონებდა, ეცნო გავცეცხლდი დამწუ ხრებუ. შესწვდებოდა იალტა ვგავდი.</p>
                </li>
                <li>
                    <span><img src="images/0/team.jpg" alt="" /></span>
                    <h5>სახელი გვარი</h5>
                    <p>მსხდარ გადავასხი უკუგდება დარბაზია წაეტანა უჯიშო შვი ლო დაიწრიტება, გერ იგონებდა, ეცნო გავცეცხლდი დამწუ ხრებუ. შესწვდებოდა იალტა ვგავდი.</p>
                </li>
                <li>
                    <span><img src="images/0/team.jpg" alt="" /></span>
                    <h5>სახელი გვარი</h5>
                    <p>მსხდარ გადავასხი უკუგდება დარბაზია წაეტანა უჯიშო შვი ლო დაიწრიტება, გერ იგონებდა, ეცნო გავცეცხლდი დამწუ ხრებუ. შესწვდებოდა იალტა ვგავდი.</p>
                </li>
                <li>
                    <span><img src="images/0/team.jpg" alt="" /></span>
                    <h5>სახელი გვარი</h5>
                    <p>მსხდარ გადავასხი უკუგდება დარბაზია წაეტანა უჯიშო შვი ლო დაიწრიტება, გერ იგონებდა, ეცნო გავცეცხლდი დამწუ ხრებუ. შესწვდებოდა იალტა ვგავდი.</p>
                </li>
                <li>
                    <span><img src="images/0/team.jpg" alt="" /></span>
                    <h5>სახელი გვარი</h5>
                    <p>მსხდარ გადავასხი უკუგდება დარბაზია წაეტანა უჯიშო შვი ლო დაიწრიტება, გერ იგონებდა, ეცნო გავცეცხლდი დამწუ ხრებუ. შესწვდებოდა იალტა ვგავდი.</p>
                </li>
            </ul>
        </div>
    </section>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsPlaceHolder" runat="server">
<script>
    $("header nav li").eq(0).addClass("active");
</script>
</asp:Content>