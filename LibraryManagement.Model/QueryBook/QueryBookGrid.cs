using System;

namespace LibraryManagement.Model
{
    public class QueryBookGrid
    {
        /// <summary>
        /// 書籍編號
        /// </summary>
        public string BookId { get; set; }

        /// <summary>
        /// 書名
        /// </summary>
        public string BookName { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 出版地
        /// </summary>
        public string PublishCountry { get; set; }

        /// <summary>
        /// 出版商
        /// </summary>
        public string Publisher { get; set; }

        /// <summary>
        /// 出版日期
        /// </summary>
        public string PublishYear { get; set; }

        /// <summary>
        /// ISBN
        /// </summary>
        public string Isbn { get; set; }

        /// <summary>
        /// 館藏地
        /// </summary>
        public string StoreRoom { get; set; }

        /// <summary>
        /// 索書號
        /// </summary>
        public string StoreId { get; set; }

        /// <summary>
        /// 借閱人
        /// </summary>
        public string Borrower { get; set; }

        /// <summary>
        /// 到期日
        /// </summary>
        public DateTime? ReturnDate { get; set; }

        /// <summary>
        /// 借閱狀態
        /// </summary>
        public string BookStatus { get; set; }

        /// <summary>
        /// 版本號
        /// </summary>
        public int? VersionNum { get; set; }

        /// <summary>
        /// 數量
        /// </summary>
        public int? Quantity { get; set; }
    }
}
