using SAXO.Abstractions;
using SAXO.Domain;
using SAXO.Models;
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
        private IBookSyncedRepository booksRepo;

        public HomeController(IBookSyncedRepository booksRepo)
        {

            this.booksRepo = booksRepo;
        }

        public ActionResult Index()
        {            
            return View();
        }


        public ActionResult ShowBooks()
        {
            var books = booksRepo.GetAll();
            return PartialView("BooksList", books);
        }

        [HttpPost]
        public ActionResult GetBooks(ISBNViewModel model)
        {
            var isbnsList = model.ISBN.Replace("\r", "").Split('\n');
            var newBooks = booksRepo.GetNotSynced(isbnsList);
            return PartialView("BooksList", newBooks);
        }
    }
}