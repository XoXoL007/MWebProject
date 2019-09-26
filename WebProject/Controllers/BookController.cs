using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebProject.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            List<Books> books;
            using (Model1 db = new Model1())
            {
                books = db.Books.ToList();
            }
            return View(books);
        }

        public ActionResult CreateAndEdit(int? id)
        {
            Books book;
            using (Model1 db = new Model1())
            {
                book = db.Books.Where(a => a.Id == id).FirstOrDefault();
                if (book != null)
                {
                    return View(book);
                }
                else
                {
                    return View();
                }
            }
        }

        [HttpPost]
        public ActionResult CreateAndEdit(Books book)
        {
            using (Model1 db = new Model1())
            {
                if (book.Id != 0)
                {
                    var oldBook = db.Books.Where(a => a.Id == book.Id).FirstOrDefault();
                    oldBook.Title = book.Title;
                    oldBook.Price = book.Price;
                    oldBook.Pages = book.Pages;
                    oldBook.AuthorId = book.AuthorId;

                    db.SaveChanges();
                    return RedirectToAction("Index", "Book");
                }
                else
                {
                    db.Books.Add(book);
                    db.SaveChanges();
                    return Redirect("Index");
                }

            }
        }

        public ActionResult Delete(int id)
        {
            using (Model1 db = new Model1())
            {
                var book = db.Books.Where(a => a.Id == id).FirstOrDefault();
                db.Books.Remove(book);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Book");
        }
    }
}