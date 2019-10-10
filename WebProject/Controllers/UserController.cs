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
            List<Orders> userHistory = new List<Orders>();

            if (id != null)
            {
                using (Model1 db = new Model1())
                {
                    user = db.Users.Where(a => a.Id == id).FirstOrDefault();

                    var userOrders = db.Orders.OrderByDescending(n => n.DateOfIssue).Where(u => u.UserId == user.Id).Select(o => o.Id).Take(5).ToList();
                    userOrders.ForEach(
                        x =>
                        {
                            userHistory.Add(db.Orders.Where(i => i.Id == x).FirstOrDefault());
                        });
                    ViewBag.userHistory = userHistory;

                    
                }
                return View(user);
            }
            else
            {
                return View();
            }

        }

        public ActionResult userHistory()
        {
            return PartialView();
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
                }
                else
                {
                    db.Users.Add(user); 
                }
                db.SaveChanges();
            }
            return RedirectToAction("Index", "User");

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