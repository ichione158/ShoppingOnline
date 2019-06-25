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
    
namespace ShoppingOnline.Controllers
{
    public class HomeController : Controller
    {
        private DbShoppingContext db = new DbShoppingContext();
        // GET: Products
        public ActionResult Index(string SreachString, int? cateID, int? Page_No, int Size_Of_Page = 12)
        {
            int Number_Of_Page = (Page_No ?? 1);

            // Kiểm tra chuỗi tìm kiếm
            if (String.IsNullOrEmpty(SreachString))
            {
                SreachString = "";  
            }
            var products = db.Products.Include(p => p.category).Where(p => p.name.Contains(SreachString)).OrderBy(p => p.id).ToPagedList(Number_Of_Page, Size_Of_Page);
            if (cateID != null)
            {
                ViewBag.cateID = cateID;
                ViewBag.ChuoiTimKiem = SreachString;
                products = db.Products.Include(p => p.category).Where(p => p.cateId == cateID && p.name.Contains(SreachString)).OrderBy(p => p.id).ToPagedList(Number_Of_Page, Size_Of_Page);
                return View(products);
            }
            ViewBag.ChuoiTimKiem = SreachString;
            return View(products);
        }

        public ActionResult TimKiem(string SreachString, int? cateID, int? Page_No, int Size_Of_Page = 10)
        {
            int Number_Of_Page = (Page_No ?? 1);

            // Kiểm tra chuỗi tìm kiếm
            if (String.IsNullOrEmpty(SreachString))
            {
                SreachString = "";
            }
            ViewBag.cateID = cateID;
            ViewBag.ChuoiTimKiem = SreachString;
            var products = db.Products.Include(p => p.category).Where(p => p.cateId == cateID && p.name.Contains(SreachString)).OrderBy(p => p.id).ToPagedList(Number_Of_Page, Size_Of_Page);
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

        public ActionResult Menu()
        {
            var categories = db.Categories.ToList();
            return PartialView(categories);
        }
    }
}