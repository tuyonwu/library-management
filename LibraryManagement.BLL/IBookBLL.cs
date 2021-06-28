using LibraryManagement.Model;
using LibraryManagement.Model.AddBook;
using LibraryManagement.Model.BookDetail;
using LibraryManagement.Model.BookRecord;
using LibraryManagement.Model.UpdateBook;
using System.Collections.Generic;

namespace LibraryManagement.BLL
{
    public interface IBookBLL
    {
        void AddBook(AddBookForm model);
        string DeleteBook(string bookId);
        UpdateBookForm QueryBookById(string bookId);
        BookDetailForm QueryBookDetail(string bookId);
        List<BookRecordGrid> QueryBookRecord(string bookId);
        List<QueryBookGrid> QueryBoook(QueryBookForm model);
        void UpdateBook(UpdateBookForm model);
    }
}