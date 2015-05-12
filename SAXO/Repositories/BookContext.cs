using SAXO.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SAXO.Repositories

{
    public class BookContext : DbContext
    {
        public BookContext()
        {
            Database.SetInitializer<BookContext>(new DropCreateDatabaseAlways<BookContext>());
        }
        public DbSet<Book> Books { get; set; }

    }
}