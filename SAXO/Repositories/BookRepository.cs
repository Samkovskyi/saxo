using SAXO.Abstractions;
using SAXO.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using System.Data.Entity;

namespace SAXO.Repositories

{
    public class BookRepository : IRepository<Book>
    {
        private readonly BookContext _dbContext;

        public BookRepository()
        {
            _dbContext = new BookContext();
        }
        public void Add(Book entity)
        {
            _dbContext.Books.Add(entity);
        }

        public void Delete(Book entity)
        {
            _dbContext.Books.Remove(entity);
        }

        public void Edit(Book entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public IQueryable<Book> FindBy(Expression<Func<Book, bool>> predicate)
        {
            return _dbContext.Books.Where(predicate);
        }

        public IQueryable<Book> GetAll()
        {
            return _dbContext.Books;
        }

        public void Save()
        {            
            _dbContext.SaveChanges();
        }
    }
}