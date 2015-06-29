using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SAXO.Helpers;

namespace SAXO.Domain.Model
{
    public class Book
    {
        public Book() { }

        public Book(dynamic product)
        {
            this.Id = product.id;
            this.ISBN10 = product.isbn10;
            this.ISBN13 = product.isbn13;
            this.Name = EncodingConverter.Convert((String)product.title);
            this.ImageUrl = product.imageurl;
            this.Url = product.url;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 Id { get; set; }
        public String ISBN10 { get; set; }
        public String ISBN13 { get; set; }
        public String Name { get; set; }
        public String Url { get; set; }
        public String ImageUrl { get; set; }

    }

   
}