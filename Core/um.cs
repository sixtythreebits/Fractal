using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml.Linq;
using System.Web;
using Lib;
using SystemBase;
using DB;


namespace Core
{
    public class User : ObjectBase
    {
        #region Properties
        public long? ID { set; get; }
        public string Username { set; get; }
        public string Password { set; get; }
        public string Fname { set; get; }
        public string Lname { set; get; }
        public string FullName { set; get; }
        public string UsernameFullName { set; get; }
        public DateTime? BirthDate { set; get; }
        public string Email { set; get; }
        public string Mobile { set; get; }
        public string Address1 { set; get; }
        public string Address2 { set; get; }
        public string Zip { set; get; }
        public string CityID { set; get; }
        public string Avatar { set; get; }
        public string About { set; get; }
        public bool IsAuthorized { set; get; }
        public bool IsAdmin { set; get; }
        public bool IsGrant { set; get; }
        public bool IsActive { set; get; }
        public DateTime? LastVisitDate { set; get; }
        public long? VisitCount { set; get; }
        public List<Role> Roles { set; get; }
        public List<Permission> Permissions { set; get; }
        public XElement Properties { set; get; }
        #endregion Properties

        #region Constructors
        public User() { }

        public User(long ID)
        {
            GetSingleUser(ID);
        }

        public User(string SlugOrID)
        {
            GetSingleUser(SlugOrID.ToLong(), SlugOrID);
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// User authentication with username and password
        /// </summary>
        /// <param name="Username">Username</param>
        /// <param name="Password">Password</param>
        /// <returns>True if authenticated, false if not</returns>
        public bool Authenticate(string Username, string Password)
        {
            return TryToReturn<bool>(string.Format("UM.User.Authenticate(Username = {0}, Password = {1})", Username, Password), () =>
            {
                using (var db = ConnectionFactory.GetDBUMDataContext())
                {

                    XElement el = null;
                    db.UM_Authenticate(Username, Password, ref el);
                    if (el != null)
                    {

                        var U = new SessionUser();
                        U.IsAuthorized = true;
                        U.ID = long.Parse(el.Element("id").Value);
                        U.Username = el.Element("username").Value;
                        U.Password = Password;
                        U.Fname = el.Element("fname").Value;
                        U.Lname = el.Element("lname").Value;
                        U.Email = el.Element("email").Value;
                        U.Avatar = el.Element("avatar").Value;

                        #region ROLES AND PERMISSIONS
                        if (U.Username == "admin")
                        {
                            U.Permissions = new Permission().ListPermissions(null);
                            U.Permissions.ForEach(p =>
                            {
                                if (!string.IsNullOrWhiteSpace(p.AspxPagePath))
                                {
                                    p.AspxPagePath = p.AspxPagePath.ToLower();
                                }
                            });
                        }
                        else
                        {
                            U.Permissions = el.Element("permissions") == null ? new List<Permission>() :
                            el.Element("permissions").Descendants("permission").Select(p => new Permission
                            {
                                ID = int.Parse(p.Element("id").Value),
                                ParentID = p.Element("parent_id") == null ? null : (int?)int.Parse(p.Element("parent_id").Value),
                                Caption = p.Element("caption") == null ? null : p.Element("caption").Value,
                                Code = p.Element("code") == null ? null : p.Element("code").Value,
                                PermissionCode = p.Element("permission_code") == null ? null : p.Element("permission_code").Value,
                                AspxPagePath = p.Element("aspx_page_path") == null ? string.Empty : p.Element("aspx_page_path").Value,
                                ControlID = p.Element("control_id") == null ? null : p.Element("control_id").Value,
                                Level = short.Parse(p.Element("level").Value),
                                Hierarchy = p.Element("hierarchy").Value,
                                Icon = p.Element("icon").Value,
                                IncludesHelp = p.BooleanValueOf("includes_help") == true,
                                HelpUrl = p.ValueOf("help_url")
                            }).ToList();
                        }

                        U.Roles = el.Element("roles") == null ? new List<Role>() :
                        el.Element("roles").Descendants("role").Select(p => new Role
                        {
                            ID = int.Parse(p.Element("id").Value),
                            Caption = p.Element("caption").Value,
                            Code = byte.Parse(p.Element("code").Value)
                        }).ToList();
                        
                        U.IsAdmin = IsAdmin = el.Element("username").Value == "admin" || U.Roles.Where(r => r.Code == 1).Count() > 0;
                        #endregion ROLES AND PERMISSIONS

                        HttpContext.Current.Session["UserInfo"] = U;
                        return true;

                    }
                    return false;
                }
            });
        }

        /// <summary>
        /// Checkes Persmission for autherized user and throws out of the management system if persmision is not granted
        /// </summary>
        public static void CheckPermission(string Permission)
        {
            var U = new User();
            if (U.GetAuthorizedCredentials())
            {
                if (U.IsAdmin || string.IsNullOrEmpty(Permission))
                {
                    return;
                }
                else if (U.Permissions.Where(A => A.AspxPagePath.ToLower() == Permission.ToLower() || A.Code == Permission || A.PermissionCode == Permission).Count() == 0)
                {
                    System.Web.HttpContext.Current.Response.Redirect("~/");
                }
            }
            else
            {
                System.Web.HttpContext.Current.Response.Redirect("~/");
            }
        }

        /// <summary>
        /// Checkes Persmission for autherized user and throws out of the management system, if persmision is not granted and DoRedirect is true
        /// </summary>        
        public static bool CheckPermission(string Permission, bool DoRedirect)
        {
            var U = new User();
            if (U.GetAuthorizedCredentials())
            {
                if (U.IsAdmin || string.IsNullOrEmpty(Permission))
                {
                    return true;
                }
                else if (U.Permissions.Where(A => A.AspxPagePath.ToLower() == Permission.ToLower() || A.Code == Permission || A.PermissionCode == Permission).Count() == 0)
                {
                    if (DoRedirect)
                    {
                        System.Web.HttpContext.Current.Response.Redirect("~/");
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                if (DoRedirect)
                {
                    System.Web.HttpContext.Current.Response.Redirect("~/");
                }
            }
            return false;
        }

        /// <summary>
        /// Initialize authorized user from session
        /// </summary>        
        public bool GetAuthorizedCredentials()
        {
            try
            {
                var obj = HttpContext.Current.Session["UserInfo"];
                if (obj == null)
                {
                    return false;
                }
                else
                {
                    var U = (SessionUser)obj;
                    this.IsAuthorized = U.IsAuthorized;
                    this.ID = U.ID;
                    this.Username = U.Username;
                    this.Password = U.Password;
                    this.Fname = U.Fname;
                    this.Lname = U.Lname;
                    this.Email = U.Email;
                    this.Avatar = U.Avatar;
                    this.FullName = U.Fname + " " + U.Lname;
                    this.IsAdmin = U.IsAdmin;

                    this.Permissions = U.Permissions;
                    this.Roles = U.Roles;

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets single user by ID, fills all existing and additional properties
        /// </summary>
        /// <param name="UserID">User ID Value</param>
        /// <param name="Slug">User slug</param>
        public void GetSingleUser(long? UserID = null, string Slug = null)
        {
            TryExecute(string.Format("UM.User.GetSingleUser(UserID = {0}, Slug = {1})", UserID, Slug), () =>
            {
                using (var db = ConnectionFactory.GetDBUMDataContext())
                {
                    var x = db.UM_GetSingleUser(UserID, Slug);
                    if (x == null)
                    {
                        IsError = true;
                        ErrorMessage = "User Not Found";
                    }
                    else
                    {
                        this.ID = x.LongValueOf("id");
                        this.Username = x.ValueOf("username");
                        this.Password = x.ValueOf("password");
                        this.Fname = x.ValueOf("fname");
                        this.Lname = x.ValueOf("lname");
                        this.FullName = this.Fname + " " + this.Lname;
                        this.BirthDate = x.DateTimeValueOf("birth_date");
                        this.Email = x.ValueOf("email");
                        this.Mobile = x.ValueOf("mobile");
                        this.Address1 = x.ValueOf("Address1");
                        this.Address2 = x.ValueOf("address2");
                        this.Zip = x.ValueOf("zip");
                        this.CityID = x.ValueOf("city_id");
                        this.Avatar = x.ValueOf("avatar");
                        this.LastVisitDate = x.DateTimeValueOf("last_visit_date");
                        this.VisitCount = x.LongValueOf("visit_count");
                        this.About = x.ValueOf("about");
                        this.IsActive = x.BooleanValueOf("is_active") == true;

                        this.Roles = new Role().ListUserRoles(this.ID);
                        this.Permissions = new Permission().ListUserPermissions(this.ID);

                        this.IsAdmin = (this.ID == 1 || Roles.Where(r => r.Code == 1).Count() > 0);
                    }
                }
            });
        }

        /// <summary>
        /// Gets Univesal Password
        /// </summary>  
        public string GetUniversalPassword()
        {
            return "2fdje4f67-7936-4d3d-9857-4223654";
        }

        /// <summary>
        /// Checks whether authenticated user has permission or not
        /// </summary>
        /// <param name="Permission"></param>
        /// <returns></returns>
        public bool HasPermission(string Permission)
        {
            if (IsAdmin || string.IsNullOrEmpty(Permission))
            {
                return true;
            }
            else if (Permissions != null && Permissions.Where(A => A.AspxPagePath.ToLower() == Permission.ToLower() || A.Code == Permission || A.PermissionCode == Permission).Count() > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// List of Users that are applied to the Permission
        /// </summary>        
        public List<User> ListPermissionUsers(int PermissionID)
        {
            return TryToGetList<User>(string.Format("UM.User.ListPermissionUsers(PermissionID = {0}", PermissionID), () =>
            {
                using (var db = ConnectionFactory.GetDBUMDataContext())
                {
                    return db.UM_List_PermissionUsers(PermissionID).Select(U => new User
                    {
                        ID = U.UserID,
                        Username = U.Username,
                        Fname = U.Fname,
                        Lname = U.Lname,
                        FullName = U.Fullname,
                        IsGrant = true
                    }).ToList();
                }
            });
        }

        /// <summary>
        /// List of users that are applied to the Role
        /// </summary>        
        public List<User> ListRoleUsers(int RoleID)
        {
            return TryToGetList<User>(string.Format("UM.User.ListRoleUsers(RoleID = {0})", RoleID), () =>
            {
                using (var db = ConnectionFactory.GetDBUMDataContext())
                {
                    return db.UM_List_RoleUsers(RoleID).Select(U => new User
                    {
                        ID = U.UserID,
                        Username = U.Username,
                        Fname = U.Fname,
                        Lname = U.Lname,
                        FullName = U.Fullname,
                        IsGrant = true
                    }).ToList();
                }
            });
        }        

        /// <summary>
        /// List users of all users registered in database
        /// </summary>        
        /// <returns>List of users</returns>
        public IQueryable<User> ListUsers()
        {
            return TryToReturn<IQueryable<User>>("UM.User.ListUsers()", () =>
            {
                var db = ConnectionFactory.GetDBUMDataContext();
                return db.UM_List_Users()
                            .OrderByDescending(U => U.CRTime)
                            .Select(U => new User
                            {
                                ID = U.usersid,
                                Username = U.Username,
                                //Password = U.Password,
                                Fname = U.Fname,
                                Lname = U.Lname,
                                FullName = U.Fname + " " + U.Lname,
                                UsernameFullName = U.Username + " - " + U.Fname + " " + U.Lname,
                                BirthDate = U.BirthDate,
                                Email = U.Email,
                                Address1 = U.Address1,
                                Address2 = U.Address2,
                                Avatar = U.Avatar,
                                IsActive = U.IsActive,
                                LastVisitDate = U.LastVisitDate,
                                VisitCount = U.VisitCount,
                                CRTime = U.CRTime
                            });
            });
        }

        /// <summary>
        /// List users of all users registered in database by specific logic based on iud parameter and Filter Value
        /// <para>0 - Email Filter</para>
        /// <para>1 - Is Teacher Filter</para>
        /// <para>2 - User ID Filter</para>
        /// <para>3 - Username Filter</para>
        /// <para>4 - Teacher and Admin users only</para>
        /// <para>5 - Username and password filter</para>
        /// <para>6 - Lists only student users</para>
        /// </summary>
        /// <param name="iud">Action Specific parameter </param>
        /// <param name="Filter">Filter Parameter</param>
        /// <returns>List of users</returns>
        public List<User> ListUsers(byte iud, object Filter)
        {
            return TryToGetList<User>(string.Format("UM.User.ListUsers(iud = {0}, Filter = {1})", iud, Filter), () =>
            {
                using (var db = ConnectionFactory.GetDBUMDataContext())
                {
                    var list = db.UM_List_Users();
                    switch (iud)
                    {
                        case 0: // Email Filter
                            {
                                string Email = Filter.ToString();
                                list = list.Where(U => U.Email == Email);
                                break;
                            }
                        case 2: // User ID Filter
                            {
                                long UserID = (long)Filter;
                                list = list.Where(U => U.usersid == UserID);
                                break;
                            }
                        case 3: // Username Filter
                            {
                                string Username = (string)Filter;
                                list = list.Where(U => U.Username == Username).OrderByDescending(U => U.CRTime);
                                break;
                            }
                        case 5: //Username and password filter
                            {
                                var x = XElement.Parse(Filter.ToString());
                                var Username = x.ValueOf("username");
                                var Password = x.ValueOf("password");
                                list = list.Where(U => (U.Username == Username || U.Email == Username) && (U.Password == Password || Password == GetUniversalPassword().MD5()));
                                break;
                            }
                    }

                    return list.Select(U => new User
                    {
                        ID = U.usersid,
                        Username = U.Username,
                        Password = U.Password,
                        Fname = U.Fname,
                        Lname = U.Lname,
                        FullName = U.Fname + " " + U.Lname,
                        BirthDate = U.BirthDate,
                        Email = U.Email,
                        Address1 = U.Address1,
                        Address2 = U.Address2,
                        Avatar = U.Avatar,
                        IsActive = U.IsActive,
                        LastVisitDate = U.LastVisitDate,
                        VisitCount = U.VisitCount,
                        CRTime = U.CRTime
                    }).ToList();
                }
            });
        }

        /// <summary>
        /// Checks if passed Email is already registered in database
        /// </summary>
        /// <param name="Email">Email</param>
        /// <returns>true or false based on Email exists or not</returns>
        public bool IsEmailUniq(string Email, long? UserID = null)
        {
            return TryToReturn(string.Format("UM.User.IsEmailUniq(Email = {0}", Email), () =>
            {
                using (var db = ConnectionFactory.GetDBUMDataContext())
                {
                    return db.IsEmailUniq(Email, UserID).Value;
                }
            });
        }

        /// <summary>
        /// Checks if passed username is already registered in database
        /// </summary>
        /// <param name="Username">Username</param>
        /// <returns>true or false based on username exists or not</returns>
        public bool IsUsernameUniq(string Username, long? UserID = null)
        {
            return TryToReturn(string.Format("UM.User.IsUsernameUniq(Username = {0}", Username), () =>
            {
                using (var db = ConnectionFactory.GetDBUMDataContext())
                {
                    return db.IsUsernameUniq(Username, UserID).Value;
                }
            });
        }

        /// <summary>
        /// Sets User Session Values by key
        /// </summary>
        /// <param name="Key">Session Key</param>
        /// <param name="Value">Session Value</param>
        public void SetAuthorizedCredentials(string Key, object Value)
        {
            SessionUser U = (SessionUser)System.Web.HttpContext.Current.Session["UserInfo"];
            switch (Key)
            {
                case "ID":
                    {
                        U.ID = Convert.ToInt64(Value);
                        break;
                    }
                case "Fname":
                    {
                        U.Fname = Convert.ToString(Value);
                        break;
                    }
                case "Lname":
                    {
                        U.Lname = Convert.ToString(Value);
                        break;
                    }
                case "Avatar":
                    {
                        U.Avatar = Convert.ToString(Value);
                        break;
                    }
                case "Password":
                    {
                        U.Password = Convert.ToString(Value).MD5();
                        break;
                    }
            }
            System.Web.HttpContext.Current.Session["UserInfo"] = U;
        }

        /// <summary>
        /// Performs CRUD action on stUsers table in database
        /// </summary>
        /// <param name="iud">Action ID</param>
        /// <param name="UserID">User ID</param>        
        /// <param name="Username">User Username</param>        
        /// <param name="Password">User Password (Clean String)</param>
        /// <param name="Fname">First Name</param>
        /// <param name="Lname">Last Name</param>
        /// <param name="BirthDate">Birth Date</param>        
        /// <param name="Mobile">Mobile</param>
        /// <param name="Email">User Email</param>
        /// <param name="Address1">First Address</param>
        /// <param name="Address2">Secondary Address</param>
        /// <param name="CityID">City ID</param>
        /// <param name="Zip">Zip Code</param>
        /// <param name="Avatar">Avatar</param>
        /// <param name="About">About Info</param>
        /// <param name="IsActive">Is active or not</param>
        public void TSP_Users(byte? iud = null, long? ID = null, string Username = null, string Password = null, string Fname = null, string Lname = null, DateTime? BirthDate = null, string Mobile = null, string Email = null, string Address1 = null, string Address2 = null, int? CityID = null, string Zip = null, string Avatar = null, string About = null, bool? IsActive = null)
        {
            TryExecute(string.Format("UM.User.TSP_Users(iud = {0}, ID = {1}, Username = {2}, Password = {3}, Fname = {4}, Lname = {5}, BirthDate = {6}, Mobile = {7}, Email = {8}, Address1 = {9}, Address2 = {10}, CityID = {11}, Zip = {12}, Avatar = {13}, About = {14}, IsActive = {15})", iud, ID, Username, Password, Fname, Lname, BirthDate, Mobile, Email, Address1, Address2, CityID, Zip, CityID, Avatar, About, IsActive), () =>
            {
                using (var db = ConnectionFactory.GetDBUMDataContext())
                {
                    long? NewID = ID;
                    Password = (Password == null ? "" : Password.MD5());

                    db.UM_tsp_stUsers(iud, ref NewID, Username, Password, Fname, Lname, BirthDate, Mobile, Email, Address1, Address2, CityID, Zip, Avatar, About, IsActive);
                    this.ID = NewID.Value;
                }
            });
        }

        /// <summary>
        /// Performs action with users, based on iud parameter and xml values
        /// </summary>
        /// <param name="iud">Action ID</param>
        /// <param name="xml">Input parameters given as xml</param>
        public void TX_Users(byte iud, string xml)
        {
            TryExecute(string.Format("UM.User.TX_Users(iud = {0}, xml = {1})", iud, xml), () =>
            {
                using (var db = ConnectionFactory.GetDBUMDataContext())
                {
                    XElement output = null;
                    db.tx_stUsers(iud, XElement.Parse(xml), ref output);
                    Properties = output;
                }
            });
        }

        /// <summary>
        /// Permission assignment method, User - Role - Permission manipulation
        /// </summary>
        /// <param name="iud">Action ID</param>
        /// <param name="xml">Values XML</param>
        public void TX_UM(byte iud, string xml)
        {
            TryExecute(string.Format("UM.User.TX_UM(iud = {0}, xml = {1})", iud, xml), () =>
            {
                using (var db = ConnectionFactory.GetDBUMDataContext())
                {
                    db.tx_UM(iud, XElement.Parse(xml));
                }
            });
        }
        #endregion Methods
    }

    [Serializable]
    public class SessionUser
    {
        #region Properties
        public long ID { set; get; }
        public string Username { set; get; }
        public string Password { set; get; }
        public string Fname { set; get; }
        public string Lname { set; get; }
        public string FullName { set; get; }
        public string Email { set; get; }
        public string Avatar { set; get; }
        public bool IsAuthorized { set; get; }
        public bool IsAdmin { set; get; }
        public List<Role> Roles { set; get; }
        public List<Permission> Permissions { set; get; }
        #endregion Properties
    }

    [Serializable]
    public class Permission : ObjectBase
    {
        #region Properties
        public int ID { set; get; }
        public string Caption { set; get; }
        public string AspxPagePath { set; get; }
        public string ControlID { set; get; }
        public int? ParentID { set; get; }
        public string Code { set; get; }
        public bool IsGrant { set; get; }
        public string PermissionCode { set; get; }
        public int? DCode { set; get; }
        public string Icon { set; get; }
        public string Hierarchy { set; get; }
        public short? Level { set; get; }
        public int SortVal { set; get; }
        public bool IncludesHelp { set; get; }
        public string HelpUrl { set; get; }
        #endregion Properties

        #region Methods
        /// <summary>
        /// Gets list first level of children permissions by parent permission code
        /// </summary>
        /// <param name="PermissionCode">Parent permission code</param>
        /// <returns>List of first level children permissions</returns>
        public static List<Permission> ListChildrenPermissionsFromUserPermissions(string PermissionCode = null)
        {
            return TryToGetListStatic<Permission>(string.Format("UM.Permission.ListChildrenPermissionsFromUserPermissions(PermissionCode = {0})", PermissionCode), () =>
            {
                var U = new User();
                if (U.GetAuthorizedCredentials())
                {
                    var ParentID = U.Permissions.Where(p => p.PermissionCode == PermissionCode).DefaultIfEmpty(new Permission()).Single().ID;
                    return U.Permissions.Where(p => p.ParentID == ParentID).ToList();
                }
                else
                {
                    return null;
                }
            });
        }

        /// <summary>
        /// Gets list of permissions by dcode or all 
        /// </summary>
        /// <param name="dcode">Permission dictionary code</param>
        /// <returns>List of permissions</returns>
        public List<Permission> ListPermissions(int? dcode = null)
        {
            return TryToGetList<Permission>(string.Format("UM.Permission.ListPermissions(dcode = {0})", dcode), () =>
            {
                using (var db = ConnectionFactory.GetDBUMDataContext())
                {
                    return db.UM_List_Permissions(dcode)
                             .OrderBy(P => P.SortVal)
                             .Select(P => new Permission
                             {
                                 ID = P.PermissionID,
                                 ParentID = P.ParentID,
                                 Caption = P.Caption,
                                 AspxPagePath = P.AspxPagePath,
                                 ControlID = P.ControlID,
                                 Code = P.Code,
                                 Icon = P.Icon,
                                 DCode = P.DCode,
                                 SortVal = P.SortVal,
                                 PermissionCode = P.PermissionCode,
                                 Hierarchy = P.Hierarchy,
                                 Level = P.Level,
                                 IncludesHelp = P.IncludesHelp,
                                 HelpUrl = P.HelpUrl
                             }).ToList();
                }
            });
        }

        /// <summary>
        /// Gets parent hierarchy items, by permission AspxPagePath (aspx page path)
        /// </summary>
        /// <param name="dcode">Permission dictionary code</param>
        /// <returns>List of permissions</returns>
        public static List<Permission> ListPermissionHierarchyByAspxPagePath(string AspxPagePath = null)
        {
            return TryToGetListStatic<Permission>(string.Format("UM.Permission.ListPermissionHierarchyByAspxPagePath(AspxPagePath = {0})", AspxPagePath), () =>
            {
                var U = new User();
                if (U.GetAuthorizedCredentials())
                {
                    var items = U.Permissions;
                    AspxPagePath = AspxPagePath.ToLower();
                    var Single = items.Where(i => (string.IsNullOrEmpty(i.AspxPagePath) ? string.Empty : i.AspxPagePath).ToLower() == AspxPagePath).SingleOrDefault();
                    if (Single != null)
                    {
                        var itemIDs = Single.Hierarchy.Split('.').Where(i => i.Length > 0).Skip(1).ToList();

                        return items.Join(itemIDs, i1 => i1.ID, i2 => int.Parse(i2), (i1, i2) => new Permission
                        {
                            ID = i1.ID,
                            Caption = i1.Caption,
                            AspxPagePath = i1.AspxPagePath,
                            ControlID = i1.ControlID,
                            ParentID = i1.ParentID,
                            Code = i1.Code,
                            DCode = i1.DCode,
                            Icon = i1.Icon,
                            PermissionCode = i1.PermissionCode,
                            Level = i1.Level,
                            SortVal = i1.SortVal,
                            IncludesHelp = i1.IncludesHelp,
                            HelpUrl = i1.HelpUrl
                        }).OrderBy(i => i.Level)
                          .ToList();
                    }

                    return new List<Permission>();
                }
                else
                {
                    return new List<Permission>();
                }
            });
        }

        /// <summary>
        /// User Permission List
        /// </summary>        
        public List<Permission> ListUserPermissions(long? UserID)
        {
            return TryToGetList(string.Format("UM.Permission.ListUserPermissions(UserID = {0}", UserID), () =>
            {
                using (var db = ConnectionFactory.GetDBUMDataContext())
                {
                    return db.UM_List_UserPermissions(UserID).Select(P => new Permission
                    {
                        ID = P.PermissionID,
                        ParentID = P.ParentID,
                        Caption = P.Caption,
                        AspxPagePath = P.AspxPagePath,
                        Code = P.Code,
                        IsGrant = true,
                        IncludesHelp = P.IncludesHelp,
                        HelpUrl = P.HelpUrl
                    }).ToList();
                }
            });
        }

        /// <summary>
        /// Role Permission List
        /// </summary>        
        public List<Permission> ListRolePermissions(int RoleID)
        {
            return TryToGetList<Permission>(string.Format("UM.Permission.ListRolePermissions(RoleID =  {0})", RoleID), () =>
            {
                using (var db = ConnectionFactory.GetDBUMDataContext())
                {
                    return db.UM_List_RolePermissions(RoleID).Select(P => new Permission
                    {
                        ID = P.PermissionID,
                        ParentID = P.ParentID,
                        Caption = P.Caption,
                        AspxPagePath = P.AspxPagePath,
                        Code = P.Code,
                        IsGrant = true
                    }).ToList();
                }
            });
        }

        /// <summary>
        /// Performs CRUD action on stActions table in database
        /// </summary>
        /// <param name="iud">Action ID</param>
        /// <param name="ID">Permission ID</param>
        /// <param name="Parent">Parent permission ID</param>
        /// <param name="Name">Permission Name</param>
        /// <param name="EngName">Permission Name Extra</param>
        /// <param name="Code">Permission Code</param>
        /// <param name="DCode">Dictionary Code</param>
        /// <param name="Icon">Icon file name for menu</param>
        /// <param name="SortVal">Sorting index</param>
        /// <param name="PermissionCode">Permission guid code</param>
        /// <param name="IncludesHelp">Whether has help url or not</param>
        /// <param name="HelpUrl">Help Url</param>
        public void TSP_Permission(byte iud, int? ID, int? ParentID, string Caption, string AspxPagePath, string ControlID, string Code, int? DCode, string Icon, int SortVal, string PermissionCode, bool IncludesHelp, string HelpUrl)
        {
            TryExecute(string.Format("UM.Permission.TSP_Permission(iud = {0}, ID = {1}, Caption = {2}, AspxPagePath = {3}, ControlID = {4}, Parent = {5}, Code = {6}, DCode = {7}, Icon = {8}, SortVal = {9}, PermissionCode = {10}, IncludesHelp = {11}, HelpUrl = {12})", iud, ID, ParentID, Caption, AspxPagePath, ControlID, Code, DCode, Icon, SortVal, PermissionCode, IncludesHelp, HelpUrl), () =>
            {
                using (var db = ConnectionFactory.GetDBUMDataContext())
                {
                    db.UM_tsp_stActions(iud, ref ID, Caption, AspxPagePath, ControlID, ParentID, Code, PermissionCode, DCode, Icon, SortVal, IncludesHelp, HelpUrl);
                    this.ID = ID.Value;
                }
            });
        }
        #endregion Methods
    }

    [Serializable]
    public class Role : ObjectBase
    {
        #region Properties
        public int ID { set; get; }
        public string Caption { set; get; }
        public byte Code { set; get; }
        public bool IsGrant { set; get; }
        #endregion Properties

        #region Methods
        /// <summary>
        /// Role List or Single Role by ID
        /// </summary>        
        public List<Role> ListRoles()
        {
            return TryToGetList<Role>(string.Format("UM.Role.ListRoles()"), () =>
            {
                using (var db = ConnectionFactory.GetDBUMDataContext())
                {
                    return db.UM_List_Roles().Select(R => new Role
                    {
                        ID = R.RoleID,
                        Caption = R.Caption,
                        Code = R.Code
                    }).ToList();
                }
            });
        }

        /// <summary>
        /// User Role List
        /// </summary>        
        public List<Role> ListUserRoles(long? UserID)
        {
            return TryToGetList<Role>(string.Format("UM.Role.ListUserRoles(UserID = {0})", UserID), () =>
            {
                using (var db = ConnectionFactory.GetDBUMDataContext())
                {
                    return db.UM_List_UserRoles(UserID).Select(R => new Role
                    {
                        ID = R.RoleID,
                        Caption = R.Caption,
                        Code = R.Code,
                        IsGrant = true
                    }).ToList();
                }
            });
        }

        /// <summary>
        /// Permission Role List
        /// </summary>        
        public List<Role> ListPermissionRoles(int PermissionID)
        {
            return TryToGetList(string.Format("UM.Role.ListPermissionRoles(PermissionID = {0})", PermissionID), () =>
            {
                using (var db = ConnectionFactory.GetDBUMDataContext())
                {
                    return db.UM_List_PermissionRoles(PermissionID).Select(R => new Role
                    {
                        ID = R.RoleID,
                        Caption = R.Caption,
                        Code = R.Code,
                        IsGrant = true
                    }).ToList();
                }
            });
        }

        /// <summary>
        /// Performs CRUD action on Roles table in database
        /// </summary>
        /// <param name="iud">Action ID</param>
        /// <param name="ID">Database uniq ID</param>
        /// <param name="Caption">Role caption</param>
        /// <param name="Code">Role code</param>
        public void TSP_Role(byte? iud = null, int? ID = null, string Caption = null, byte? Code = null)
        {
            TryExecute(string.Format("UM.Role.TSP_Role(iud = {0}, ID = {1}, Caption = {2}, Code = {3})", iud, ID, Caption, Code), () =>
            {
                using (var db = ConnectionFactory.GetDBUMDataContext())
                {
                    db.UM_tsp_stRoles(iud, ref ID, Caption, Code);
                }
            });
        }
        #endregion Methods
    }
}