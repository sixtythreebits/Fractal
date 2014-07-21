using System;
using System.Collections.Generic;
using System.Linq;
using SystemBase;
using Lib;
using DB;
using System.Xml.Linq;
using Core.Utilities;

namespace Core
{
    public class Asset : ObjectBase
    {
        #region Properties
        public long? RecordID { set; get; }
        public long? ID { set; get; }
        public long? ParentID { set; get; }
        public string Caption { set; get; }
        public string Description { set; get; }
        public int Position { set; get; }
        public int? TypeID { set; get; }
        public string TypeCode { set; get; }
        public string Type { set; get; }
        public int? SubjectID { set; get; }
        public string Subject { set; get; }
        public int? CourseSectionAssetTypeID { set; get; }
        public string CourseSectionAssetTypeCode { set; get; }
        public string CourseSectionAssetType { set; get; }
        public string Author { set; get; }
        public string Icon { set; get; }
        public bool IsPublished { set; get; }
        public bool IsForSale { set; get; }
        public decimal Price { set; get; }
        public string unid { set; get; }
        public bool? IsHiddenFromLibrary { get; set; }
        public int? NumberOfUse { set; get; }

        public long? FileID { set; get; }
        public string FileName { set; get; }
        public long? Size { set; get; }
        public int? Duration { set; get; }
        public string ContentType { set; get; }

        public XElement Properties { set; get; }
        #endregion Properties

        #region Constructors
        public Asset() { }

        public Asset(long? ID)
        {
            GetSingleAsset(ID, false);
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Completely removes asset if it has no connections, to more than one user, otherwise removes only connection
        /// </summary>
        /// <param name="AssetID">Asset ID</param>
        public void DeleteAsset(long? AssetID, bool ForceDelete = false)
        {
            TryExecute(string.Format("Core.Asset.DeleteAsset(AssetID = {0})", AssetID), delegate()
            {
                GetSingleAsset(AssetID);
                Utility.GetUploadFileLocations().ForEach(Path =>
                {
                    if (System.IO.File.Exists(Path + FileName))
                    {
                        System.IO.File.Delete(Path + FileName);
                    }
                });
                TX_Assets(4, string.Format("<data><asset_id>{0}</asset_id></data>", AssetID));

            });
        }

        /// <summary>
        /// Returns all general information, markers, subtitles and statistics of asset in XML format
        /// </summary>
        /// <param name="ID">Requested Asset ID</param>
        public void GetSingleAsset(long? ID, bool IncludeStatistics = false)
        {
            TryExecute(string.Format("Core.Asset.GetSingleAsset(ID = {0}, IncludeStatistics = {1})", ID, IncludeStatistics), () =>
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    var x = Properties = db.GetSingleAsset(ID, IncludeStatistics);
                    if (x == null)
                    {
                        IsError = true;
                        ErrorMessage = "Not Found";
                    }
                    else
                    {
                        this.ID = ID;
                        this.Caption = x.Element("caption").Value;
                        this.Description = x.Element("description").Value;
                        this.Icon = x.Element("icon").Value;
                        this.TypeID = int.Parse(x.Element("type_id").Value);
                        this.TypeCode = x.Element("type_code").Value;
                        this.Type = x.Element("type").Value;
                        this.SubjectID = int.Parse(x.Element("subject_id").Value);
                        this.Subject = x.Element("subject").Value;
                        this.Subject = x.Element("subject").Value;
                        this.Author = x.Element("author").Value;
                        this.Price = decimal.Parse(x.Element("price").Value);
                        this.IsForSale = x.Element("for_sale").Value == "1";
                        this.CRTime = DateTime.Parse(x.Element("crtime").Value);
                        this.IsHiddenFromLibrary = x.Element("is_hidden_from_library").Value == "1";
                        this.FileID = long.Parse(x.Element("file_id").Value);
                        this.FileName = x.Element("fullname").Value;
                        this.ContentType = x.Element("content_type").Value;
                        this.Duration = int.Parse(x.Element("duration").Value);
                        this.unid = x.Element("unid").Value;
                    }
                }
            });
        }

        /// <summary>
        /// Performs some action with Assets and some related tables, based on iud parameter and xml values
        /// </summary>
        /// <param name="iud">Action ID</param>
        /// <param name="xml">Input parameters given as xml</param>
        public void TX_Assets(byte? iud = null, string xml = null)
        {
            TryExecute(string.Format("Core.Course.tx_Assets(iud = {0}, xml = {1})", iud, xml), delegate()
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    XElement output = null;
                    db.tx_Assets(iud, XElement.Parse(xml), ref output);
                    Properties = output;
                }
            });
        }
        #endregion Methods
    }
}
