using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using SAXO.Domain.Abstractions;
using SAXO.Domain.Model;
using SAXO.Helpers;

namespace SAXO.Domain.Services
{
    public class BookService : IBookService
    {
        public IEnumerable<Book> GetBooks(IEnumerable<String> isbnList)
        {
            var newBooks = new List<Book>();

            var splitedIsbn = SplitIsbnList(isbnList);
            foreach (var isbnPart in splitedIsbn.Where(p => p.Any()))
            {
                newBooks.AddRange(this.GetBooksFromService(isbnPart));
            }

            return newBooks;
        }

        private IEnumerable<Book> GetBooksFromService(IEnumerable<String> isbnList)
        {
            using (var client = new WebClient())
            {
                var responce = client.DownloadString(GetBookServiceUrl(isbnList));
                return JsonToBooks(responce);
            }
        }

        private async Task<String> GetBooksFromServiceAsync(IEnumerable<String> isbnList)
        {
            using (var client = new WebClient())
            {
                var serviceUri = new Uri(GetBookServiceUrl(isbnList));
                return await client.DownloadStringTaskAsync(serviceUri);
            }
        }

        public async Task<IEnumerable<Book>> GetBooksAsync(IEnumerable<String> isbnList)
         {
             var newBooks = new List<Book>();

             var splitedIsbn = this.SplitIsbnList(isbnList);
             foreach (var isbnPart in splitedIsbn.Where(p => p.Any()))
             {
                 var jsonResponce = await GetBooksFromServiceAsync(isbnPart);
                 var books = JsonToBooks(jsonResponce);
                 newBooks.AddRange(books);
             }

             return newBooks;
         }

        private IEnumerable<IEnumerable<String>> SplitIsbnList(IEnumerable<String> isbnList, Int32 chunkLength = 50)
        {
            var totalLength = isbnList.Count();
            var nChunks = totalLength / chunkLength + 1;
            var parts = Enumerable.Range(0, nChunks)
                                  .Select(i => isbnList.Skip(i * chunkLength).Take(chunkLength));

            return parts;
        }

        private static IEnumerable<Book> JsonToBooks(String content)
        {
            var books = new List<Book>();
            if (String.IsNullOrEmpty(content)) return books;

            var deserializeData = JsonConvert.DeserializeObject<dynamic>(content);
            foreach (var product in deserializeData.products)
            {
                books.Add(new Book(product));
            }
            return books;
        }

        private static String GetBookServiceUrl(IEnumerable<String> isbnList)
        {
            var isbnUrlPart = String.Join(",", isbnList);
            var booksApiUrl = ConfigurationHelper.GetBooksApiUrl();
            var apiKey = ConfigurationHelper.GetApiKey();
            var url = String.Format("{0}?key={1}&isbn={2}", booksApiUrl, apiKey, isbnUrlPart);
            return url;
        }
    }
}