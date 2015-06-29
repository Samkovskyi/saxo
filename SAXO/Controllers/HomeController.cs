using SAXO.Domain.Abstractions;
using SAXO.Models;
using System.Linq;
using System.Web.Mvc;

namespace SAXO.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookSynchronizer bookSynchronizer;

        public HomeController(IBookSynchronizer bookSynchronizer)
        {
            this.bookSynchronizer = bookSynchronizer;
        }

        public ActionResult Index()
        {            
            return View();
        }

        public ActionResult ShowBooks()
        {
            var books = bookSynchronizer
                            .GetAll()
                            .Select(b => new BookViewModel(b));

            return PartialView("BooksList", books);
        }

        [HttpPost]
        public ActionResult RetrieveBooks(ISBNViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isbnsList = model.ISBN.Replace("\r", "").Split('\n');
                var newBooks = bookSynchronizer
                                    .RetrieveNewBooks(isbnsList)
                                    .Select(b => new BookViewModel(b));
                return PartialView("BooksList", newBooks);
            }
            return new EmptyResult();
        }
    }
}