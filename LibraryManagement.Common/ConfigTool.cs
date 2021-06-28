using System;

namespace LibraryManagement.Common
{
    public class ConfigTool
    {
        /// <summary>
        /// 取得DB連線字串
        /// </summary>
        /// <param name="connStr"></param>
        /// <returns></returns>
        public static string GetDBConnectionString(String connStr)
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings[connStr].ConnectionString.ToString();
        }
    }
}
