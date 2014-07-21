<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_ExporterControl" Codebehind="ExporterControl.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList.Export" TagPrefix="dx" %>


<dx:ASPxGridViewExporter ID="Exporter" runat="server"></dx:ASPxGridViewExporter>
<dx:ASPxTreeListExporter ID="TreeExporter" runat="server"></dx:ASPxTreeListExporter>
<asp:LinkButton ID="ExporterButton1" runat="server" Text="ექსოპორტი" CssClass="icon text xls" OnClick="DoExport" ToolTip="Export To XLS"></asp:LinkButton>
