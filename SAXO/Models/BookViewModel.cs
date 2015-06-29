using System;

using SAXO.Domain.Model;

namespace SAXO.Models
{
    public class BookViewModel
    {
        public BookViewModel()
        {
        }

        public BookViewModel(Book book)
        {
            this.Name = book.Name;
            this.Url = book.Url;
            this.ImageUrl = book.ImageUrl;
        }

        public String Name { get; set; }
        public String Url { get; set; }
        public String ImageUrl { get; set; }
    }
}