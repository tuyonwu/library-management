using System.Collections.Generic;
using System.Web.Mvc;

namespace LibraryManagement.BLL
{
    public interface IDropDownBLL
    {
        List<SelectListItem> GetBookClassDropDown();
        List<SelectListItem> GetBookNameDropDown();
        List<SelectListItem> GetBookStatusDropDown();
        List<SelectListItem> GetBorrowerDropDown();
        List<SelectListItem> GetStoreRoomDropDown();
    }
}