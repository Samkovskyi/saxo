using SAXO.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAXO.Abstractions
{
    public interface IBookSyncedRepository
    {
        IEnumerable<Book> GetAll();

        IEnumerable<Book> GetNotSynced(IEnumerable<String> ids);
    }
}