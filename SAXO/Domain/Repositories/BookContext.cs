using System.Data.Entity;

using SAXO.Domain.Model;

namespace SAXO.Domain.Repositories
{
    public class BookContext : DbContext
    {
        public BookContext()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<BookContext>());
        }

        public DbSet<Book> Books { get; set; }
    }
}