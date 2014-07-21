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
                            <a href="#" class="image-tip" id='<%# Eval("AssetID").ToString().EncryptWeb() %>'>
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
                                <li><a href="#" rel="hint">SHOW hint</a> &bull; </li>
                            </asp:PlaceHolder>
                           <%-- <asp:PlaceHolder ID="ShowAnalysisPlaceHolder" runat="server" EnableViewState="false">
                                <li><a href="#" rel="analysis">SHOW Analysis</a> &bull; </li>
                            </asp:PlaceHolder>--%>
                            <%--<asp:PlaceHolder runat="server" ID="ShowCorrectAnswerPlaceHolder" EnableViewState="false">
                                <li><a href="#" rel="correct answer">SHOW Correct Answer</a> &bull; </li> 
                            </asp:PlaceHolder>--%>
                            <%--<asp:PlaceHolder ID="ShowVideoExplanationPlaceHolder" runat="server" EnableViewState="false" Visible="false">
                                <li><a<%# Eval("VideoAnswerID") == null ? string.Empty : string.Format(" href=\"{0}\"", Eval("VideoPlayerData")) %> rel="video answer">SHOW video explanation</a> &bull; </li> 
                            </asp:PlaceHolder>--%>
                            <asp:PlaceHolder ID="PointsPlaceHolder" runat="server" EnableViewState="false">
                            <li><%# String.Format("{0:0} POINTS", Eval("Score")) %> &bull; </li>                            
                            </asp:PlaceHolder>
                            <li><a href="#">ზევით</a></li> 
                        </ul>
                    </div>                    
                   <%-- <asp:PlaceHolder ID="AnalysisPlaceHolder" runat="server" EnableViewState="False">
                        <div class="analysis hidden" id="analysis_<%# Eval("ID").ToString().EncryptWeb() %>">
                            <p><b>ANALYSIS: </b><%# Eval("Analysis") %></p>
                        </div>
                    </asp:PlaceHolder>--%>
                    <asp:PlaceHolder ID="HintPlaceHolder" runat="server" EnableViewState="false">
                    <div class="hint hidden" id="hint_<%# Eval("ID").ToString().EncryptWeb() %>">
                        <p><b>მინიშნება: </b><%# Eval("Hint") %></p>
                    </div>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="StatsPlaceHolder" runat="server" EnableViewState="false" Visible="false">
                    <div class="clearfix">
                    	<div class="quizz-stat">
                        	<h5>How other students did answer?</h5>                            
                            <ul>
                                <asp:Repeater ID="StatsRepeater" runat="server" EnableViewState="false">
                                <ItemTemplate>
                            	    <li>
                                	    <b><%#Eval("Index") %></b>
                                        <span class="percent"><%#Eval("Percent") %></span>
                            		    <span class="bar color<%#Container.ItemIndex+1 %>"><span <%#string.Format("style=\"width:{0}\"",Eval("Percent")) %>></span></span>
                                    </li>
                                </ItemTemplate>
                                </asp:Repeater>                                
                            </ul>
                            
                        </div>
                    </div>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="VideoExplanationPlaceHolder" runat="server" Visible="false">
                        <div class="video-answer hidden">
                            <div class="player-placeholder"></div>
                            <input type="hidden" value="<%#Eval("VideoAnswerTime") %>" />
                        </div>
                    </asp:PlaceHolder>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
    <asp:PlaceHolder runat="server" ID="FinishQuizPlaceHolder" EnableViewState="False" Visible="False">
        <div>
        <a href='#' class='submit quiz'>Finish Quiz</a>
        </div>
    </asp:PlaceHolder>

</asp:PlaceHolder>
</div>