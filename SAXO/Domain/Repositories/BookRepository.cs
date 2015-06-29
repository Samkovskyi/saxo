using System;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;

using SAXO.Domain.Abstractions;
using SAXO.Domain.Model;

namespace SAXO.Domain.Repositories

{
    public class BookRepository : IRepository<Book>
    {
        private readonly BookContext dbContext;

        public BookRepository()
        {
            this.dbContext = new BookContext();
        }
        public void Add(Book entity)
        {
            this.dbContext.Books.Add(entity);
        }

        public void Delete(Book entity)
        {
            this.dbContext.Books.Remove(entity);
        }

        public void Edit(Book entity)
        {
            this.dbContext.Entry(entity).State = EntityState.Modified;
        }

        public IQueryable<Book> FindBy(Expression<Func<Book, bool>> predicate)
        {
            return this.dbContext.Books.Where(predicate);
        }

        public IQueryable<Book> GetAll()
        {
            return this.dbContext.Books;
        }

        public void Save()
        {            
            this.dbContext.SaveChanges();
        }
    }
}