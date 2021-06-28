using LibraryManagement.DAL;
using LibraryManagement.Model;
using LibraryManagement.Model.AddBook;
using LibraryManagement.Model.BookDetail;
using LibraryManagement.Model.BookRecord;
using LibraryManagement.Model.UpdateBook;
using System.Collections.Generic;

namespace LibraryManagement.BLL
{
    public class BookBLL : IBookBLL
    {
        private IBookDAL IBookDAL { get; set; }

        /// <summary>
        /// 查詢書籍
        /// </summary>
        /// <param name="model">查詢條件</param>
        /// <returns></returns>
        public List<QueryBookGrid> QueryBoook(QueryBookForm model)
        {
            return IBookDAL.QueryBook(model);
        }

        /// <summary>
        /// 查詢書籍資訊
        /// </summary>
        /// <param name="BookId">書籍編號</param>
        /// <returns></returns>
        public BookDetailForm QueryBookDetail(string bookId)
        {
            return IBookDAL.QueryBookDetail(bookId);
        }

        /// <summary>
        /// 查詢書籍
        /// </summary>
        /// <param name="bookId">書籍編號</param>
        /// <returns></returns>
        public UpdateBookForm QueryBookById(string bookId)
        {
            return IBookDAL.QueryBookById(bookId);
        }

        /// <summary>
        /// 查詢書籍紀錄
        /// </summary>
        /// <param name="bookId">書籍編號</param>
        /// <returns></returns>
        public List<BookRecordGrid> QueryBookRecord(string bookId)
        {
            return IBookDAL.QueryBookRecord(bookId);
        }

        /// <summary>
        /// 新增書籍
        /// </summary>
        /// <param name="model"></param>
        public void AddBook(AddBookForm model)
        {
            IBookDAL.AddBook(model);
        }

        /// <summary>
        /// 刪除書籍
        /// </summary>
        /// <param name="bookId">書籍編號</param>
        public string DeleteBook(string bookId)
        {
            var bookData = IBookDAL.QueryBookById(bookId);
            //判斷書籍是否存在
            if(string.IsNullOrEmpty(bookData.BookId))
            {
                return "IsNotExist";
            }
            //判斷書籍是否已被借閱
            if (bookData.BookStatus == "S002")
            {
                return "IsBorrow";
            }

            IBookDAL.DeleteBook(bookId);
            return "IsDelete";
        }

        /// <summary>
        /// 更新書籍資訊
        /// </summary>
        /// <param name="model">書籍資訊</param>
        public void UpdateBook(UpdateBookForm model)
        {
            IBookDAL.UpdateBook(model);         
        }
    }
}
