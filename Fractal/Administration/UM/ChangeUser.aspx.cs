using System;
using System.Linq;
using Core;


public partial class management_ChangeUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        LoadData();
    }    

    void LoadData()
    {
        if (!IsPostBack)
        {
            var U = new User();
            UsersCombo.DataSource = U.ListUsers().Select(u => new { ID = u.ID, Name = u.FullName + " - " + u.Username });
            UsersCombo.DataBind();
            UsersCombo.Value = Master.UserObject.ID.ToString();
        }
    }

    protected void UsersCombo_SelectedIndexChanged(object sender, EventArgs e)
    {
        long UserID = long.Parse(UsersCombo.Value.ToString());
        Master.UserObject.GetSingleUser(UserID);
        Master.UserObject.SetAuthorizedCredentials("ID", Master.UserObject.ID);
        Master.UserObject.SetAuthorizedCredentials("Fname", Master.UserObject.Fname);
        Master.UserObject.SetAuthorizedCredentials("Lname", Master.UserObject.Lname);        
    }
}
