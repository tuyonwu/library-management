using LibraryManagement.Model;
using LibraryManagement.Model.AddBook;
using LibraryManagement.Model.BookDetail;
using LibraryManagement.Model.BookRecord;
using LibraryManagement.Model.UpdateBook;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace LibraryManagement.DAL
{
    public class BookDAL : IBookDAL
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
        /// 查詢書籍
        /// </summary>
        /// <param name="model">查詢條件</param>
        /// <returns></returns>
        public List<QueryBookGrid> QueryBook(QueryBookForm model)
        {
            string sql = @"SELECT bd.BOOK_ID AS BookId,                                     --書籍編號
	                              bd.BOOK_NAME AS BookName,                                 --書名
	                              bd.AUTHOR AS Author,                                      --作者
	                              bd.ISBN AS Isbn,                                          --ISBN
	                              sr.STOREROOM_NAME AS StoreRoom,                           --館藏地
	                              bd.STORE_ID AS StoreId ,                                  --索書號
	                              bd.VERSION AS VersionNum,                                 --版本號
	                              bd.QUANTITY AS Quantity,                                  --數量
	                              bd.PUBLISHER AS Publisher,                                --出版商
	                              bd.PUBLISH_COUNTRY AS PublishCountry,                     --出版地
	                              YEAR(bd.PUBLISH_DATE) AS PublishYear,                     --出版年
                                  bs.STATUS_NAME AS BookStatus,                             --書籍狀態
	                              m.MEMBER_NAME AS Borrower,		 	                    --借閱人
	                              CONVERT(varchar(10), br.RETURN_DATE, 111) AS ReturnDate 	--還書日期
                          FROM BOOK_DATA bd
                              INNER JOIN BOOK_CLASS AS bc ON bc.CLASS_ID = bd.CLASS_ID
                              INNER JOIN BOOK_STATUS AS bs ON bs.STATUS_ID = bd.BOOK_STATUS
                              INNER JOIN BOOK_STOREROOM AS sr on sr.STOREROOM_ID = bd.STOREROOM
                              LEFT JOIN MEMBER AS m on m.MEMBER_ID = bd.BORROWER_ID
                              LEFT JOIN (SELECT *
		                                 FROM BOOK_RECORD AS b
		                                 WHERE b.CREATE_DATE = ( SELECT MAX(r.CREATE_DATE) FROM BOOK_RECORD AS r
									                             WHERE b.BOOK_ID = r.BOOK_ID AND b.BORROWER_ID = r.BORROWER_ID)
		                                 ) AS br ON br.BOOK_ID = bd.BOOK_ID AND br.BORROWER_ID = bd.BORROWER_ID
                           WHERE (bd.CLASS_ID = @ClassNo OR @ClassNo = '')
                              AND (UPPER(bd.BOOK_NAME) LIKE UPPER('%' + @BookName + '%') OR @BookName='')
                              AND (bd.AUTHOR = @Author OR @Author = '')
                              AND (bd.BOOK_STATUS = @BookStatus OR @BookStatus = '')";

            DataTable dtBookData = new DataTable();
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@ClassNo", !String.IsNullOrEmpty(model.ClassNo) ? model.ClassNo : String.Empty));
                cmd.Parameters.Add(new SqlParameter("@BookName", !String.IsNullOrEmpty(model.BookName) ? model.BookName : String.Empty));
                cmd.Parameters.Add(new SqlParameter("@Author", !String.IsNullOrEmpty(model.Author) ? model.Author : String.Empty));
                cmd.Parameters.Add(new SqlParameter("@BookStatus", !String.IsNullOrEmpty(model.BookStatus) ? model.BookStatus : String.Empty));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dtBookData);
                conn.Close();
            }

            List<QueryBookGrid> bookList = new List<QueryBookGrid>();
            #region Map Book Data
            foreach (DataRow row in dtBookData.Rows)
            {
                bookList.Add(new QueryBookGrid
                {
                    BookId = row["BookId"].ToString(),
                    BookName = row["BookName"].ToString(),
                    Author = row["Author"].ToString(),
                    Isbn = row["Isbn"].ToString(),
                    StoreRoom = row["StoreRoom"].ToString(),
                    StoreId = row["StoreId"].ToString(),
                    VersionNum = String.IsNullOrEmpty(row["VersionNum"].ToString()) ? (int?)null : Convert.ToInt32(row["VersionNum"]),
                    Quantity = Convert.ToInt32(row["Quantity"]),
                    Publisher = row["Publisher"].ToString(),
                    PublishCountry = row["PublishCountry"].ToString(),
                    PublishYear = row["PublishYear"].ToString(),
                    BookStatus = row["BookStatus"].ToString(),
                    Borrower = row["Borrower"].ToString(),
                    ReturnDate = String.IsNullOrEmpty(row["ReturnDate"].ToString()) ? (DateTime?)null
                                        : DateTime.ParseExact(row["ReturnDate"].ToString(), "yyyy/MM/dd", null)
                });
            }
            #endregion

            return bookList;
        }

        /// <summary>
        /// 查詢書籍資訊
        /// </summary>
        /// <param name="bookId">書籍編號</param>
        /// <returns></returns>
        public BookDetailForm QueryBookDetail(string bookId)
        {
            string sql = @"SELECT bd.BOOK_NAME AS BookName,			                            --書名
	                              bc.Class_Name AS ClassName,			                        --類別
	                              bd.AUTHOR AS Author,					                        --作者
	                              bd.PUBLISHER AS Publisher,			                        --出版商
	                              CONVERT(varchar(10), bd.PUBLISH_DATE, 111) AS PublishDate,    --出版日期
	                              bd.PUBLISH_COUNTRY AS PublishCountry,                         --出版地
	                              bd.ISBN AS Isbn,						                        --ISBN
	                              bd.VERSION AS VersionNum,			                            --版本
	                              bd.PAGE_NUM AS PageNum,				                        --頁數
	                              bd.DESCRIPTION AS Description,		                        --書籍簡介
	                              sr.STOREROOM_NAME AS StoreRoom,		                        --館藏地
	                              bd.STORE_ID AS StoreId,				                        --索書號
	                              bs.STATUS_NAME AS BookStatus,		                            --書籍狀態
	                              m.MEMBER_NAME AS Borrower,			                        --借閱人
	                              CONVERT(varchar(10), br.RETURN_DATE, 111) AS ReturnDate		--還書日期
                            FROM BOOK_DATA AS bd
                                INNER JOIN BOOK_CLASS AS bc ON bc.CLASS_ID = bd.CLASS_ID
                                INNER JOIN BOOK_STATUS AS bs ON bs.STATUS_ID = bd.BOOK_STATUS
                                INNER JOIN BOOK_STOREROOM AS sr ON sr.STOREROOM_ID = bd.STOREROOM
                                LEFT JOIN MEMBER AS m ON m.MEMBER_ID = bd.BORROWER_ID
                                LEFT JOIN (SELECT *
		                                   FROM BOOK_RECORD AS b
		                                   WHERE b.CREATE_DATE = (SELECT MAX(r.CREATE_DATE) FROM BOOK_RECORD AS r
									                              WHERE b.BOOK_ID = r.BOOK_ID AND b.BORROWER_ID = r.BORROWER_ID)
		                                   ) AS br ON br.BOOK_ID = bd.BOOK_ID AND br.BORROWER_ID = bd.BORROWER_ID
                            WHERE bd.BOOK_ID = @BookId";

            DataTable dtBookDetail = new DataTable();
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BookId", bookId));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dtBookDetail);
                conn.Close();
            }

            BookDetailForm bookDetail = new BookDetailForm();
            #region Map Book Detail
            if (dtBookDetail.Rows.Count > 0)
            {
                DataRow row = dtBookDetail.Rows[0];
                bookDetail = new BookDetailForm
                {
                    BookName = row["BookName"].ToString(),
                    ClassName = row["ClassName"].ToString(),
                    Author = row["Author"].ToString(),
                    Publisher = row["Publisher"].ToString(),
                    PublishDate = DateTime.ParseExact(row["PublishDate"].ToString(), "yyyy/MM/dd", null),
                    PublishCountry = row["PublishCountry"].ToString(),
                    Isbn = row["Isbn"].ToString(),
                    VersionNum = String.IsNullOrEmpty(row["VersionNum"].ToString()) ? (int?)null : Convert.ToInt32(row["VersionNum"]),
                    PageNum = Convert.ToInt32(row["PageNum"]),
                    Description = row["Description"].ToString(),
                    StoreRoom = row["StoreRoom"].ToString(),
                    StoreId = row["StoreId"].ToString(),
                    BookStatus = row["BookStatus"].ToString(),
                    Borrower = row["Borrower"].ToString(),
                    ReturnDate = String.IsNullOrEmpty(row["ReturnDate"].ToString()) ? (DateTime?)null
                                        : DateTime.ParseExact(row["ReturnDate"].ToString(), "yyyy/MM/dd", null)
                };
            }
            #endregion

            return bookDetail;
        }

        /// <summary>
        /// 查詢書籍
        /// </summary>
        /// <param name="bookId">書籍編號</param>
        /// <returns></returns>
        public UpdateBookForm QueryBookById(string bookId)
        {
            string sql = @"SELECT bd.BOOK_ID AS BookId,
                                  bd.CLASS_ID AS ClassNo,
	                              bd.BOOK_NAME AS BookName,
	                              bd.AUTHOR AS Author,
	                              bd.ISBN AS Isbn,
	                              bd.STOREROOM AS StoreRoom,
	                              bd.STORE_ID AS StoreId,
	                              bd.DESCRIPTION AS Description,
	                              bd.VERSION AS VersionNum,
	                              bd.PAGE_NUM AS PageNum,
	                              bd.PUBLISHER AS Publisher,
	                              bd.PUBLISH_COUNTRY AS PublishCountry,
	                              bd.PUBLISH_DATE AS PublishDate,
	                              bd.BOOK_STATUS AS BookStatus,
	                              m.MEMBER_ID AS Borrower,
	                              br.BORROW_DATE AS BorrowDate,
	                              br.RETURN_DATE AS ReturnDate
                           FROM BOOK_DATA bd
                                INNER JOIN BOOK_CLASS AS bc ON bc.CLASS_ID = bd.CLASS_ID
                                INNER JOIN BOOK_STATUS AS bs ON bs.STATUS_ID = bd.BOOK_STATUS
                                INNER JOIN BOOK_STOREROOM AS sr on sr.STOREROOM_ID = bd.STOREROOM
                                LEFT JOIN MEMBER AS m on m.MEMBER_ID = bd.BORROWER_ID
                                LEFT JOIN (SELECT *
		                                 FROM BOOK_RECORD AS b
		                                 WHERE b.CREATE_DATE = ( SELECT MAX(r.CREATE_DATE) FROM BOOK_RECORD AS r
									                             WHERE b.BOOK_ID = r.BOOK_ID AND b.BORROWER_ID = r.BORROWER_ID)
		                                 ) AS br ON br.BOOK_ID = bd.BOOK_ID AND br.BORROWER_ID = bd.BORROWER_ID
                           WHERE bd.BOOK_ID = @BookId;";

            DataTable dtBookData = new DataTable();
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BookId", bookId));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dtBookData);
                conn.Close();
            }

            UpdateBookForm model = new UpdateBookForm();
            #region Map Book Data
            if (dtBookData.Rows.Count > 0)
            {
                DataRow row = dtBookData.Rows[0];
                model = new UpdateBookForm
                {
                    BookId = row["BookId"].ToString(),
                    ClassNo = row["ClassNo"].ToString(),
                    BookName = row["BookName"].ToString(),
                    Author = row["Author"].ToString(),
                    Isbn = row["Isbn"].ToString(),
                    StoreRoom = row["StoreRoom"].ToString(),
                    StoreId = row["StoreId"].ToString(),
                    Description = row["Description"].ToString(),
                    VersionNum = String.IsNullOrEmpty(row["VersionNum"].ToString()) ? (int?)null : Convert.ToInt32(row["VersionNum"]),
                    PageNum = Convert.ToInt32(row["PageNum"]),
                    Publisher = row["Publisher"].ToString(),
                    PublishCountry = row["PublishCountry"].ToString(),
                    PublishDate = Convert.ToDateTime(row["PublishDate"].ToString()),
                    BookStatus = row["BookStatus"].ToString(),
                    Borrower = String.IsNullOrEmpty(row["Borrower"].ToString()) ? null : row["Borrower"].ToString(),
                    BorrowDate = String.IsNullOrEmpty(row["BorrowDate"].ToString()) ? (DateTime?)null
                                    : Convert.ToDateTime(row["BorrowDate"].ToString()),
                    ReturnDate = String.IsNullOrEmpty(row["ReturnDate"].ToString()) ? (DateTime?)null
                                    : Convert.ToDateTime(row["ReturnDate"].ToString())
                };
            }
            #endregion

            return model;
        }
        
        /// <summary>
        /// 查詢書籍紀錄
        /// </summary>
        /// <param name="bookId">書籍編號</param>
        /// <returns></returns>
        public List<BookRecordGrid> QueryBookRecord(string bookId)
        {
            string sql = @"SELECT ROW_NUMBER() OVER (ORDER BY br.RECORD_ID) AS SerialNo,
                                  bd.BOOK_NAME AS BookName,
                                  m.MEMBER_NAME AS Borrower,
                                  br.BORROW_DATE AS BorrowDate,
                                  br.RETURN_DATE AS ReturnDate
                           FROM BOOK_RECORD　AS br
                                LEFT JOIN BOOK_DATA AS bd ON bd.BOOK_ID = br.BOOK_ID
                                LEFT JOIN MEMBER AS m ON m.MEMBER_ID = br.BORROWER_ID
                           WHERE br.BOOK_ID = @BookId;";

            DataTable dtBookRecord = new DataTable();
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BookId", bookId));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dtBookRecord);
                conn.Close();
            }

            List<BookRecordGrid> bookRecordList = new List<BookRecordGrid>();
            #region Map Book Record
            foreach (DataRow row in dtBookRecord.Rows)
            {
                bookRecordList.Add(new BookRecordGrid
                {
                    SerialNo = row["SerialNo"].ToString(),
                    BookName = row["BookName"].ToString(),
                    Borrower = row["Borrower"].ToString(),
                    BorrowDate = String.IsNullOrEmpty(row["BorrowDate"].ToString()) ? (DateTime?)null
                                    : Convert.ToDateTime(row["BorrowDate"].ToString()),
                    ReturnDate = String.IsNullOrEmpty(row["ReturnDate"].ToString()) ? (DateTime?)null
                                    : Convert.ToDateTime(row["ReturnDate"].ToString())
                });
            }
            #endregion

            return bookRecordList;
        }

        /// <summary>
        /// 更新書籍資訊
        /// </summary>
        /// <param name="model">書籍資訊</param>
        public void UpdateBook(UpdateBookForm model)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.AppendLine(@"UPDATE BOOK_DATA
                                    SET CLASS_ID = @ClassNo,
                                        BOOK_NAME = @BookName,
                                        AUTHOR = @Author,
                                        ISBN = @Isbn,
	                                    STOREROOM = @StoreRoom,
                                        STORE_ID = @StoreId,
                                        DESCRIPTION = @Description,
                                        VERSION = @VersionNum,
                                        PAGE_NUM = @PageNum,
	                                    PUBLISHER = @Publisher,
                                        PUBLISH_COUNTRY = @PublishCountry,
                                        PUBLISH_DATE = @PublishDate,
                                        BOOK_STATUS = @BookStatus,
                                        BORROWER_ID = @Borrower,
                                        UPDATE_DATE = GETDATE()
                                    WHERE BOOK_ID = @BookId;");

            //如果書籍狀態為已借閱，需新增借閱紀錄
            if (model.BookStatus == "S002")
            {
                sqlBuilder.AppendLine(@"INSERT INTO BOOK_RECORD
	                                        (BOOK_ID, BORROWER_ID, BORROW_DATE, RETURN_DATE, CREATE_DATE)
                                        VALUES
	                                        (@BookNo, @BorrowerNo, @BorrowDate, @ReturnDate, GETDATE());");
            }

            SqlConnection conn = new SqlConnection(this.GetDBConnectionString());
            SqlCommand cmd = new SqlCommand(sqlBuilder.ToString(), conn);
            #region Add Parameters
            cmd.Parameters.Add(new SqlParameter("@BookId", model.BookId));
            cmd.Parameters.Add(new SqlParameter("@ClassNo", model.ClassNo));
            cmd.Parameters.Add(new SqlParameter("@BookName", model.BookName));
            cmd.Parameters.Add(new SqlParameter("@Author", model.Author));
            cmd.Parameters.Add(new SqlParameter("@Isbn", model.Isbn));
            cmd.Parameters.Add(new SqlParameter("@StoreRoom", model.StoreRoom));
            cmd.Parameters.Add(new SqlParameter("@StoreId", model.StoreId));
            cmd.Parameters.Add(new SqlParameter("@Description", model.Description));
            cmd.Parameters.Add(new SqlParameter("@VersionNum", model.VersionNum == null ? (object)DBNull.Value : model.VersionNum));
            cmd.Parameters.Add(new SqlParameter("@PageNum", model.PageNum));
            cmd.Parameters.Add(new SqlParameter("@Publisher", model.Publisher));
            cmd.Parameters.Add(new SqlParameter("@PublishCountry", model.PublishCountry));
            cmd.Parameters.Add(new SqlParameter("@PublishDate", model.PublishDate));
            cmd.Parameters.Add(new SqlParameter("@BookStatus", model.BookStatus));
            cmd.Parameters.Add(new SqlParameter("@Borrower", String.IsNullOrEmpty(model.Borrower) ? (object)DBNull.Value : model.Borrower));
            //判斷書籍狀態是否為已借閱，需加入借閱紀錄參數
            if (model.BookStatus == "S002")
            {
                cmd.Parameters.Add(new SqlParameter("@BookNo", model.BookId));
                cmd.Parameters.Add(new SqlParameter("@BorrowerNo", model.Borrower));
                cmd.Parameters.Add(new SqlParameter("@BorrowDate", model.BorrowDate));
                cmd.Parameters.Add(new SqlParameter("@ReturnDate", model.ReturnDate));
            }
            #endregion

            conn.Open();
            SqlTransaction transaction = conn.BeginTransaction();
            cmd.Transaction = transaction;
            try
            {
                cmd.ExecuteScalar();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("update book exception msg:", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 新增書籍
        /// </summary>
        /// <param name="model">書籍資料</param>
        public void AddBook(AddBookForm model)
        {
            string sql = @"INSERT INTO BOOK_DATA
	                           (CLASS_ID, BOOK_NAME, AUTHOR, ISBN, STOREROOM, STORE_ID, DESCRIPTION,
                                VERSION, PAGE_NUM, QUANTITY, PUBLISHER, PUBLISH_COUNTRY, PUBLISH_DATE,
                                BOOK_STATUS, CREATE_DATE)
                           VALUES
	                           (@ClassNo, @BookName, @Author, @Isbn, @StoreRoom, @StoreId, @Description,
                                @VersionNum, @PageNum, '1', @Publisher, @PublishCountry, @PublishDate,
                                'S001', GETDATE())
                           SELECT SCOPE_IDENTITY() --傳回目前工作階段中，任何資料表產生的最後一個識別值";

            SqlConnection conn = new SqlConnection(this.GetDBConnectionString());
            SqlCommand cmd = new SqlCommand(sql, conn);
            #region Add Parameters
            cmd.Parameters.Add(new SqlParameter("@ClassNo", model.ClassNo));
            cmd.Parameters.Add(new SqlParameter("@BookName", model.BookName));
            cmd.Parameters.Add(new SqlParameter("@Author", model.Author));
            cmd.Parameters.Add(new SqlParameter("@Isbn", model.Isbn));
            cmd.Parameters.Add(new SqlParameter("@StoreRoom", model.StoreRoom));
            cmd.Parameters.Add(new SqlParameter("@StoreId", model.StoreId));
            cmd.Parameters.Add(new SqlParameter("@Description", model.Description));
            cmd.Parameters.Add(new SqlParameter("@VersionNum", model.VersionNum == null ? (object)DBNull.Value : model.VersionNum));
            cmd.Parameters.Add(new SqlParameter("@PageNum", model.PageNum));
            cmd.Parameters.Add(new SqlParameter("@Publisher", model.Publisher));
            cmd.Parameters.Add(new SqlParameter("@PublishCountry", model.PublishCountry));
            cmd.Parameters.Add(new SqlParameter("@PublishDate", model.PublishDate));
            #endregion

            conn.Open();
            SqlTransaction transaction = conn.BeginTransaction();
            cmd.Transaction = transaction;
            try
            {
                cmd.ExecuteScalar();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Insert book exception msg:", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 刪除書籍
        /// </summary>
        /// <param name="bookId">書籍編號</param>
        public void DeleteBook(string bookId)
        {
            string sql = @"DELETE FROM BOOK_DATA
                           WHERE BOOK_ID = @BookId
                                AND (BOOK_STATUS <> 'S002')";

            SqlConnection conn = new SqlConnection(this.GetDBConnectionString());
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@BookId", bookId));

            conn.Open();
            SqlTransaction transaction = conn.BeginTransaction();
            cmd.Transaction = transaction;

            try
            {
                cmd.ExecuteNonQuery(); //傳回受影響列數
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("delete book exception msg:", ex);
            }
            finally
            {
                conn.Close();
            }
        }

    }
}