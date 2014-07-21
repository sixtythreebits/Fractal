using System;
using System.Linq;
using System.Text;
using Res = Core.Properties.Resources;
using Core;

public partial class ManagementUmUserRp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        InitStartUp();        
    }

    void InitStartUp()
    {
        ActionsTree.SettingsEditing.AllowNodeDragDrop = false;
        if (!IsPostBack)
        {
            UsersGrid.SettingsPager.PageSize = 10;
        }
    }

    protected void ActionsTree_CustomCallback(object sender, DevExpress.Web.ASPxTreeList.TreeListCustomCallbackEventArgs e)
    {
        ActionsTree.UnselectAll();
        if (UsersGrid.FocusedRowIndex > -1)
        {
            var U = new User();
            U.GetSingleUser((long)UsersGrid.GetRowValues(UsersGrid.FocusedRowIndex, "ID"));
            U.Permissions.Where(P => P.IsGrant).ToList().ForEach(p =>
            {
                ActionsTree.FindNodeByKeyValue(p.ID.ToString()).Selected = true;
            });
        }
    }

    protected void ActionsTree_SelectionChanged(object sender, EventArgs e)
    {
        if (ActionsTree.FocusedNode.Selected)
        {
            SelectParentNode(ActionsTree.FocusedNode);
        }
        else
        {
            UnselectChildren(ActionsTree.FocusedNode);
        }
    }

    protected void RolesGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        RolesGrid.Selection.UnselectAll();
        if (UsersGrid.FocusedRowIndex > -1)
        {
            var U = new User();
            U.GetSingleUser((long)UsersGrid.GetRowValues(UsersGrid.FocusedRowIndex, "ID"));

            U.Roles.Where(r => r.IsGrant).ToList().ForEach(r =>
            {
                RolesGrid.Selection.SelectRow(RolesGrid.FindVisibleIndexByKeyValue(r.ID.ToString()));
            });
        }
    }

    protected void Callbacker_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        switch (e.Parameter)
        {
            case "Save":
                {
                    Save();
                    break;
                }
        }
    }

    void SelectParentNode(DevExpress.Web.ASPxTreeList.TreeListNode Node)
    {
        if (Node.ParentNode != null && Node.ParentNode != ActionsTree.RootNode)
        {
            Node.ParentNode.Selected = true;
            SelectParentNode(Node.ParentNode);
        }
    }

    void UnselectChildren(DevExpress.Web.ASPxTreeList.TreeListNode Node)
    {
        foreach (DevExpress.Web.ASPxTreeList.TreeListNode Child in Node.ChildNodes)
        {
            Child.Selected = false;
            if (Child.ChildNodes.Count > 0)
            {
                UnselectChildren(Child);
            }
        }
    }

    void Save()
    {
        var sb = new StringBuilder();
        sb.Append("<data>")
          .AppendFormat("<user_id>{0}</user_id>",UsersGrid.GetRowValues(UsersGrid.FocusedRowIndex, "ID"))
          .Append("<roles>");
        RolesGrid.GetSelectedFieldValues("ID").ToList().ForEach(r => sb.AppendFormat("<role><id>{0}</id></role>", r));
        sb.Append("</roles><permissions>");

        ActionsTree.GetSelectedNodes().ForEach(n => sb.AppendFormat("<permission><id>{0}</id></permission>",n.Key));
        sb.Append("</permissions></data>");
        //System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "file.xml", xml);
        Master.UserObject.TX_UM(0, sb.ToString());
        if (Master.UserObject.IsError)
        {
            throw new Exception(Res.Abort);
        }       
    }
}