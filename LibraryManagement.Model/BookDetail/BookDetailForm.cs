using System;
using System.ComponentModel;

namespace LibraryManagement.Model.BookDetail
{
    public class BookDetailForm
    {
        /// <summary>
        /// 書名
        /// </summary>
        [DisplayName("書名")]
        public string BookName { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        [DisplayName("作者")]
        public string Author { get; set; }

        /// <summary>
        /// 出版商
        /// </summary>
        [DisplayName("出版商")]
        public string Publisher { get; set; }

        /// <summary>
        /// 出版日期
        /// </summary>
        [DisplayName("出版日期")]
        public DateTime? PublishDate { get; set; }

        /// <summary>
        /// 出版地
        /// </summary>
        [DisplayName("出版地")]
        public string PublishCountry { get; set; }

        /// <summary>
        /// ISBN
        /// </summary>
        [DisplayName("ISBN")]
        public string Isbn { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        [DisplayName("版本")]
        public int? VersionNum { get; set; }

        /// <summary>
        /// 頁數
        /// </summary>
        [DisplayName("頁數")]
        public int? PageNum { get; set; }

        /// <summary>
        /// 書籍簡介
        /// </summary>
        [DisplayName("書籍簡介")]
        public string Description { get; set; }

        /// <summary>
        /// 館藏地
        /// </summary>
        [DisplayName("館藏地")]
        public string StoreRoom { get; set; }

        /// <summary>
        /// 索書號
        /// </summary>
        [DisplayName("索書號")]
        public string StoreId { get; set; }

        /// <summary>
        /// 書籍狀態
        /// </summary>
        [DisplayName("書籍狀態")]
        public string BookStatus { get; set; }

        /// <summary>
        /// 類別
        /// </summary>
        [DisplayName("類別")]
        public string ClassName { get; set; }

        /// <summary>
        /// 借閱人
        /// </summary>
        [DisplayName("借閱人")]
        public string Borrower { get; set; }

        /// <summary>
        /// 到期日
        /// </summary>
        [DisplayName("到期日")]
        public DateTime? ReturnDate { get; set; }
    }
}
