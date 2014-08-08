using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Lib;
using Core;
using Core.Properties;

public partial class administration_CM_courses : System.Web.UI.Page
{
    List<Course> CourseList;

    protected void Page_Load(object sender, EventArgs e)
    {
        InitStartUp();
    }

    void InitStartUp()
    {
        string action = Request.Form["action"];
        if (string.IsNullOrEmpty(action))
        {
            if (!IsPostBack)
            {
                InitCourses();
            }                        
            Master.DialogText = Resources.ConfirmDeleteCourse;

        }
        else
        {
            ExecAjax(action);
        }        
    }

    void InitCourses()
    {
        var C = new Course();
        CourseList = C.ListUserCourses(Master.UserObject.ID).OrderBy(c => c.Caption).ToList();
        LettersRepeater.DataSource = CourseList.Select(g => (g.Caption.Length > 0 ? g.Caption[0].ToString().ToLower() : " ")).Distinct();
        LettersRepeater.DataBind();
    }

    protected void LettersRepeater_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {
        var CoursesRepeater = (Repeater)e.Item.FindControl("CoursesRepeater");
        CoursesRepeater.DataSource = CourseList.Where(c => c.Caption.ToLower().StartsWith(e.Item.DataItem.ToString().ToLower())).ToList();
        CoursesRepeater.DataBind();
    }

    #region AJAX
    void ExecAjax(string Action)
    {
        Response.Clear();
        try
        {
            switch (Action)
            {
                case "create":
                    {
                        CreateCourse();
                        break;
                    }
                case "delete":
                    {
                        DeleteCourse();
                        break;
                    }      
                case "rename":
                    {
                        RenameCourse();
                        break;
                    }
            }
        }
        catch (Exception ex)
        {
            (ex.Message).LogString();
        }        

        Response.End();
    }

    void CreateCourse()
    {
        var C = new Course();
        var x = string.Format(@"
        <data>
            <caption>New Course</caption>
            <year>{0}</year>
            <author>{1}</author>
            <user_id>{2}</user_id>
        </data>
        ",
        DateTime.Now.Year,
        Master.UserObject.FullName,
        Master.UserObject.ID
        );
        C.TX_Courses(0, x);
        if (C.IsError || C.Properties == null)
        {
            Response.Write("error");
        }
        else
        {
            Response.Write("success_" + C.Properties.Value);
        }
        
    }

    void DeleteCourse()
    {
        var ID = Request.Form["id"].ToLong();
        var C = new Course();
        C.DeleteCourse(ID);
    }

    void RenameCourse()
    {
        var ID = Request.Form["id"].ToLong();
        string Caption = Request.Form["caption"];
        var C = new Course();
        C.TSP_Courses(1, ID, Caption);
        if (!C.IsError)
        {
            Response.Write("success");
        }
    }
    #endregion AJAX
}