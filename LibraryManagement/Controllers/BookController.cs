using LibraryManagement.Model;
using LibraryManagement.Model.BookDetail;
using LibraryManagement.Model.AddBook;
using LibraryManagement.Model.UpdateBook;
using LibraryManagement.BLL;
using System.Web.Mvc;

namespace LibraryManagement.Controllers
{
    public class BookController : Controller
    {
        private IDropDownBLL IDropDownBLL { get; set; }
        private IBookBLL IBookBLL { get; set; }

        /// <summary>
        /// 查詢書籍 畫面
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryBook()
        {
            ViewBag.IsSearching = false;

            QueryBookForm model = new QueryBookForm
            {
                ClassNoList = IDropDownBLL.GetBookClassDropDown(),
                BookStatusList = IDropDownBLL.GetBookStatusDropDown()
            };

            return View("QueryBook", model);
        }

        /// <summary>
        /// 查詢書籍
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult QueryBook(QueryBookForm queryModel)
        {
            ViewBag.IsSearching = true;
            QueryBookForm model = new QueryBookForm
            {
                ClassNoList = IDropDownBLL.GetBookClassDropDown(),
                BookStatusList = IDropDownBLL.GetBookStatusDropDown()
            };

            ViewBag.QueryBookGrid = IBookBLL.QueryBoook(queryModel);

            return View("QueryBook", model);
        }

        /// <summary>
        /// 書籍資訊 畫面
        /// </summary>
        /// <returns></returns>
        public ActionResult BookDetail(string bookId)
        {
            BookDetailForm model = IBookBLL.QueryBookDetail(bookId);
            return View("BookDetail", model);
        }

        /// <summary>
        /// 新增書籍 畫面
        /// </summary>
        /// <returns></returns>
        public ActionResult AddBook()
        {
            AddBookForm model = new AddBookForm
            {
                ClassNoList = IDropDownBLL.GetBookClassDropDown(),
                StoreRoomList = IDropDownBLL.GetStoreRoomDropDown()
            };

            return View("AddBook", model);
        }

        /// <summary>
        /// 新增書籍
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddBook(AddBookForm model)
        {
            if (!ModelState.IsValid)
            {
                model.ClassNoList = IDropDownBLL.GetBookClassDropDown();
                model.StoreRoomList = IDropDownBLL.GetStoreRoomDropDown();
                return View("AddBook", model);
            }

            IBookBLL.AddBook(model);
            return Json(new { status = "successed", msg = "新增成功！" });
        }

        /// <summary>
        /// 編輯書籍 畫面
        /// </summary>
        /// <param name="BookId"></param>
        /// <returns></returns>
        public ActionResult UpdateBook(string bookId)
        {
            UpdateBookForm model = IBookBLL.QueryBookById(bookId);
            //取得下拉式選單
            model.ClassNoList = IDropDownBLL.GetBookClassDropDown();
            model.BookStatusList = IDropDownBLL.GetBookStatusDropDown();
            model.StoreRoomList = IDropDownBLL.GetStoreRoomDropDown();
            model.BorrowerList = IDropDownBLL.GetBorrowerDropDown();
            return View("UpdateBook", model);
        }

        /// <summary>
        /// 編輯書籍
        /// </summary>
        /// <param name="model">書籍資料</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateBook(UpdateBookForm model)
        {
            IBookBLL.UpdateBook(model);
            return Json(new { status = "successed", msg = "修改成功！" });
        }

        /// <summary>
        /// 刪除書籍
        /// </summary>
        /// <param name="bookId">書籍編號</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteBook(string bookId)
        {
            string deleteStatusStr = IBookBLL.DeleteBook(bookId);
            //判斷刪除狀態
            if (deleteStatusStr == "IsNotExist")
            {
                return Json(new { status = "IsNotExist", msg = "此書已不存在，請再次確認！" });
            }
            else if (deleteStatusStr == "IsBorrow")
            {
                return Json(new { status = "IsBorrow", msg = "此書已被借閱，不可刪除！" });
            }

            return Json(new { status = "IsDelete", msg = "刪除成功！" });
        }

        /// <summary>
        /// 借閱紀錄 畫面
        /// </summary>
        /// <param name="bookId">書籍編號</param>
        /// <returns></returns>
        public ActionResult BookRecord(string bookId)
        {
            ViewBag.BookRecordGrid = IBookBLL.QueryBookRecord(bookId);
            return PartialView("BookRecord");
        }
    }
}