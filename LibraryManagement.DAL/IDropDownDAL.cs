using System.Collections.Generic;
using System.Web.Mvc;

namespace LibraryManagement.DAL
{
    public interface IDropDownDAL
    {
        List<SelectListItem> GetBookClassDropDown();
        List<SelectListItem> GetBookNameDropDown();
        List<SelectListItem> GetBookStatusDropDown();
        List<SelectListItem> GetBorrowerDropDown();
        List<SelectListItem> GetStoreRoomDropDown();
    }
}