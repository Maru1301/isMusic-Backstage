using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using iSMusic.Models.EFModels;
using iSMusic.Models.Infrastructures.Repositories;
using iSMusic.Models.Services;
using iSMusic.Models.Services.Interfaces;
using iSMusic.Models.ViewModels;
using PagedList;

namespace iSMusic.Controllers
{
    public class ProductsController : Controller
    {
        AppDbContext db = new AppDbContext();
        private ProductService productService;

        public ProductsController()
        {
            var db = new AppDbContext();
            IProductRepository repo = new ProductRepository(db);
            this.productService = new ProductService(repo);
        }

        // GET: Products
        public ActionResult Index(int? AlbumId, int? categoryId, string productName, int pageNumber = 1)
        {

            ViewBag.Categories = GetCategories(categoryId);
            ViewBag.ProductName = productName;

          

            IPagedList<Product> pagedData = GetPagedProducts(categoryId, productName, pageNumber);

            return View(pagedData.Select(x => x.ToVM()));
        }

        private IEnumerable<SelectListItem> GetCategories(int? categoryId)
        {
            var items = db.ProductCategories
                .Select(c => new SelectListItem
                { Value = c.id.ToString(), Text = c.categoryName, Selected = (categoryId.HasValue && c.id == categoryId.Value) })
                .ToList()
                .Prepend(new SelectListItem { Value = string.Empty, Text = "請選擇" });

            return items;
        }



        private IPagedList<Product> GetPagedProducts(int? categoryId, string productName, int pageNumber)
        {
            int pageSize = 3;

            var query = db.Products.Include(x => x.ProductCategory);

            // 若有篩選categoryid
            if (categoryId.HasValue) query = query.Where(p => p.ProductCategory.id == categoryId.Value);

            // 若有篩選 productName
            if (string.IsNullOrEmpty(productName) == false) query = query.Where(p => p.productName.Contains(productName));

            query = query.OrderBy(x => x.ProductCategory.id);
            return query.ToPagedList(pageNumber, pageSize);
        }


        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.productCategoryId = new SelectList(db.ProductCategories, "id", "categoryName");
            ViewBag.albumId = new SelectList(db.Albums, "id", "albumName");
            return View();
        }

        // POST: Products/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,productCategoryId,productPrice,albumId,productName,stock,status")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.productCategoryId = new SelectList(db.ProductCategories, "id", "categoryName", product.productCategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.productCategoryId = new SelectList(db.ProductCategories, "id", "categoryName", product.productCategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,productCategoryId,productPrice,albumId,productName,stock,status")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.productCategoryId = new SelectList(db.ProductCategories, "id", "categoryName", product.productCategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
