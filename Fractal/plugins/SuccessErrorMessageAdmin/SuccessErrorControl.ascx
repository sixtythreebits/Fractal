<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_SuccessErrorControl" Codebehind="SuccessErrorControl.ascx.cs" %>
<asp:PlaceHolder ID="SuccessPlaceHolder" runat="server" EnableViewState="false" Visible="false">
<div class="success-massage<%=IsClientHidden?" hidden":string.Empty %><%=IsUploader?" uploader":string.Empty %>">
	<span></span>
	<div>
        <span class="icon-success"></span>		
		<p><asp:Literal ID="SuccessMessageLiteral" runat="server" EnableViewState="false"></asp:Literal></p>
	</div>
	<span></span>
</div>
</asp:PlaceHolder>
<asp:PlaceHolder ID="ErrorPlaceHolder" runat="server" EnableViewState="false" Visible="false">
<div class="error-massage<%=IsClientHidden?" hidden":string.Empty %><%=IsUploader?" uploader":string.Empty %>">
	<span></span>
	<div>
        <span class="icon-error"></span>		
		<p><asp:Literal ID="ErrorMessageLiteral" runat="server" EnableViewState="false"></asp:Literal></p>
	</div>
	<span></span>
</div>
</asp:PlaceHolder>
