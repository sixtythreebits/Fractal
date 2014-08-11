<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_QuizUserControl" Codebehind="QuizUserControl.ascx.cs" %>
<%@ Import Namespace="Lib" %>
<%@ Import Namespace="Core.Utilities" %>

<div class="quiz-container">
<asp:PlaceHolder ID="QuizTitlePlaceHolder" runat="server" EnableViewState="false">
<h1 class="title">
    <asp:Literal ID="QuizCaptionLiteral" runat="server" EnableViewState="false"></asp:Literal>
</h1>
</asp:PlaceHolder>

<asp:PlaceHolder ID="QuizPlaceHolder" runat="server" Visible="false" EnableViewState="false">
    <asp:HiddenField runat="server" ID="HFData" ClientIDMode="Static" />    
    <ul class="questions clearfix">
        <asp:Repeater ID="QuestionsRepeater" runat="server" OnItemDataBound="QuestionsRepeater_ItemDataBound" EnableViewState="false">
            <ItemTemplate>
                <li>
                    <h4><%#Container.ItemIndex + 1 %></h4>
                    <p>
                        <%# Eval("question") %>
                        <asp:PlaceHolder runat="server" ID="QuestionAssetPlaceHolder" Visible="False" EnableViewState="False">
                            <a class="image-tip">
                                <img class="asset" src="<%#string.Format("/preview/{0}/",new PreviewImageData{AssetID=(long)Eval("AssetID"), Width=700, FitMode = "max"}.ToEncripted()) %>" />
                            </a>
                        </asp:PlaceHolder>
                    </p>
                    <ul id="ul_<%# Eval("ID").ToString().EncryptWeb() %>">
                        <asp:Repeater ID="AnswersRepeater" runat="server" EnableViewState="false" OnItemDataBound="AnswersRepeater_ItemDataBound">
                            <ItemTemplate>
                                <li id='li_<%# Eval("ID").ToString().EncryptWeb() %>' class="<%# GetAnswerClass((bool)Eval("IsCorrect"), (bool)Eval("IsUserAnswer")) %>">                                    
                                    <asp:Literal ID="CheckBoxLiteral" runat="server" EnableViewState="false"></asp:Literal>
                                    <h4><%# Utility.IndexToChar(Container.ItemIndex + 1) %></h4>
                                    <p><%#Eval("answer")%></p>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                    <div class="controll-panel" id='<%# Eval("ID").ToString().EncryptWeb() %>'>
                        <ul>
                            <asp:PlaceHolder ID="ShowHintPlaceHolder" runat="server" EnableViewState="false">
                                <li><a href="#" class="question_hint">მაჩვენე მინიშნება</a> &bull; </li>
                            </asp:PlaceHolder>                           
                            <asp:PlaceHolder ID="PointsPlaceHolder" runat="server" EnableViewState="false">
                            <li><%# String.Format("{0:0} ქულა", Eval("Score")) %> </li>                            
                            </asp:PlaceHolder>                            
                        </ul>
                    </div>                                       
                    <asp:PlaceHolder ID="HintPlaceHolder" runat="server" EnableViewState="false">
                    <div class="hint hidden" id="hint_<%# Eval("ID").ToString().EncryptWeb() %>">
                        <p><b>მინიშნება: </b><%# Eval("Hint") %></p>
                    </div>
                    </asp:PlaceHolder>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
    <asp:PlaceHolder runat="server" ID="FinishQuizPlaceHolder" EnableViewState="False" Visible="False">
        <a id="SubmitQuiz" class="btn" href="#">გამოცდის დასრულება</a>
    </asp:PlaceHolder>

</asp:PlaceHolder>
</div>