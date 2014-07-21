using System;
using System.Linq;
using System.Text;
using Res = Core.Properties.Resources;
using Core;

public partial class management_UM_RoleUP : System.Web.UI.Page
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
            UsersGrid.SettingsPager.PageSize = 20;
        }
    }

    protected void ActionsTree_CustomCallback(object sender, DevExpress.Web.ASPxTreeList.TreeListCustomCallbackEventArgs e)
    {
        ActionsTree.UnselectAll();
        if (RolesGrid.FocusedRowIndex > -1)
        {

            new Permission().ListRolePermissions((int)RolesGrid.GetRowValues(RolesGrid.FocusedRowIndex, "ID"))
                            .Where(p => p.IsGrant)
                            .ToList()
                            .ForEach(p =>
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

    protected void UsersGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        UsersGrid.Selection.UnselectAll();
        if (RolesGrid.FocusedRowIndex > -1)
        {
            Master.UserObject.ListRoleUsers((int)RolesGrid.GetRowValues(RolesGrid.FocusedRowIndex, "ID"))
                             .Where(u => u.IsGrant)
                             .ToList()
                             .ForEach(u =>
            {
                UsersGrid.Selection.SelectRow(UsersGrid.FindVisibleIndexByKeyValue(u.ID.ToString()));
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
        StringBuilder sb = new StringBuilder();
        sb.Append("<data>")
          .AppendFormat("<role_id>{0}</role_id>", RolesGrid.GetRowValues(RolesGrid.FocusedRowIndex, "ID"))
          .Append("<users>");

        UsersGrid.GetSelectedFieldValues("ID").ToList().ForEach(r =>
        {
            sb.AppendFormat("<user><id>{0}</id></user>", r);
        });
        sb.Append("</users><permissions>");

        ActionsTree.GetSelectedNodes().ForEach(n =>
        {
            sb.AppendFormat("<permission><id>{0}</id></permission>", n.Key);
        });
        sb.Append("</permissions></data>");
        //System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "file.xml", xml);
        Master.UserObject.TX_UM(1, sb.ToString());
        if (Master.UserObject.IsError)
        {
            throw new Exception(Res.Abort);
        }
    }
}