using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShoppingOnline.DAO;
using ShoppingOnline.Models;
using PagedList;
using EntityState = System.Data.Entity.EntityState;

namespace ShoppingOnline.Areas.AdministratorCP.Controllers
{
    [Authorize(Roles = "ADMIN,MANAGER")]
    public class ProductsController : Controller
    {
        private DbShoppingContext db = new DbShoppingContext();

        // GET: Products
        public ActionResult Index(int? Page_No, int Size_Of_Page = 5)
        {
            int Number_Of_Page = (Page_No ?? 1);
            var products = db.Products.Include(p => p.category).OrderBy(p => p.id).ToPagedList(Number_Of_Page, Size_Of_Page);
            return View(products);
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
            ViewBag.cateId = new SelectList(db.Categories, "id", "name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,price,amount,description,thumbnail,valid,cateId")] Product product)
        {
            if (ModelState.IsValid)
            {
                var f = Request.Files["hinhanh"];
                if (f != null && f.ContentLength > 0)
                {
                    // lấy đường dẫn
                    var path = Server.MapPath("~/ImageUpload/" + f.FileName);
                    // Upload file lên server
                    f.SaveAs(path);
                    // Gán url của hình ảnh vào giá trị của thumnail
                    product.thumbnail = "/ImageUpload/" + f.FileName;
                }
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cateId = new SelectList(db.Categories, "id", "name", product.cateId);
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
            ViewBag.cateId = new SelectList(db.Categories, "id", "name", product.cateId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,price,amount,description,thumbnail,valid,cateId")] Product product)
        {
            if (ModelState.IsValid)
            {
                var f = Request.Files["hinhanh"];
                if (f != null && f.ContentLength > 0)
                {
                    // lấy đường dẫn
                    var path = Server.MapPath("~/ImageUpload/" + f.FileName);
                    // Upload file lên server
                    f.SaveAs(path);
                    // Gán url của hình ảnh vào giá trị của thumnail
                    product.thumbnail = "/ImageUpload/" + f.FileName;
                }
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cateId = new SelectList(db.Categories, "id", "name", product.cateId);
            return View(product);
        }

        [Authorize(Roles = "ADMIN")]
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
