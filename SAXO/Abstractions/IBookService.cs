using SAXO.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAXO.Abstractions
{
    public interface IBookService
    {
        IEnumerable<Book> GetBooks(IEnumerable<String> ids);

    }
}