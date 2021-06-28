using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace LibraryManagement.DAL
{
    public class DropDownDAL : IDropDownDAL
    {
        /// <summary>
        /// 取得DB連線字串
        /// </summary>
        /// <returns></returns>
        private string GetDBConnectionString()
        {
            return Common.ConfigTool.GetDBConnectionString("DBConn");
        }

        /// <summary>
        /// 取得書籍種類下拉式選單
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetBookClassDropDown()
        {
            string sql = @"SELECT CLASS_ID AS DropDownId, CLASS_NAME AS DropDownText
                           FROM dbo.BOOK_CLASS";

            return QueryDropDownData(sql);
        }

        /// <summary>
        /// 取得書籍狀態下拉式選單
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetBookStatusDropDown()
        {
            string sql = @"SELECT STATUS_ID AS DropDownId, STATUS_NAME AS DropDownText
                           FROM dbo.BOOK_STATUS";

            return QueryDropDownData(sql);
        }

        /// <summary>
        /// 取得館藏地下拉式選單
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetStoreRoomDropDown()
        {
            string sql = @"SELECT STOREROOM_ID AS DropDownId, STOREROOM_NAME AS DropDownText
                           FROM dbo.BOOK_STOREROOM";

            return QueryDropDownData(sql);
        }

        /// <summary>
        /// 取得書名下拉式選單
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetBookNameDropDown()
        {
            string sql = @"SELECT BOOK_ID AS DropDownId, BOOK_NAME AS DropDownText
                           FROM dbo.BOOK_DATA";

            return QueryDropDownData(sql);
        }

        /// <summary>
        /// 取得借閱人下拉式選單
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetBorrowerDropDown()
        {
            string sql = @"SELECT MEMBER_ID AS DropDownId, MEMBER_NAME AS DropDownText
                           FROM dbo.MEMBER";

            return QueryDropDownData(sql);
        }

        /// <summary>
        /// 查詢DB
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private List<SelectListItem> QueryDropDownData(string sql)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);

                sqlAdapter.Fill(dataTable);
                conn.Close();
            }
            return this.MapDropDownData(dataTable);
        }

        /// <summary>
        /// 組合下拉式選單資料
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        private List<SelectListItem> MapDropDownData(DataTable dataTable)
        {
            List<SelectListItem> tmpDropDownList = new List<SelectListItem>();
            foreach (DataRow row in dataTable.Rows)
            {
                tmpDropDownList.Add(new SelectListItem
                {
                    Value = row["DropDownId"].ToString(),
                    Text = row["DropDownText"].ToString()
                });
            }
            return tmpDropDownList;
        }
    }
}
