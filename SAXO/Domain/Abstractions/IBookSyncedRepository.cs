using System;
using System.Collections.Generic;

using SAXO.Domain.Model;

namespace SAXO.Domain.Abstractions
{
    public interface IBookSynchronizer
    {
        IEnumerable<Book> GetAll();

        IEnumerable<Book> RetrieveNewBooks(IEnumerable<String> ids);
    }
}