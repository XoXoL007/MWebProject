using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebProject.Controllers
{
    public class AuthorController : Controller
    {
        #region До добавления UOW
        //public ActionResult Index()
        //{
        //    List<Authors> authors;
        //    using (Model1 db = new Model1())
        //    {
        //        authors = db.Authors.ToList();
        //    }
        //    return View(authors);
        //}

        //public ActionResult CreateAndEdit(int? id)
        //{
        //    Authors author;
        //    using (Model1 db = new Model1())
        //    {
        //        author = db.Authors.Where(a => a.Id == id).FirstOrDefault();
        //        if (author != null)
        //        {
        //            return View(author);
        //        }
        //        else
        //        {
        //            return View();
        //        }
        //    }
        //}

        //[HttpPost]
        //public ActionResult CreateAndEdit(Authors author)
        //{
        //    using (Model1 db = new Model1())
        //    {
        //        if (author.Id != 0)
        //        {
        //            var oldAuthor = db.Authors.Where(a => a.Id == author.Id).FirstOrDefault();
        //            oldAuthor.FirstName = author.FirstName;
        //            oldAuthor.LastName = author.LastName;

        //            db.SaveChanges();
        //            return RedirectToAction("Index", "Author");
        //        }
        //        else
        //        {
        //            db.Authors.Add(author);
        //            db.SaveChanges();
        //            return Redirect("Index");
        //        }

        //    }
        //}
        //public ActionResult Delete(int id)
        //{
        //    using (Model1 db = new Model1())
        //    {
        //        var author = db.Authors.Where(a => a.Id == id).FirstOrDefault();
        //        db.Authors.Remove(author);
        //        db.SaveChanges();
        //    }
        //    return RedirectToAction("Index", "Author");
        //}
        #endregion

        WebProject.UnitOfWork.UnitOfWork unitOfWork;
        public AuthorController()
        {
            unitOfWork = new UnitOfWork.UnitOfWork();
        }

        public ActionResult Index()
        {
            IEnumerable<Authors> model = unitOfWork.AuthorUowRepository.GetAll();
            return View(model);
        }

        public ActionResult CreateAndEdit(int? id)
        {
            Authors model = unitOfWork.AuthorUowRepository.Get(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateAndEdit(Authors author)
        {
            if(author.Id == 0)
            {
                unitOfWork.AuthorUowRepository.Create(author);
            }
            else
            {
                unitOfWork.AuthorUowRepository.Update(author);
            }
            unitOfWork.AuthorUowRepository.Save();

            return RedirectToActionPermanent("Index", "Author");
        }

        public ActionResult Delete(int id)
        {
            unitOfWork.AuthorUowRepository.Delete(id);
            unitOfWork.AuthorUowRepository.Save();

            return RedirectToActionPermanent("Index", "Author");
        }
    }
}