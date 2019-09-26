using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebProject.Controllers
{
    public class AuthorController : Controller
    {
        // GET: Author
        public ActionResult Index()
        {
            List<Authors> authors;
            using (Model1 db = new Model1())
            {
                authors = db.Authors.ToList();
            }
            return View(authors);
        }

        public ActionResult CreateAndEdit(int? id)
        {
            Authors author;
            using (Model1 db = new Model1())
            {
                author = db.Authors.Where(a => a.Id == id).FirstOrDefault();
                if (author != null)
                {
                    return View(author);
                }
                else
                {
                    return View();
                }
            }
        }

        [HttpPost]
        public ActionResult CreateAndEdit(Authors author)
        {
            using (Model1 db = new Model1())
            {
                if (author.Id != 0)
                {
                    var oldAuthor = db.Authors.Where(a => a.Id == author.Id).FirstOrDefault();
                    oldAuthor.FirstName = author.FirstName;
                    oldAuthor.LastName = author.LastName;

                    db.SaveChanges();
                    return RedirectToAction("Index", "Author");
                }
                else
                {
                    db.Authors.Add(author);
                    db.SaveChanges();
                    return Redirect("Index");
                }

            }
        }

        public ActionResult Delete(int id)
        {
            using (Model1 db = new Model1())
            {
                var author = db.Authors.Where(a => a.Id == id).FirstOrDefault();
                db.Authors.Remove(author);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Author");
        }
    }
}