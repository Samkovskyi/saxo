using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAXO.Domain
{
    public class Book
    {
        public Book() { }

        public Book(dynamic product)
        {
            this.Id = product.id;
            this.ISBN10 = product.isbn10;
            this.ISBN13 = product.isbn13;
            this.Name = product.title;
            this.ImageUrl = product.imageurl;
            this.Url = product.url;
        }

        public Int32 Id { get; set; }
        public String ISBN10 { get; set; }
        public String ISBN13 { get; set; }
        public String Name { get; set; }
        public String Url { get; set; }
        public String ImageUrl { get; set; }
    }
}