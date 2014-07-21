using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Text;
using Core;
using Lib;


public partial class administration_course_MasterPage : System.Web.UI.MasterPage
{
    #region Properties
    public Course CourseObject { set; get; }
            
    List<Permission> CourseItems;
    #endregion Properties

    protected void Page_Init(object sender, EventArgs e)
    {
        InitStartUp();        
    }

    protected void Page_Load(object sender, EventArgs e)
    {        
        
    }

    void InitStartUp()
    {
        long? ID = Request.QueryString["id"].ToLong();
        CourseObject = new Course(ID);
        if (CourseObject.IsError)
        {
            Response.Redirect(Master.Root + "CM/courses.aspx");                
        }
        else
        {
            InitHeader();
            InitNavigation();
            CourseCaptionLiteral.Text = CourseObject.Caption;
            Master.PageTitle = " ";
            Page.Title = "წიგნი " + CourseObject.Caption;
        }
    }

    void InitHeader()
    {
        Master.HeaderInclude.AppendFormat("<link type='text/css' href='{0}css/box-bar.css' rel='stylesheet'/>{1}", Master.Root, Environment.NewLine);
    }

    void InitNavigation()
    {
        Master.IsCustomNavigationMenuBuild = true;
        var PermissionCode = "A26E6A52-77A3-46BB-AE55-D775944FFFC0";

        var PermissionHierarchyList = Permission.ListPermissionHierarchyByAspxPagePath(Master.PagePath);        
        var CourseLevelID = PermissionHierarchyList.Where(p => p.PermissionCode == PermissionCode).SingleOrDefault().ID;

        var Captions = PermissionHierarchyList.Select(p => p.ID == CourseLevelID ? CourseObject.Caption : p.Caption).ToList();
        var Links = PermissionHierarchyList.Take(Captions.Count - 1).Select(p => Master.Root + (p.ID == CourseLevelID || p.ParentID == CourseLevelID ? p.AspxPagePath + "?id=" + CourseObject.ID : p.AspxPagePath)).ToList();
        Links[0] = string.Empty;
        if (Session["ParentMenuItems"] != null)
        {
            var ParentItem = ((Permission[])Session["ParentMenuItems"])[0];
            var Item = ((Permission[])Session["ParentMenuItems"])[1];
            Captions[0] = ParentItem.Caption;
            Captions[1] = Item.Caption;
            Links[1] = Master.Root + Item.AspxPagePath;
        }
        Master.NavigationMenuHTML = Navigation.GetNavigationMenu(Captions, Links);

        if (Captions.Count > 3)
        {
            CourseNavigationLevelPlaceHolder.Visible = true;
            CourseNavigationCurrentLevelLiteral.Text = Captions[3];
        }
        
        CourseItems = Permission.ListChildrenPermissionsFromUserPermissions(PermissionCode);
        CourseMenuRepeater.DataSource = CourseItems;
        CourseMenuRepeater.DataBind();
    }

    public void InitCourseItems()
    {        
        var CourseItemsRepeater = Lib.Service.FindControl<Repeater>(Page, "CourseItemsRepeater");
        if (CourseItemsRepeater != null)
        {
            CourseItemsRepeater.DataSource = CourseItems;
            CourseItemsRepeater.DataBind();
        }
    }
}
