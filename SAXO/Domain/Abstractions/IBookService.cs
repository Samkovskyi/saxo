using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using SAXO.Domain.Model;

namespace SAXO.Domain.Abstractions
{
    public interface IBookService
    {
        IEnumerable<Book> GetBooks(IEnumerable<String> isbnList);

        Task<IEnumerable<Book>> GetBooksAsync(IEnumerable<String> isbnList);
    }
}