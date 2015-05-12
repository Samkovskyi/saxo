using SAXO.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAXO.Domain;
using SAXO.Services;

namespace SAXO.Repositories
{
    public class BookSyncedRepository : IBookSyncedRepository
    {
        private IRepository<Book> booksRepo;
        private BookService booksService;

        public BookSyncedRepository(IRepository<Book> booksRepo, BookService booksService)
        {
            this.booksRepo = booksRepo;
            this.booksService = booksService;
        }

        public IEnumerable<Book> GetAll()
        {
            return booksRepo.GetAll().ToList();
        }

        public IEnumerable<Book> GetNotSynced(IEnumerable<string> isbnList)
        {            
            var newBooksISBNList = GetNotUploadedISBNList(isbnList);
            var newBooks = UploadBooks(newBooksISBNList);
            SyncBooks(newBooks);

            return newBooks;
        }

        private IEnumerable<String> GetNotUploadedISBNList(IEnumerable<string> isbnList)
        {
            var newBooksISBNList = new List<String>();
            foreach (var isbn in isbnList)
            {
                if (IsNotUploaded(isbn))
                {
                    newBooksISBNList.Add(isbn);
                }
            }
            return newBooksISBNList;
        }

        private Boolean IsNotUploaded(String isbn)
        {
            if (String.IsNullOrEmpty(isbn))
                return false;

            if (isbn.Length == 10 && !booksRepo.FindBy(b => b.ISBN10.Equals(isbn)).Any())
            {
                return true;
            }
            else if (isbn.Length == 13 && !booksRepo.FindBy(b => b.ISBN13.Equals(isbn)).Any())
            {
                return true;
            }

            return false;
        }

        private IEnumerable<Book> UploadBooks(IEnumerable<string> isbnList)
        {
            return booksService.GetBooks(isbnList);
        }

        private void SyncBooks(IEnumerable<Book> books)
        {
            foreach (var book in books)
            {
                booksRepo.Add(book);
            }
            booksRepo.Save();
        }
    }
}