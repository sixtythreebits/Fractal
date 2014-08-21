using System;
using System.Linq;
using Lib;
using Res = Core.Properties.Resources;
using Core;
using Core.Utilities;

public partial class management_UM_users : System.Web.UI.Page
{
    User RowUserObject;
    public string FormatDateTime = Res.FormatDateTime;

    protected void Page_Load(object sender, EventArgs e)
    {
     
    }
    
    protected void UsersDataSource_Deleting(object sender, System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs e)
    {
        while (e.InputParameters.Count > 2)
        {
            e.InputParameters.RemoveAt(2);
        }
    }

    protected void UsersDataSource_Inserting(object sender, System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs e)
    {
        while (e.InputParameters.Count > 2)
        {
            e.InputParameters.RemoveAt(2);
        }
    }

    protected void UsersDataSource_Updating(object sender, System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs e)
    {                
        while (e.InputParameters.Count > 2)
        {
            e.InputParameters.RemoveAt(2);
        }
    }

    protected void UsersGrid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        long UserID = (long)e.Keys["ID"];
        CheckAdmin(UserID);
        var U = new User();
        U.GetSingleUser(UserID);
        if (!string.IsNullOrEmpty(U.Avatar))
        {
            System.IO.File.Delete(AppSettings.UploadFolderPhysicalPath + U.Avatar);
        }
        UsersDataSource.DeleteParameters["xml"].DefaultValue =
       "<data>" +
        "<id>" + e.Keys["ID"] + "</id>" +
       "</data>";
        //System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "file.xml", s);
    }

    protected void UsersGrid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        UsersDataSource.InsertParameters["xml"].DefaultValue =
        "<data>" +
         "<username>" + e.NewValues["Username"] + "</username>" +
         (e.NewValues["Password"] == null ? string.Empty : ("<password>" + e.NewValues["Password"].ToString().MD5() + "</password>")) +
         "<fname>" + e.NewValues["Fname"] + "</fname>" +
         "<lname>" + e.NewValues["Lname"] + "</lname>" +
         "<email>" + e.NewValues["Email"] + "</email>" +
            //"<is_teacher>" + e.NewValues["IsTeacher"] + "</is_teacher>" +
            //"<is_ta>" + e.NewValues["IsTA"] + "</is_ta>" +
            //"<is_admin>" + e.NewValues["IsAdmin"] + "</is_admin>" +
         "<is_active>" + e.NewValues["IsActive"] + "</is_active>" +
         "<city_id>" + e.NewValues["CityID"] + "</city_id>" +
        "</data>";
    }

    protected void UsersGrid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        long UserID = (long)e.Keys["ID"];
        CheckAdmin(UserID);
        UsersDataSource.UpdateParameters["xml"].DefaultValue =
       "<data>" +
        "<id>" + e.Keys["ID"] + "</id>" +
        "<username>" + e.NewValues["Username"] + "</username>" +
        (e.NewValues["Password"] == null ? string.Empty : ("<password>" + e.NewValues["Password"].ToString().MD5() + "</password>")) +
        "<fname>" + e.NewValues["Fname"] + "</fname>" +
        "<lname>" + e.NewValues["Lname"] + "</lname>" +        
        "<email>" + e.NewValues["Email"] + "</email>" +
        //"<is_teacher>" + e.NewValues["IsTeacher"] + "</is_teacher>" +
        //"<is_ta>" + e.NewValues["IsTA"] + "</is_ta>" +
        //"<is_admin>" + e.NewValues["IsAdmin"] + "</is_admin>" +
        "<is_active>" + e.NewValues["IsActive"] + "</is_active>" +
        "<city_id>" + e.NewValues["CityID"] + "</city_id>" +
       "</data>";
    }

    protected void UsersGrid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {        
        if (e.OldValues["Username"] != e.NewValues["Username"])
        {
            long? ID = (long?)e.Keys["ID"];
            var U = new User();
            U = U.ListUsers(3, e.NewValues["Username"]).DefaultIfEmpty(new User()).Single();
            if ((ID == null && U.ID > 0) /*insert*/ ||
               (ID > 0 && U.ID > 0 && ID != U.ID) /*edit*/)
            {
                throw new Exception(Res.UniqUsername);
            }

            U = U.ListUsers(0, e.NewValues["Email"]).DefaultIfEmpty(new User()).Single();
            if ((ID == null && U.ID > 0) /*insert*/ ||
               (ID > 0 && U.ID > 0 && ID != U.ID) /*edit*/)
            {
                throw new Exception(Res.UniqEmail);
            }
        }  
    }

    void CheckAdmin(long ID)
    {
        var U = new User();
        U.GetSingleUser(ID);
        if (ID == 1 || U.Username.ToLower() == "admin")
        {        
            throw new Exception(Res.DontTouchMe);
        }
    }
    
    protected void UsersDataSource_Deleted(object sender, System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs e)
    {
        if (RowUserObject.IsError && RowUserObject.IsClient)
        {
            throw new Exception(RowUserObject.ErrorMessage);
        }
    }

    protected void UsersDataSource_ObjectCreated(object sender, System.Web.UI.WebControls.ObjectDataSourceEventArgs e)
    {
        RowUserObject = e.ObjectInstance as User;
    }
}