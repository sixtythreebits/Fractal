using System;
using System.Collections.Generic;
using System.Linq;
using SystemBase;
using DB;
using Lib;

namespace Core
{
    public class Security : ObjectBase
    {
        public static bool IsUserAllowedToViewAsset(long? UserID, long? AssetID)
        {
            return TryToReturnStatic<bool>(string.Format("Core.Security.IsUserAllowedToViewAsset(UserID = {0}, AssetID = {1})", UserID, AssetID), () =>
            {
                using (var db = ConnectionFactory.GetDBSecurityDataContext())
                {
                    return db.sec_IsUserAllowedToViewAsset(UserID, AssetID).Value;
                }
            });
        }

        public static bool IsUserAllowedToViewCourse(long? UserID, long? CourseID)
        {
            return TryToReturnStatic<bool>(string.Format("Core.Security.IsUserAllowedToViewCourse(UserID = {0}, CourseID = {1})", UserID, CourseID), () =>
            {
                using (var db = ConnectionFactory.GetDBSecurityDataContext())
                {
                    return db.sec_IsUserAllowedToViewCourse(UserID, CourseID).Value;
                }
            });
        }
    }
}
