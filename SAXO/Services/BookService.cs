using SAXO.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAXO.Domain;
using RestSharp;
using Newtonsoft.Json;
using System.Net;

namespace SAXO.Services
{
    public class BookService : IBookService
    {
        private RestClient client = new RestClient("http://api.saxo.com");

        public IEnumerable<Book> GetBooks(IEnumerable<String> isbnList)
        {
            var result = new List<Book>();
            if (!isbnList.Any())
                return result;

            var totalLength = isbnList.Count();
            var chunkLength = 50;
            var nChunks = totalLength / chunkLength + 1;
            var parts = Enumerable.Range(0, nChunks)
                                  .Select(i => isbnList.Skip(i * chunkLength).Take(chunkLength));

            foreach(var idsPart in parts)
            {
                result.AddRange(GetPartOfBooks(idsPart));
            }

            return result;
        }

        private IEnumerable<Book> GetPartOfBooks(IEnumerable<String> ids)
        {
            var result = new List<Book>();
            var request = new RestRequest("v1/products/products.json");
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("key", "08964e27966e4ca99eb0029ac4c4c217");

            foreach (var id in ids)
            {
                request.AddQueryParameter("isbn", id);
            }

            var response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return result;
            }

            var deserializeData = DeserializeObject(response.Content);
            foreach (dynamic product in deserializeData.products)
            {
                result.Add(new Book(product));
                //throw new Exception(product.ToString());
            }
            return result;
        }

        private dynamic DeserializeObject(String content)
        {
            var dynamicObject = JsonConvert.DeserializeObject<dynamic>(content);
            return dynamicObject;
        }
    }
}