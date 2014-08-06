<%@ Control Language="C#" AutoEventWireup="true" Inherits="StreamingTutorsWebProject.UserControls.SuccessErrorControl" Codebehind="SuccessErrorControl.ascx.cs" %>
<asp:Panel ID="SuccesErrorPanel" runat="server" CssClass="succes-error hidden" ClientIDMode="Static">
<asp:Literal ID="MessageLiteral" runat="server" ViewStateMode="Disabled" Text="Some Message"></asp:Literal><a href="#"></a>
</asp:Panel>
<style type="text/css">
.succes-error{
    position:fixed;
    left:0;
    top:-60px;
    z-index:999;
    width:100%;
    background:#0e9256;
    
    margin:0;
    color:#fff;
    font-size:14px;
    line-height:60px;
    text-align:center;
    -webkit-transition: all 0.5s;
    -moz-transition: all 0.5s;
    transition: all 0.5s;
}
.succes-error.error{
    /*background:#c50c0c;*/
    background:#F73939;
}
.succes-error > a{
    position:absolute;
    right:20px;
    top:18px;
    display:block;
    width:24px;
    height:24px;
    background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyFpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuNS1jMDIxIDc5LjE1NDkxMSwgMjAxMy8xMC8yOS0xMTo0NzoxNiAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENDIChXaW5kb3dzKSIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDpDM0NCNTZDM0RBOUQxMUUzQjczRkM4MEYyQUZGQTlFMCIgeG1wTU06RG9jdW1lbnRJRD0ieG1wLmRpZDpDM0NCNTZDNERBOUQxMUUzQjczRkM4MEYyQUZGQTlFMCI+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOkMzQ0I1NkMxREE5RDExRTNCNzNGQzgwRjJBRkZBOUUwIiBzdFJlZjpkb2N1bWVudElEPSJ4bXAuZGlkOkMzQ0I1NkMyREE5RDExRTNCNzNGQzgwRjJBRkZBOUUwIi8+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+xOV4+QAAAP5JREFUeNrsld0KgkAQhbUHiLAftIfU94v0VtAKoihfI7rxbpuFkcZlZn8CverAd9PMnGOts8VKqWhKLaKJNXtAAfRACSQBPmugwtl8VNFnQHirr85AYtQ5dM+FzL1o3Ww+qrFcIQn2UB1sASugMQaeQMqYp1ijatBDDJBCHkaIl7kUMIS0hsEV2ISY2wI0O+BuGJ0Qqhv2RqEB0jehaqUn9w2whTjNNT6bHOt1YT73u8QcT5DiGyRJH/z215+IWyLukK3LKJlnzKtYA0ukZpYx8w3YA51gPvRwIR3OWgN8zINCZr/sJr+uC6AHSk/zgTVQ4WxOa/H/T9+ljwADAPJXD1r7CYx6AAAAAElFTkSuQmCC);
}

.succes-error.opened{
    top:0;
}
</style>
<script>$(function () { $('.succes-error a').click(function () { $(this).parent('div').removeClass('opened'); return false; }); if (!$("#SuccesErrorPanel").hasClass("error")) { setTimeout(function () { $("#SuccesErrorPanel a").trigger("click") }, 3000) } });</script>

