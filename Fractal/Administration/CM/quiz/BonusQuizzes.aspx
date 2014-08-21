<%@ Page Title="" Language="C#" MasterPageFile="~/Administration/MasterPage.master" AutoEventWireup="true" CodeBehind="BonusQuizzes.aspx.cs" Inherits="Fractal.Administration.CM.quiz.BonusQuizzes" Theme="DevEx" %>
<%@ MasterType VirtualPath="~/Administration/MasterPage.master" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="main-title">
	<span class="left"></span>        
    <a href="#" class="icon text add">დამატება</a>
    <span class="right"></span>
</div>  
<div class="container">
    <dx:ASPxGridView ID="BonusQuizzesGrid" ClientInstanceName="BonusQuizzesGrid" runat="server" DataSourceID="BonusQuizzesDataSource" KeyFieldName="ID" Width="780px">
        <Columns>
            <dx:GridViewDataComboBoxColumn FieldName="ID" Caption="ტესტი" Width="500px">
                <PropertiesComboBox DataSourceID="QuizzesDataSource" TextField="Caption" ValueField="ID"></PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewCommandColumn ButtonType="Image" Caption=" " ShowDeleteButton="true" ShowClearFilterButton="true" Width="60px"></dx:GridViewCommandColumn>
            <dx:GridViewDataColumn>
                <EditItemTemplate></EditItemTemplate>
            </dx:GridViewDataColumn>
        </Columns>
    </dx:ASPxGridView>    
    <asp:ObjectDataSource ID="BonusQuizzesDataSource" runat="server" TypeName="Core.Quiz" SelectMethod="ListTeacherBonusQuizes"  DeleteMethod="TSP_Quiz" InsertMethod="TSP_Quiz">     
        <SelectParameters>
            <asp:Parameter Name="UserID" />
        </SelectParameters>   
        <InsertParameters>
            <asp:Parameter Name="iud" Type="Byte" DefaultValue="1" />
            <asp:Parameter Name="ID" Type="Int64" />
            <asp:Parameter Name="Caption" Type="String" />
            <asp:Parameter Name="IsBonus" Type="Boolean" DefaultValue="True" />
        </InsertParameters>
        <DeleteParameters>
            <asp:Parameter Name="iud" Type="Byte" DefaultValue="1" />
            <asp:Parameter Name="ID" Type="Int64" />
            <asp:Parameter Name="Caption" Type="String" />
            <asp:Parameter Name="IsBonus" Type="Boolean" DefaultValue="False" />
        </DeleteParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="QuizzesDataSource" runat="server" SelectMethod="ListTeacherQuizes" TypeName="Core.Quiz">
        <SelectParameters>
            <asp:Parameter Name="UserID" />
        </SelectParameters>
    </asp:ObjectDataSource>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsPlaceHolder" runat="server">
<script type="text/javascript">
    $(function () {
        $(".add").click(function () {
            BonusQuizzesGrid.AddNewRow();
            return false;
        });
    });
</script>
</asp:Content>
