using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using iSMusic.Models.EFModels;

namespace iSMusic.Controllers
{
    public class OrdersController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Coupon).Include(o => o.Member);
            return View(orders.ToList());
        }

        //public ActionResult Order_Product_Metadata()
        //{
        //    var orders = db.Orders.Include(o => o.Coupon).Include(o => o.Member);
        //    return View(orders.ToList());
        //}

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.couponId = new SelectList(db.Coupons, "id", "couponText");
            ViewBag.memberId = new SelectList(db.Members, "id", "memberNickName");
            return View();
        }

        // POST: Orders/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,memberId,couponId,payments,orderStatus,paid,created,receiver,address,cellphone")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.couponId = new SelectList(db.Coupons, "id", "couponText", order.couponId);
            ViewBag.memberId = new SelectList(db.Members, "id", "memberNickName", order.memberId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.couponId = new SelectList(db.Coupons, "id", "couponText", order.couponId);
            ViewBag.memberId = new SelectList(db.Members, "id", "memberNickName", order.memberId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,memberId,couponId,payments,orderStatus,paid,created,receiver,address,cellphone")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.couponId = new SelectList(db.Coupons, "id", "couponText", order.couponId);
            ViewBag.memberId = new SelectList(db.Members, "id", "memberNickName", order.memberId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
