using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using SystemBase;
using DB;
using Lib;
using System.Xml.Serialization;
using System.Web;
using System.Text.RegularExpressions;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using System.Configuration;


namespace Core.Utilities
{
    public class Any : ObjectBase
    {
        #region Properties
        public int? ID { set; get; }
        public string Name { set; get; }
        public string NameE { set; get; }
        public string HCode { set; get; }
        public short? DCode { set; get; }
        public decimal? VM { set; get; }
        public int? Parent { set; get; }
        public int? SortVal { set; get; }
        public string img1 { set; get; }
        public string img2 { set; get; }
        public string img3 { set; get; }
        public string img4 { set; get; }
        public string img5 { set; get; }
        public string img6 { set; get; }
        public int? IntCode { set; get; }
        #endregion Properties

        #region Methods
        /// <summary>
        /// Performs action based on iud parameter with database dtAny Object
        /// </summary>
        /// <param name="iud">Action ID</param>
        /// <param name="ID">Database uniq ID</param>        
        /// <param name="Parent">Parent record ID</param>
        /// <param name="Name">Name</param>
        /// <param name="NameE">Additional name</param>
        /// <param name="HCode">Some code value</param>
        /// <param name="DCode">Dictionary code</param>
        /// <param name="IsDefault">If comes first in list</param>
        /// <param name="VM">Some decimal value</param>
        /// <param name="SortVal">Sorting index</param>
        /// <param name="img1">Image 1</param>
        /// <param name="img2">Image 2</param>
        /// <param name="img3">Image 3</param>
        /// <param name="img4">Image 4</param>
        /// <param name="img5">Image 5</param>
        /// <param name="img6">Image 6</param>
        /// <param name="IntCode">Integer Code</param>
        public void Action(byte? iud = null, int? ID = null, int Parent = 0, string Name = null, string NameE = null, string HCode = null, short? DCode = null, bool? IsDefault = null, decimal? VM = null, int? SortVal = null, string img1 = null, string img2 = null, string img3 = null, string img4 = null, string img5 = null, string img6 = null, int? IntCode = null)
        {
            TryExecute(string.Format("Dictionary.Any.Action(iud = {0}, ID = {1}, Parent = {2}, Name = {3}, NameE = {4}, HCode = {5}, DCode = {6}, IsDefault = {7}, VM = {8}, SortVal = {9}, img1 = {10}, img2 = {11}, img3 = {12}, img4 = {13}, img5 = {14}, img6 = {15}, IntCode = {16})", iud, ID, Parent, Name, NameE, HCode, DCode, IsDefault, VM, SortVal, img1, img2, img3, img4, img5, img6, IntCode), () =>
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    int? NewID = ID;
                    db.tsp_dtAny(iud, ref NewID, Parent, Name, NameE, HCode, DCode, IsDefault, VM, SortVal, img1, img2, img3, img4, img5, img6, IntCode);
                    this.ID = NewID;
                }
            });
        }

        /// <summary>
        /// Gets List of dictionary values by Dictionary Code and Hierarchy Level
        /// <para>1 - FileTypes</para>
        /// <para>2 - Course Subscription Types</para>
        /// <para>3 - Semesters</para>
        /// <para>4 - User Actions</para>
        /// <para>5 - States</para>
        /// <para>6 - Course - Asset Subjects</para>
        /// <para>7 - Course Section Asset Types</para>
        /// <para>8 - Section Types</para>
        /// <para>9 - Tracking Items</para>
        /// <para>10 - Invoice Types</para>
        /// <para>11 - Credit Card Types</para>
        /// <para>12 - Quiz Status</para>
        /// </summary>
        /// <param name="level">Level No</param>
        /// <param name="dcode">Dictionary Code</param>
        /// <returns>List of dictionary items</returns>
        public List<Any> ListAny(int? level = 1, int? dcode = 1)
        {
            return TryToGetList(string.Format("Any.ListAny(level = {0}, dcode = {1})", level, dcode), delegate()
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    return db.List_dtAny(level, dcode)
                             .OrderByDescending(a => a.defval)
                             .ThenBy(a => a.SortVal)
                             .ThenBy(a => a.hname)
                             .Select(a => new Any
                             {
                                 ID = a.codeid,
                                 Name = a.hname,
                                 NameE = a.hnamee,
                                 HCode = a.hcode,
                                 DCode = a.dcode,
                                 VM = a.vm1,
                                 Parent = a.mgrid,
                                 SortVal = a.SortVal,
                                 img1 = a.img1,
                                 img2 = a.img2,
                                 img3 = a.img3,
                                 img4 = a.img4,
                                 img5 = a.img5,
                                 img6 = a.img6,
                                 IntCode = a.IntCode
                             }).ToList();
                }
            });
        }

        /// <summary>
        /// Gets list of dictionary items filtered by "iud" option and "Filter" parameters. Each iud option what type of filter will it recieve
        /// <para>0 - List of video extensions</para>
        /// <para>1 - List children items by parent ID</para>
        /// <para>2 - List of items by level and dcode. Filters list by search keyword. Search is performed in item "Name" property</para>
        /// </summary>
        /// <param name="iud">Option parameter</param>
        /// <param name="Filter">Filter object</param>
        /// <returns>Dictionary Items</returns>
        public List<Any> ListAnyCustom(byte iud, params object[] Filter)
        {
            return TryToGetList<Any>(string.Format("General.Any.ListAny(iud = {0}, Filter = {1})", iud, Filter), delegate()
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    var list = db.List_dtAny(null, null);

                    switch (iud)
                    {
                        case 0: // Video extension list
                            {
                                list = list.Where(a => a.hcode.Contains("video"));
                                break;
                            }
                        case 1: // List children by parent
                            {
                                int? Parent = (int?)Filter[0];
                                list = list.Where(a => a.mgrid == Parent);
                                break;
                            }
                        case 2:
                            {
                                list = db.List_dtAny((int?)Filter[0], (int?)Filter[1]).Where(a => a.hname.Contains((string)Filter[2]));
                                break;
                            }
                    }

                    return list.OrderBy(a => a.SortVal)
                    .Select(a => new Any
                    {
                        ID = a.codeid,
                        Name = a.hname,
                        NameE = a.hnamee,
                        HCode = a.hcode,
                        DCode = a.dcode,
                        VM = a.vm1,
                        Parent = a.mgrid,
                        SortVal = a.SortVal,
                        img1 = a.img1,
                        img2 = a.img2,
                        img3 = a.img3,
                        img4 = a.img4,
                        img5 = a.img5,
                        img6 = a.img6
                    }).ToList();
                }
            });
        }

        /// <summary>
        /// List dictionary items by specific filter
        /// <para> iud = 0 - gets list of video extensions</para>
        /// </summary>
        /// <param name="iud"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<Any> ListDisctionaryCustomFiltered(byte? iud = null, object filter = null)
        {
            return TryToGetList(string.Format("Any.ListAny(iud = {0}, filter = {1})", iud, filter), delegate()
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    var list = db.List_dtAny(null, null);
                    switch (iud)
                    {
                        case 0:
                            {
                                list = list.Where(d => d.dcode == 1 &&
                                                  d.hrclvl == 2 &&
                                                  (d.hcode.Contains("video")) || d.hcode.Contains("image"));
                                break;
                            }
                    }


                    return list.OrderBy(a => a.SortVal)
                             .Select(a => new Any
                             {
                                 ID = a.codeid,
                                 Name = a.hname,
                                 NameE = a.hnamee,
                                 HCode = a.hcode,
                                 DCode = a.dcode,
                                 VM = a.vm1,
                                 Parent = a.mgrid,
                                 SortVal = a.SortVal,
                                 img1 = a.img1,
                                 img2 = a.img2,
                                 img3 = a.img3,
                                 img4 = a.img4,
                                 img5 = a.img5,
                                 img6 = a.img6
                             }).ToList();
                }
            });
        }
        #endregion Methods
    }
    public class AppSettings
    {
        #region Properties
        public static string ApiUrl
        {
            get { return ConfigurationManager.AppSettings["ApiUrl"]; }
        }

        public static string AssetFolderPhysicalPath
        {
            get { return ConfigurationManager.AppSettings["AssetFolder"]; }
        }

        public static string GeneralCoverImageHttpPath
        {
            get { return ImageFolderHttpPath + "video.jpg"; }
        }

        public static string HtmlTemplateFolderPhysicalPath
        {
            get { return AppDomain.CurrentDomain.BaseDirectory + "TPL\\"; }
        }

        public static bool IsAllowedGoogleAnalytics
        {
            get { return ConfigurationManager.AppSettings["GoogleAnalyticsAllowed"].ToBoolean() == true; }
        }

        public static string ImageFolderHttpPath
        {
            get { return "/images/"; }
        }

        public static string ImageFolderPhysicalPath
        {
            get { return AppDomain.CurrentDomain.BaseDirectory + "images\\"; }
        }

        public static string NoUserAvatarHttpPath
        {
            get { return ImageFolderHttpPath + "no-avatar.jpg"; }
        }

        public static string NoCourseIconHttpPath
        {
            get { return "/images/0/book.png"; }
        }

        public static string PluginIconImagesFolderHttpPath
        {
            get { return "/plugins/icons/img/"; }
        }

        public static string UploadFolderHttpPath
        {
            get { return "/upload/"; }
        }

        public static string UploadFolderPhysicalPath
        {
            get { return ConfigurationManager.AppSettings["UploadFolderPhysicalPath"]; }
        }

        public static string WebsiteHttpFullPath
        {
            get { return Utility.GetRootUrl(); }
        }
        #endregion Properties
    }

    [XmlRoot("data")]
    public class PreviewImageData
    {
        #region Properties
        [XmlElement("asset_id")]
        public long? AssetID { set; get; }

        [XmlElement("filename")]
        public string Filename { set; get; }

        /// <summary>
        /// FitMode values can be: "max", "none", "pad", "stretch". Default value is crop.
        /// </summary>
        [XmlElement("fit_mode")]
        public string FitMode { set; get; }

        [XmlElement("height", IsNullable = true)]
        public int? Height { set; get; }

        [XmlElement("width", IsNullable = true)]
        public int? Width { set; get; }
        #endregion Properties

        #region Methods
        public string ToEncripted()
        {
            return this.ToXml().EncryptWeb();
        }

        public bool ShouldSerializeAssetID()
        {
            return AssetID.HasValue;
        }

        public bool ShouldSerializeHeight()
        {
            return Height.HasValue;
        }

        public bool ShouldSerializeWidth()
        {
            return Width.HasValue;
        }
        #endregion Methods
    }

    public class Utility : ObjectBase
    {
        #region Methods
        /// <summary>
        /// Gets asset types for css class and other use as dictionary
        /// </summary>
        /// <returns>Dictionaried asset types</returns>
        public static Dictionary<string, string> GetAssetTypeDictionary()
        {
            var AssetTypes = new Dictionary<string, string>();
            AssetTypes["1"] = "video";
            AssetTypes["2"] = "image";
            AssetTypes["3"] = "doc";
            AssetTypes["4"] = "other";
            return AssetTypes;
        }
 

        /// <summary>
        /// Gets course subscription state
        /// </summary>
        /// <para>If price is more that 0 returns price (for ex. $200)</para>
        /// <para>If price is zero returns "Free"</para>                
        /// <param name="Price"></param>
        /// <returns>String that displays price on the webpage</returns>
        public static string GetCoursePriceForPage(decimal? Price)
        {
            return Price > 0 ? string.Format("{0:n2} USD", Price) : "Free";
        }

        /// <summary>
        /// Gets all supported image extensions for streaming tutors
        /// </summary>
        /// <returns>List of extensions</returns>
        public static List<string> GetImageExtensions()
        {
            return (new string[] { ".jpg", ".jpeg", ".gif", ".png" }).ToList();
        }

        /// <summary>
        /// Gets correct height for image to resize when given width parameter only
        /// </summary>
        /// <param name="OriginalWidth">Image original width</param>
        /// <param name="OriginalHeight">Image original height</param>
        /// <param name="Width">Image new width</param>
        /// <returns>Image new height</returns>
        public static int GetImageHeightForWidth(int OriginalWidth, int OriginalHeight, int Width)
        {
            if (OriginalWidth > Width)
            {
                return (Width * OriginalHeight) / OriginalWidth;
            }
            else
            {
                return OriginalHeight;
            }
        }

        /// <summary>
        /// Converts given file size based on iud parameter. (iud = 0 from bytes to mega bytes ...)
        /// <para>0 - From bytes to Mega bytes</para>
        /// </summary>
        /// <param name="iud">Action ID</param>
        /// <param name="Size">input size</param>
        /// <returns></returns>
        public static string GetFileSizeConverted(byte iud, object Size)
        {

            switch (iud)
            {
                //From bytes to Mega bytes
                case 0:
                    {
                        var size = Convert.ToDouble(Size);
                        if (size > 1048576)
                        {
                            return Math.Round(size / 1048576, 1) + "&nbsp;MB";
                        }
                        else
                        {
                            return Math.Round(size / 1024, 0) + "&nbsp;KB";
                        }
                    }
                default:
                    {
                        return string.Empty;
                    }
            }

        }

        public static string GetRootUrl()
        {
            var Request = HttpContext.Current.Request;
            var Url = Request.Url;

            string url = Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath;
            return url += url.EndsWith("/") ? string.Empty : "/";
        }        

        /// <summary>
        /// Takes ID parameter and returs "[ID]_[Guid]" encripted string
        /// </summary>
        /// <param name="ID">Input ID parameter</param>
        /// <returns>Encoded string</returns>
        public static string GetSimpleEncodedID(object ID)
        {
            return string.Format("{0}_{1}", ID, Guid.NewGuid().ToString().Substring(0, 6)).EncryptWeb();
        }

        /// <summary>
        /// Takes simple encoded ID string and returs decoded ID
        /// </summary>
        /// <param name="ID">Encoded ID</param>
        /// <returns>Decoded ID</returns>
        public static string GetSimpleDecodedID(string ID)
        {
            return ID.DecryptWeb().Split('_')[0];
        }
       
        /// <summary>
        /// Gets total secons from time format 02:45:11
        /// </summary>
        /// <param name="time">Time</param>
        /// <returns>seconds</returns>
        public static string GetTotalSecondsFromTime(string time)
        {
            var part = time.Split(':');
            return (int.Parse(part[0]) * 3600 + int.Parse(part[1]) * 60 + int.Parse(part[2])).ToString();
        }

        public static string GetTplContent(string TplPath, string[] Keys, string[] Values)
        {
            string s = System.IO.File.ReadAllText(TplPath);
            int l = Keys.Length;
            for (int i = 0; i < l; i++)
            {
                s = s.Replace(Keys[i], Values[i]);
            }
            return s;
        }

        public static List<string> GetUploadFileLocations()
        {
            return new string[]
            {
                AppSettings.AssetFolderPhysicalPath,
                AppSettings.UploadFolderPhysicalPath
            }.ToList();
        }

        /// <summary>
        /// Get User IP Address
        /// </summary>
        /// <returns>IP Address string</returns>
        public static string GetUserIP()
        {
            if (HttpContext.Current == null)
            {
                return null;
            }
            else
            {
                var Request = HttpContext.Current.Request;
                if (Request.UserHostAddress == "127.0.0.1" || Request.UserHostAddress.StartsWith("192.168.") || Request.UserHostAddress.StartsWith("10.0.") || !System.Text.RegularExpressions.Regex.IsMatch(Request.UserHostAddress, @"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$"))
                {
                    //return "94.240.222.73";
                    //return "67.81.50.1";
                    return "213.157.215.228";
                }
                else
                {
                    return Request.UserHostAddress;
                }
            }
        }

        /// <summary>
        /// Converts int indexes to sequence of characters
        /// </summary>
        /// <param name="index">Index to convert</param>
        /// <returns>Character, related to index</returns>
        public static string IndexToChar(int index)
        {
            return index > 26 ?
            Convert.ToChar(64 + (index / 26)).ToString() + Convert.ToChar(64 + (index % 26)) :
            Convert.ToChar(64 + index).ToString();
        }

        /// <summary>
        /// Converts Total seconds to friendly format
        /// <para>0 - 5h 10m 20s</para>
        /// <para>1 - hh:mm:ss</para>        
        /// </summary>
        /// <param name="iud">Option parameter</param>
        /// <param name="TotalSeconds">Total number of seconds</param>
        /// <returns>Total seconds converted to friendly view</returns>
        public static string SecondsToHMS(byte iud = 0, int? TotalSeconds = null)
        {
            if (TotalSeconds > 0)
            {
                var T = TimeSpan.FromSeconds(TotalSeconds.Value);
                var sb = new StringBuilder();

                switch (iud)
                {
                    case 0:
                        {
                            if (T.Hours > 0 || T.Days > 0)
                            {
                                sb.AppendFormat("{0}h", T.Hours + T.Days * 24);
                                if (T.Minutes > 0)
                                {
                                    sb.AppendFormat(" {0}min", T.Minutes);
                                }
                            }
                            else
                            {
                                if (T.Minutes > 0)
                                {
                                    sb.AppendFormat("{0}min", T.Minutes);
                                    if (T.Seconds > 0)
                                    {
                                        sb.AppendFormat(" {0}s", T.Seconds);
                                    }
                                }
                                else
                                {
                                    if (T.Seconds > 0)
                                    {
                                        sb.AppendFormat(" {0}s", T.Seconds);
                                    }
                                }
                            }
                            break;
                        }
                    case 1:
                        {
                            sb.Append(T.ToString("hh\\:mm\\:ss"));
                            break;
                        }
                }

                return sb.ToString();
            }
            else
            {
                switch (iud)
                {
                    case 0:
                        {
                            return string.Empty;
                        }

                    default:
                        {
                            return "&mdash;";
                        }
                }
            }
        }
        #endregion Methods
    }


    public static class Extensions
    {
        public static string ToJson(this object Value, Formatting Formatting = Formatting.None)
        {
            return JsonConvert.SerializeObject(Value, Formatting);
        }
    }
}
