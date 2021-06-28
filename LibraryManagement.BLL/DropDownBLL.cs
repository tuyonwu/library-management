using System.Collections.Generic;
using LibraryManagement.DAL;
using System.Web.Mvc;

namespace LibraryManagement.BLL
{
    public class DropDownBLL : IDropDownBLL
    {
        private IDropDownDAL IDropDownDAL { get; set; }

        /// <summary>
        /// 取得書籍類別下拉式選單
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetBookClassDropDown()
        {
            return IDropDownDAL.GetBookClassDropDown();
        }

        /// <summary>
        /// 取得借閱狀態下拉式選單
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetBookStatusDropDown()
        {
            return IDropDownDAL.GetBookStatusDropDown();
        }

        /// <summary>
        /// 取得館藏地下拉式選單
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetStoreRoomDropDown()
        {
            return IDropDownDAL.GetStoreRoomDropDown();
        }

        /// <summary>
        /// 取得書名下拉式選單
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetBookNameDropDown()
        {
            return IDropDownDAL.GetBookNameDropDown();
        }

        /// <summary>
        /// 取得借閱人下拉式選單
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetBorrowerDropDown()
        {
            return IDropDownDAL.GetBorrowerDropDown();
        }
    }
}
