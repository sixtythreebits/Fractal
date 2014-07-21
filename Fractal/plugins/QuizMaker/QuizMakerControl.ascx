<%@ Control Language="C#" AutoEventWireup="true" Inherits="plugins_QuizMaker_QuizMakerControl" Codebehind="QuizMakerControl.ascx.cs" %>
<%@ Import Namespace="Lib" %>
<%@ Import Namespace="Core.Properties" %>
<asp:PlaceHolder ID="CaptionPlaceHolder" runat="server" EnableViewState="false">
<h1 class="quiz-title">Quiz: <span><asp:Literal ID="QuizCaptionLiteral" runat="server"></asp:Literal></span></h1>
</asp:PlaceHolder>
<div class="static">
    <div class="main-title big">
        <span class="left"></span>
        <div class="drag-nav">
            <a href="#question-edit" class="qz-question" title="გადაათრიეთ მარცხენა არეალში"><span>შეკითხვა</span></a>
            <%--<a href="#question-from-bank" class="qz-bank" title="Drag to the left area"><span>შეკითხვა <br />ბაზიდან</span></a>--%>
            <p>გადმოიტანეთ ღილაკი ქმედების შესასრულებლად.</p>
        </div>
        <%--<a href="#" class="icon text upload">Upload</a>--%>
        <div class="btn-group qz">
        <table>
            <tr>
                <td><a id="ShowPreviewButton" href="#">ნახვა</a></td>
                <td><a href="#quizz-properties" class="show_quizz_properties">მაჩვენე ტესტის თვისებები</a></td>
            </tr>
        </table>
        </div>
    </div>					
</div>
                
<div class="quiz-content">
    <table>
        <tr>
            <td class="question-td">
                <asp:Repeater ID="QuestionsRepeater" runat="server" EnableViewState="false" OnItemDataBound="QuestionsRepeater_ItemDataBound">
                    <ItemTemplate>
                        <div class="question-box <%#Eval("ID").ToString().EncryptWeb() %>">
                            <div><em class="arrow"></em>
                                <span class="drag"></span>
                                <span class="index"><%#Eval("Number") %></span>
                                <span class="expand"></span>
                                <span class="question-text"><span><a><%#Eval("question") %></a></span></span>
                                <em class="asset">ფაილი: <b><%#Eval("AssetCaption") %></b></em>
                                <a class="addAnswer">პასუხი</a>
                            </div>
                            <ul class="answer-box">
                                <asp:Repeater ID="AnswersRepeater" runat="server" EnableViewState="false">
                                    <ItemTemplate>
                                        <li id="<%#Eval("ID").ToString().EncryptWeb() %>">
                                            <span class="alphabet"><%# (char)(64+(short)Eval("Number")) %></span>
                                            <span><input type="checkbox" <%# ((bool)Eval("IsCorrect"))?"checked=\"checked\"":string.Empty %> /></span>
                                            <label class="answer-text"><span><a><%#Eval("answer") %></a></span></label>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>                               
                            </ul>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>                
            </td>
            <td class="properties-td">                            
                <!-- Quizz Properties  -->
                <div class="quizz-properties">
                    <h3>ტესტის თვისებები</h3>
                    <div>
                        <a id="SaveButton" href="#" class="btn save">შენახვა</a>
                        <%--<a href="#" class="btn red delete" runat="server" visible="false">Delete</a>--%>
                    </div>
                    <div class="form caption">
                        <label>ტესტის დასახელება</label>
                        <asp:TextBox ID="QuizCaptionTextBox" runat="server" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <asp:PlaceHolder ID="IsPublishedControlPlaceHolder" runat="server" EnableViewState="false" Visible="false">
                    <div class="is_published">
                        <label>გამოქვეყნებული</label>
                        <div class="toggler">
                            <div class="pad"></div>
                        </div>
                        <asp:HiddenField ID="HFIsPublished" runat="server" ClientIDMode="Static" EnableViewState="false" />
                    </div>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="CourseQuizPropertiesPlaceHolder" runat="server" Visible="false">                        
                    <div class="form start-date">
                        <label>საწყისი თარიღი</label>                            
                        <asp:TextBox ID="CourseQuizStartDate" runat="server" ClientIDMode="Static"></asp:TextBox>                            
                    </div>
                    <div class="form end-date">
                        <label>დასრულების თარიღი</label>
                        <asp:TextBox ID="CourseQuizEndDate" runat="server" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <div class="form grade-date hidden">
                        <label>Grade Date</label>
                        <asp:TextBox ID="CourseQuizGradeDate" runat="server" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    </asp:PlaceHolder>            
                    <asp:PlaceHolder ID="AssetQuizPropertiesPlaceHolder" runat="server" EnableViewState="false" Visible="false">
                        <div class="time">
	                        <label>Time</label>		
                            <asp:TextBox ID="TimeTextBox" runat="server" MaxLength="30" ClientIDMode="Static"></asp:TextBox>	                        
                        </div>                        
                        <div class="aq-practice">
	                        <label>Practice?</label>
	                        <div class="toggler">
		                        <div class="pad">
		                        </div>
	                        </div>	                        
                            <asp:HiddenField ID="HFPractice" runat="server" ClientIDMode="Static" />
                        </div>
                        <div class="aq-hints">
	                        <label>Hints?</label>
	                        <div class="toggler">
		                        <div class="pad">
		                        </div>
	                        </div>	                        
                            <asp:HiddenField ID="HFHints" runat="server" ClientIDMode="Static" />
                        </div>
                        <div class="aq-analysis">
	                        <label>Analysis?</label>
	                        <div class="toggler">
		                        <div class="pad">
		                        </div>
	                        </div>	                        
                            <asp:HiddenField ID="HFAnalysis" runat="server" ClientIDMode="Static" />
                        </div>
                        <div class="aq-other hidden">
	                        <label>Student answers?</label>
	                        <div class="toggler">
		                        <div class="pad">
		                        </div>
	                        </div>	                        
                            <asp:HiddenField ID="HFOther" runat="server" ClientIDMode="Static" />
                        </div>
                        <div class="aq-skip">
	                        <label>Allow skip?</label>
	                        <div class="toggler">
		                        <div class="pad">
		                        </div>
	                        </div>	                        
                            <asp:HiddenField ID="HFSkip" runat="server" ClientIDMode="Static" />
                        </div>
                    </asp:PlaceHolder>
                </div>
                                
                <!-- Editing Question  -->
                <div class="question-edit">
                    <h3>შეკითხვის რედაქტირება</h3>
                    <div class="dropzone" id="dropz"></div>
                    <div class="question-img hidden">
                        <div><img /></div>
                        <a id="ClearQuestionImage" href="#" class="clear-image">სურათის წაშლა</a>
                    </div>
                    <div>
                        <textarea id="QuestionTextBox" class="type2" placeholder="აკრიფეთ შეკითხვის ტექსტი"></textarea>
                    </div>
                    <div class="question-score">
                        <div>
                            <label>ქულა</label>
                            <input id="ScoreTextBox" type="text" value="1" maxlength="3" />
                        </div>
                        <%--<div>
                            <label>Tags</label>
                            <input id="TagsTextBox" name="TagsTextBox" type="text" />                            
                            <asp:HiddenField ID="HFTags" runat="server" ClientIDMode="Static" />
                            <div class="tags-container" name="tags-container"></div>
                        </div>                        --%>
                    </div>                                                                                                            
                    <h4>დამატებითი ინფორმაცია</h4>                                    
                    <div>
                        <label>მინიშნება <span>(optional)</span></label>
                        <textarea id="HintTextBox"></textarea>
                    </div>
                    <%--<div>
                        <label>Analysis <span>(optional)</span></label>
                        <textarea id="AnalysisTextBox"></textarea>
                    </div>                    --%>
                    <div>
                        <div class="btn-group">
                            <table>
                                <tr>
                                    <td><a id="SaveQuestionButton" href="#">შენახვა</a></td>                                    
                                </tr>
                            </table>
                        </div>
                        <a id="DeleteQuestion" class="btn red delete" href="#">წაშლა</a>
                    </div>
                                    
                </div>
                                
                <!-- Editing Answer  -->
                <div class="answer-edit">
                    <h3>პასუხის რედაქტირება</h3>
                    <div>
                        <label>პასუხის ტექსი</label>
                        <textarea id="AnswerTextBox" class="type2"></textarea>
                    </div>
                    <div>
                        <label>სწორი პასუხი?</label>
                        <div class="toggler">
                            <div class="pad"></div>
                        </div>
                        <input type="hidden" id="HFCorrect" />
                    </div>
                    <%--<div>
                        <label>Explanation <span>(optional)</span></label>
                        <textarea id="ExplanationTextBox"></textarea>
                    </div>--%>
                    <div class="attach hidden">
                        <div class="clearfix">
                            <label>Attached Files <span>(optional)</span></label>
                            <div>
                                <b class="icon-text16x16 upload">Upload</b>
                                <input type="file" />
                            </div>
                        </div>
                        <ul>
                            <li>
                                <span class="icon-text16x16 pdf">Document Title</span>
                                <span class="tools">
                                    <a href="#" class="icon16 delete"></a>
                                    <a href="#" class="icon16 preview"></a>
                                </span>
                            </li>
                            <li>
                                <span class="icon-text16x16 pdf">Document Title</span>
                                <span class="tools">
                                    <a href="#" class="icon16 delete"></a>
                                    <a href="#" class="icon16 preview"></a>
                                </span>
                            </li>
                        </ul>
                    </div>
                                    
                                    
                    <div>
                        <div class="btn-group">
                            <table>
                                <tr>
                                    <td><a id="SaveAnswerButton" href="#">Save</a></td>
                                </tr>
                            </table>
                        </div>
                        <a id="DeleteAnswerButton" href="#" class="btn red delete">წაშლა</a>
                    </div>
                                    
                </div>
                                
                <!-- Question from Bank  -->
                <div class="question-from-bank">
                    <h3>Search Question in Bank</h3>
                                    
                    <div class="search-bank">
                        <input id="BankSearchTextBox" type="text" />
                    </div>
                    <div class="bank-content">                      
                    </div>
                </div>
            </td>
        </tr>
    </table>
</div>
<input type="hidden" id="HFUploadQuestionsUrl" value="<%=Root %>PopupPages/uploader.aspx?iud=3&quiz_id=<%=QuizID %>" />
<input type="hidden" id="HFQuizID" value="<%=QuizID %>" />
<input type="hidden" id="HFiud" value="<%=iud %>" />
<input type="hidden" id="HFConfirmDeleteQuestionAsset" value="<%=Resources.ConfirmDeleteQuestionAsset %>" />
<input type="hidden" id="HFConfirmDeleteQuestion" value="<%=Resources.ConfirmDeleteQuestion %>" />
<input type="hidden" id="HFVideoAssetsPopupUrl" value="<%=string.Format("/PopupPages/TeacherAssets.aspx?user_id={0}&teacher_id={1}",Root,UserID,OwnerID) %>" />
<input type="hidden" id="HFOwnerID" value="<%=OwnerID.ToString().EncryptWeb() %>" />
<input type="hidden" id="HFConfirmDeleteAnswer" value="<%=Resources.ConfirmDeleteAnswer %>" />
<asp:HiddenField ID="HFBackUrl" runat="server" ClientIDMode="Static" />
<input type="hidden" id="HFRequiredQuizCaption" value="<%=Resources.RequiredQuizCaption %>" />    
<asp:PlaceHolder ID="CourseQuizHFPlaceHolder" runat="server" EnableViewState="false" Visible="false">
<input type="hidden" id="HFRequiredStartDate" value="<%=Resources.RequiredStartDate %>" />
<input type="hidden" id="HFRequiredEndDate" value="<%=Resources.RequiredEndDate %>" />
<input type="hidden" id="HFStartGreaterThanEnd" value="<%=Resources.StartGreaterThanEnd %>" />
<input type="hidden" id="HFRequiredGradeDate" value="<%=Resources.RequiredGradeDate %>" />
</asp:PlaceHolder>