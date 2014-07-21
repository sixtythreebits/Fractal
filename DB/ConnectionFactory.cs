using System;
using System.Configuration;

namespace DB
{
    public class ConnectionFactory
    {
        public static string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString; }
        }

        public static DBCoreDataContext GetDBCoreDataContext()
        {
            return new DBCoreDataContext(ConnectionString);
        }

        public static DBSecurityDataContext GetDBSecurityDataContext()
        {
            return new DBSecurityDataContext(ConnectionString);
        }       

        public static DBUMDataContext GetDBUMDataContext()
        {
            return new DBUMDataContext(ConnectionString);
        }       
    }
}
