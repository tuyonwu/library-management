using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace LibraryManagement.Model
{
    public class QueryBookForm
    {
        /// <summary>
        /// 種類
        /// </summary>
        [DisplayName("種類")]
        public string ClassNo { get; set; }
        public IEnumerable<SelectListItem> ClassNoList { get; set; }

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
        /// 借閱狀態
        /// </summary>
        [DisplayName("借閱狀態")]
        public string BookStatus { get; set; }
        public IEnumerable<SelectListItem> BookStatusList { get; set; }
    }
}
