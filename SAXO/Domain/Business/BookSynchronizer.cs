using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SAXO.Domain.Abstractions;
using SAXO.Domain.Model;

namespace SAXO.Domain.Business
{
    public class BookSynchronizer: IBookSynchronizer
    {
        private readonly IRepository<Book> booksRepo;
        private readonly IBookService booksService;

        public BookSynchronizer(IRepository<Book> booksRepo, IBookService booksService)
        {
            this.booksRepo = booksRepo;
            this.booksService = booksService;
        }

        public IEnumerable<Book> GetAll()
        {
            return booksRepo.GetAll().ToList();
        }

        public IEnumerable<Book> RetrieveNewBooks(IEnumerable<string> isbnList)
        {
            List<Book> newBooks;
            var newBooksISBNList = GetNotExistISBN(isbnList).ToList();
            
            if (newBooksISBNList.Count > 49)
            {
                newBooks = Task.Run(() => booksService.GetBooksAsync(newBooksISBNList)).Result.ToList();
            }
            else
            {
                newBooks = booksService.GetBooks(newBooksISBNList).ToList();
            }

            AddBooksToRepo(newBooks);
            return newBooks;
        }

        private IEnumerable<String> GetNotExistISBN(IEnumerable<string> isbnList)
        {
            var existISBN = booksRepo
                                .FindBy(b => isbnList.Contains(b.ISBN10) || isbnList.Contains(b.ISBN13))
                                .SelectMany(b => new[] { b.ISBN10, b.ISBN13 });
            return isbnList.Except(existISBN);
        }

        private void AddBooksToRepo(IEnumerable<Book> books)
        {
            foreach (var book in books)
            {
                booksRepo.Add(book);
            }
            booksRepo.Save();
        }
    }
}