using ShoppingOnline.DAO;
using ShoppingOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingOnline.Areas.AdministratorCP.Controllers
{
    [Authorize(Roles = "ADMIN,MANAGER")]
    public class ThongKeController : Controller
    {
        private DbShoppingContext db = new DbShoppingContext();
        // GET: AdministratorCP/ThongKe
        public ActionResult ThongKeSanPham()
        {
            var products = db.Products.ToList();
            var result = new List<SPThongKe>();
            foreach(var item in products)
            {
                result.Add(new SPThongKe { id = item.id, name = item.name, quantity = item.amount, price = item.price });
            }
            return View(result);
        }

        public ActionResult ThongKeNangCao()
        {
            var products = db.Products.ToList();
            var result = new List<SPThongKe>();
            foreach (var item in products)
            {
                result.Add(new SPThongKe { id = item.id, name = item.name, quantity = item.amount, price = item.price });
            }
            return View(result);
        }
    }
}