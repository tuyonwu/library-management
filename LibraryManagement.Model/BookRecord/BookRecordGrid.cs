using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Model.BookRecord
{
    public class BookRecordGrid
    {
        /// <summary>
        /// 序號
        /// </summary>
        public string SerialNo { get; set; }

        /// <summary>
        /// 書名
        /// </summary>
        [DisplayName("書名")]
        public string BookName { get; set; }

        /// <summary>
        /// 借閱人
        /// </summary>
        [DisplayName("借閱人")]
        public string Borrower { get; set; }

        /// <summary>
        /// 借閱日期
        /// </summary>
        [DisplayName("借閱日期")]
        public DateTime? BorrowDate { get; set; }

        /// <summary>
        /// 還書日期
        /// </summary>
        [DisplayName("還書日期")]
        public DateTime? ReturnDate { get; set; }
    }
}
