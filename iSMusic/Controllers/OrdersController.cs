using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Xml;
using iSMusic.Models.EFModels;
using iSMusic.Models.ViewModels;
using Microsoft.Ajax.Utilities;
using X.PagedList;

namespace iSMusic.Controllers
{
    public class OrdersController : Controller
    {
        private AppDbContext db = new AppDbContext();


        //public ActionResult Index()
        //{
        //    var orders = db.Orders.Include(o => o.Coupon).Include(o => o.Member).ToList().Select(x => new OrderIndexVM
        //    {
        //        id = x.id,
        //        memberNickName = x.Member.memberNickName,
        //        //memberId = x.memberId,
        //        //paymentName = PaymentList[x.payments-2],
        //        paymentName = x.PaymentList[x.payments-1],
        //        orderStatus = x.orderStatus,
        //        paid = x.paid,
        //        created = x.created,
        //        receiver = x.receiver,
        //    });
        //    return View(orders.ToList());
        //}

        public ActionResult Index(int? payments, string memberNickName, int pageNumber = 1)
        {
            var paymentlist = new Order().PaymentList;   //這要改成 讓下面呼叫

            pageNumber = pageNumber > 0 ? pageNumber : 1;

            ViewBag.Categories = GetCategories(payments); //如果要改變數名稱也要改  目前有抓到值 但是是抓錯資料  
            ViewBag.memberNickName = memberNickName;



            IPagedList<Order> pagedData = GetPagedProducts(payments, memberNickName, pageNumber);
            return View(pagedData);

        }

        private IEnumerable<SelectListItem> GetCategories(int? payments)
        {
            string pay = payments.ToString();

            var items = db.Orders
                .Select(c => new SelectListItem
                { Value = c.id.ToString(), Text = pay, Selected = (payments.HasValue && c.id == payments.Value) })
                .ToList()
                .Prepend(new SelectListItem { Value = string.Empty, Text = "請選擇" });

            return items;
        }



        private IPagedList<Order> GetPagedProducts(int? payments, string memberNickName, int pageNumber)
        {
            int pageSize = 3;

            var query = db.Orders.Include(x => x.Member);

            // 若有篩選categoryid
            if (payments.HasValue) query = query.Where(p => p.payments == payments.Value);

            // 若有篩選 productName
            if (string.IsNullOrEmpty(memberNickName) == false) query = query.Where(p => p.Member.memberNickName.Contains(memberNickName));

            query = query.OrderBy(x => x.Member.memberNickName);
            return query.ToPagedList(pageNumber, pageSize);
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
            OrderDetailVM order = db.Orders.FirstOrDefault(x => x.id == id).ToDetailVM();
            var price = order.price;
            var qty = order.qty;
            var discounts = order.discounts;
            var total = price  * qty;
            if (discounts[0] == '*')
            {
                decimal discountNumber = decimal.Parse(discounts.Substring(1));
                total = total * discountNumber;
            }
            else
            {
                var discountNumber = int.Parse(discounts.Substring(1));
                total = total - discountNumber;
            }
            ViewBag.total = total;

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
            DateTime date= DateTime.Now;
            ViewBag.date = date;

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
