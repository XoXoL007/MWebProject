using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebProject.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            List<Orders> orders;
            using (Model1 db = new Model1())
            {
                orders = db.Orders.ToList();
            }
            return View(orders);
        }

        public ActionResult CreateAndEdit(int? id)
        {
            Orders order;
            using (Model1 db = new Model1())
            {
                order = db.Orders.Where(a => a.Id == id).FirstOrDefault();
                if (order != null)
                {
                    return View(order);
                }
                else
                {
                    return View();
                }
            }
        }

        [HttpPost]
        public ActionResult CreateAndEdit(Orders order)
        {
            using (Model1 db = new Model1())
            {
                if (order.Id != 0)
                {
                    var oldOrder = db.Orders.Where(a => a.Id == order.Id).FirstOrDefault();
                    oldOrder.BookId = order.BookId;
                    oldOrder.UserId = order.UserId;
                    oldOrder.DateOfIssue = order.DateOfIssue;
                    oldOrder.EstimatedDeliveryDate = order.EstimatedDeliveryDate;
                    oldOrder.DateOfCompletion = order.DateOfCompletion;

                    db.SaveChanges();
                    return RedirectToAction("Index", "Order");
                }
                else
                {
                    db.Orders.Add(order);
                    db.SaveChanges();
                    return Redirect("Index");
                }

            }
        }

        public ActionResult Delete(int id)
        {
            using (Model1 db = new Model1())
            {
                var order = db.Orders.Where(a => a.Id == id).FirstOrDefault();
                db.Orders.Remove(order);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Order");
        }
    }
}