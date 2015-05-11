using SAXO.Abstractions;
using SAXO.Domain;
using SAXO.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAXO.Controllers
{
    public class HomeController : Controller
    {
        private IRepository<Book> booksRepo;
        private BookService booksService;
        public HomeController(IRepository<Book> booksRepo, BookService booksService)
        {
            this.booksRepo = booksRepo;
            this.booksService = booksService;
        }
        public ActionResult Index()
        {
            
            return View();
        }


        public ActionResult ShowBooks()
        {
            var books = booksRepo.GetAll();
            return PartialView("BooksList",books);
        }

        [HttpPost]
        public ActionResult GetBooks(String isbns)
        {
            var isbnsList = isbns.Trim().Split('\n');
            var notInDb = new List<String>();
            foreach(var id in isbnsList)
            {
                if (id.Length == 10 && !booksRepo.FindBy(b => b.ISBN10.Equals(id)).Any())
                {
                    notInDb.Add(id);
                }
                else if (id.Length == 13 && !booksRepo.FindBy(b => b.ISBN13.Equals(id)).Any())
                {
                    notInDb.Add(id);
                }
            }
            if (notInDb.Count > 0)
            {
                var newBooks = booksService.GetBooks(notInDb);
                return Content(newBooks.Count().ToString());
            }
            return Content("No new books");
            
        }
            
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}