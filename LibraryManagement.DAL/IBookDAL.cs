using LibraryManagement.Model;
using LibraryManagement.Model.AddBook;
using LibraryManagement.Model.BookDetail;
using LibraryManagement.Model.BookRecord;
using LibraryManagement.Model.UpdateBook;
using System.Collections.Generic;

namespace LibraryManagement.DAL
{
    public interface IBookDAL
    {
        void AddBook(AddBookForm model);
        void DeleteBook(string bookId);
        List<QueryBookGrid> QueryBook(QueryBookForm model);
        UpdateBookForm QueryBookById(string bookId);
        BookDetailForm QueryBookDetail(string bookId);
        List<BookRecordGrid> QueryBookRecord(string bookId);
        void UpdateBook(UpdateBookForm model);
    }
}