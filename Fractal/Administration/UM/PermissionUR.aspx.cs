using System;
using System.Linq;
using System.Text;
using Res = Core.Properties.Resources;
using Core;

public partial class management_UM_PermissionUR : System.Web.UI.Page
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

    protected void RolesGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        RolesGrid.Selection.UnselectAll();
        if (ActionsTree.FocusedNode != null)
        {
            new Role().ListPermissionRoles((int)ActionsTree.FocusedNode.GetValue("ID"))
                      .Where(r => r.IsGrant)
                      .ToList()
                      .ForEach(r =>
            {
                RolesGrid.Selection.SelectRow(RolesGrid.FindVisibleIndexByKeyValue(r.ID.ToString()));
            });
        }
    }

    protected void UsersGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        UsersGrid.Selection.UnselectAll();
        if (RolesGrid.FocusedRowIndex > -1)
        {
            Master.UserObject.ListPermissionUsers((int)ActionsTree.FocusedNode.GetValue("ID"))
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

    void Save()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<data>")
          .AppendFormat("<permission_id>{0}</permission_id>", ActionsTree.FocusedNode.GetValue("ID"))
          .Append("<users>");

        UsersGrid.GetSelectedFieldValues("ID").ToList().ForEach(r =>
        {
            sb.AppendFormat("<user><id>{0}</id></user>",r);
        });
        sb.Append("</users><roles>");


        RolesGrid.GetSelectedFieldValues("ID").ForEach(r =>
        {
            sb.AppendFormat("<role><id>{0}</id></role>", r);
        });
        sb.Append("</roles></data>");
        //System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "file.xml", xml);
        Master.UserObject.TX_UM(2, sb.ToString());
        if (Master.UserObject.IsError)
        {
            throw new Exception(Res.Abort);
        }        
    }
}