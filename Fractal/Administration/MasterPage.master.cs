using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using Core;

public partial class administration_MasterPage : AdminMasterBase
{
    #region Properties
    public string PageName { set; get; }
    public string PagePath { set; get; }
    public string guid { set; get; }

    public string DialogText
    {
        set
        {
            DialogPlaceHolder.Visible = true;
            DialogTextLiteral.Text = value;
            JQueryUIEnabled = true;
            HeaderInclude.AppendFormat("<script type='text/javascript' src='/plugins/jquery-ui/js/dialog.js'></script>{0}", Environment.NewLine);

        }
        get { return DialogTextLiteral.Text; }
    }

    public string PageTitle
    {
        set { PageTitlePlaceHolder.Visible = true; PageTitleLiteral.Text = value; }
        get { return PageTitleLiteral.Text; }
    }
    public string LastNavigationMenuTitle { set; get; }

    public bool IsCustomNavigationMenuBuild { set; get; }
    public string NavigationMenuHTML
    {
        set { NavigationMenuLiteral.Text = value; }
        get { return NavigationMenuLiteral.Text; }
    }            
    #endregion Properties

    protected void Page_Init(object sender, EventArgs e)
    {        
        SetNoCache();
        InitStartUp();        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        SetPageTitle();
        BuildNavigation();
        BuildMenu();
        InitHelp();
        HeadLiteral.Text = HeaderInclude.ToString();
    }

    void InitStartUp()
    {                
        if (UserObject.IsAuthorized)
        {
            var AdministrationFolder = "administration";            
            PagePath = Request.FilePath.ToLower().Replace("/" + AdministrationFolder + "/", "").ToLower();            
            PageName = Path.GetFileName(PagePath);
            guid = Guid.NewGuid().ToString();
            User.CheckPermission(PagePath);
        }
        else
        {
            Response.Redirect("~/");            
        }
    }
        
    void SetNoCache()
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddHours(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("Pragma", "no-cache");
    }
    
    void BuildMenu()
    {
        var Hierarchy = UserObject.Permissions.Where(p1 => p1.AspxPagePath == PagePath).DefaultIfEmpty(new Permission()).Single().Hierarchy;
        var CurrentPagePermissionHierarchy = string.IsNullOrEmpty(Hierarchy) ? new string[]{} : Hierarchy.Split('.');
        
        if (Session["ParentMenuItems"] != null && CurrentPagePermissionHierarchy.Length > 5)
        {
            CurrentPagePermissionHierarchy = ((Permission[])Session["ParentMenuItems"])[1].Hierarchy.Split('.');
        }

        TopMenuRepeater.DataSource = UserObject.Permissions.Where(p => p.Level == 1).Select(p => new
        {
            ID = p.ID,
            Caption = p.Caption,
            IsOpened = CurrentPagePermissionHierarchy.Contains(p.ID.ToString())
        }).ToList();
        TopMenuRepeater.DataBind();
    }

    void SetPageTitle()
    {
        if (string.IsNullOrEmpty(PageTitle))
        {
            PageTitlePlaceHolder.Visible = true;
            var item = UserObject.Permissions.Where(p => p.AspxPagePath == PagePath).DefaultIfEmpty(new Permission()).Single();
            Page.Title = item.Caption;
            PageTitleLiteral.Text = item.Caption;
        }                        
    }

    void BuildNavigation()
    {
        if (!IsCustomNavigationMenuBuild)
        {            
            var list = Permission.ListPermissionHierarchyByAspxPagePath(PagePath);
            var PageNames = list.Select(p => p.Caption).ToList();
            var Urls = list.Take(PageNames.Count - 1).Select(p => Root + p.AspxPagePath).ToList();
            if (Urls.Count > 0)
            {
                Urls[0] = string.Empty;
            }            
            NavigationMenuLiteral.Text = Navigation.GetNavigationMenu(PageNames, Urls);
        }
    }

    void InitHelp()
    {        
        var item = UserObject.Permissions.Where(p => p.AspxPagePath == PagePath).FirstOrDefault();        
        if (item != null && item.IncludesHelp && !string.IsNullOrWhiteSpace(item.HelpUrl))
        {
            HelpPlaceHolder.Visible = true;
            HelpLink.NavigateUrl = item.HelpUrl;
        }
    }
    
    protected void TopMenuRepeater_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {
        dynamic item = (dynamic)e.Item.DataItem;
        var Children = UserObject.Permissions.Where(p => p.ParentID == item.ID).Select(p => new
        {
            ID = p.ID,
            Caption = p.Caption,
            AspxPagePath = p.AspxPagePath,
            Icon = p.Icon,
            Selected = p.AspxPagePath == PagePath 
        }).ToList();

        if (Children.Count > 0)
        {
            var SubMenuRepeater = (Repeater)e.Item.FindControl("SubMenuRepeater");
            SubMenuRepeater.Visible = true;
            SubMenuRepeater.DataSource = Children;
            SubMenuRepeater.DataBind();
        }
    }
}


