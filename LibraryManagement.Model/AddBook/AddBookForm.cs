using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Model.AddBook
{
    public class AddBookForm
    {
        /// <summary>
        /// 類別
        /// </summary>
        [Required(ErrorMessage = "請輸入類別")]
        [DisplayName("類別")]
        public string ClassNo { get; set; }
        public IEnumerable<SelectListItem> ClassNoList { get; set; }

        /// <summary>
        /// 書名
        /// </summary>
        [Required(ErrorMessage = "請輸入書名")]
        [DisplayName("書名")]
        [StringLength(50, ErrorMessage = "此欄位僅接受50個字")]
        public string BookName { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        [Required(ErrorMessage = "請輸入作者")]
        [DisplayName("作者")]
        [StringLength(25, ErrorMessage = "此欄位僅接受25個字")]
        public string Author { get; set; }

        /// <summary>
        /// ISBN
        /// </summary>
        [Required(ErrorMessage = "請輸入ISBN")]
        [DisplayName("ISBN")]
        [StringLength(13, ErrorMessage = "此欄位僅接受13個字")]
        public string Isbn { get; set; }

        /// <summary>
        /// 索書號
        /// </summary>
        [Required(ErrorMessage = "請輸入索書號")]
        [DisplayName("索書號")]
        public string StoreId { get; set; }

        /// <summary>
        /// 館藏地
        /// </summary>
        [Required(ErrorMessage = "請輸入館藏地")]
        [DisplayName("館藏地")]
        public string StoreRoom { get; set; }
        public IEnumerable<SelectListItem> StoreRoomList { get; set; }

        /// <summary>
        /// 書籍簡介
        /// </summary>
        [Required(ErrorMessage = "請輸入書籍簡介")]
        [DisplayName("書籍簡介")]
        [StringLength(1000, ErrorMessage = "此欄位僅接受1000個字")]
        public string Description { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        [DisplayName("版本")]
        public int? VersionNum { get; set; }

        /// <summary>
        /// 頁數
        /// </summary>
        [Required(ErrorMessage = "請輸入頁數")]
        [DisplayName("頁數")]
        public int? PageNum { get; set; }

        /// <summary>
        /// 出版日期
        /// </summary>
        [Required(ErrorMessage = "請輸入出版日期")]
        [DisplayName("出版日期")]
        public DateTime? PublishDate { get; set; }

        /// <summary>
        /// 出版地
        /// </summary>
        [Required(ErrorMessage = "請輸入出版地")]
        [DisplayName("出版地")]
        [StringLength(7, ErrorMessage = "此欄位僅接受7個字")]
        public string PublishCountry { get; set; }

        /// <summary>
        /// 出版商
        /// </summary>
        [Required(ErrorMessage = "請輸入出版商")]
        [DisplayName("出版商")]
        [StringLength(15, ErrorMessage = "此欄位僅接受15個字")]
        public string Publisher { get; set; }
    }
}
