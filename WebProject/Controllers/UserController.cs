using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebProject.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            List<Users> users;
            using (Model1 db = new Model1())
            {
                users = db.Users.ToList();
            }
            return View(users);
        }

        public ActionResult CreateAndEdit(int? id)
        {
            Users user;
            using (Model1 db = new Model1())
            {
                user = db.Users.Where(a => a.Id == id).FirstOrDefault();
                if (user != null)
                {
                    return View(user);
                }
                else
                {
                    return View();
                }
            }
        }

        [HttpPost]
        public ActionResult CreateAndEdit(Users user)
        {
            using (Model1 db = new Model1())
            {
                if (user.Id != 0)
                {
                    var oldUser = db.Users.Where(a => a.Id == user.Id).FirstOrDefault();
                    oldUser.UserFirstName = user.UserFirstName;
                    oldUser.UserLastName = user.UserLastName;
                    oldUser.UserEmail = user.UserEmail;

                    db.SaveChanges();
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return Redirect("Index");
                }

            }
        }

        public ActionResult Delete(int id)
        {
            using (Model1 db = new Model1())
            {
                var user = db.Users.Where(a => a.Id == id).FirstOrDefault();
                db.Users.Remove(user);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "User");
        }
    }
}