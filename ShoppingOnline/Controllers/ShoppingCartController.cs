using ShoppingOnline.DAO;
using ShoppingOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ShoppingOnline.Controllers
{
    public class ShoppingCartController : Controller
    {
        private DbShoppingContext db = new DbShoppingContext();

        public ActionResult SelectValue(int id, int quantity)
        {
            if (Session["cart"] == null)
            {
                List<CartItem> cart = new List<CartItem>();
                cart.Add(new CartItem(db.Products.Find(id), 1));
                Session["cart"] = cart;
            }
            else
            {
                List<CartItem> cart = (List<CartItem>)Session["cart"];
                int index = isExisting(id);
                if (index == -1)
                {
                    cart.Add(new CartItem(db.Products.Find(id), 1));
                }
                else
                {
                    cart[index].quantity = cart[index].quantity + quantity;
                }
                Session["cart"] = cart;
            }
            ViewBag.TongTien = TongTien();
            return RedirectToAction("Order", "ShoppingCart");
        }

        // GET: ShoppingCart
        // Action trả về view giỏ hàng
        // id: mã số sản phẩm
        // quantity: số lượng sản phẩm cần mua
        public ActionResult Order()
        {
            ViewBag.TongTien = TongTien();
            return View((List<CartItem>)Session["cart"]);
        }

        public ActionResult XoaGioHang()
        {
            List<CartItem> cart = (List<CartItem>)Session["cart"];
            cart.Clear();
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Delete(int id)
        {
            List<CartItem> cart = (List<CartItem>)Session["cart"];
            var a = cart.Find(x => x.product.id == id);
            cart.Remove(a);
            ViewBag.TongTien = TongTien();
            return View("Order", cart);
        }

        public ActionResult Add(int id, int quantity)
        {
            List<CartItem> cart = (List<CartItem>)Session["cart"];
            int index = isExisting(id);
            if (index == -1)
            {
                cart.Add(new CartItem(db.Products.Find(id), 1));
            }
            else
            {
                if (cart[index].quantity >= 1)
                {
                    cart[index].quantity = cart[index].quantity + quantity;
                }
                else
                {
                    var a = cart.Find(x => x.product.id == id);
                    cart.Remove(a);
                }
            }
            Session["cart"] = cart;
            ViewBag.TongTien = TongTien();
            return View("Order", cart);
        }

        //public ActionResult Subtract(int id, int quantity)
        //{
        //    List<CartItem> cart = (List<CartItem>)Session["cart"];
        //    int index = isExisting(id);
        //    if (index == -1)
        //    {
        //        cart.Add(new CartItem(db.Products.Find(id), 1));
        //    }
        //    else
        //    {
        //        if (cart[index].quantity > 1 )
        //        {
        //            cart[index].quantity = cart[index].quantity - quantity;
        //        }
        //        else
        //        {
        //            var a = cart.Find(x => x.product.id == id);
        //            cart.Remove(a);
        //        }
        //    }
        //    Session["cart"] = cart;
        //    ViewBag.TongTien = TongTien();
        //    return View("Order", cart);
        //}

        private double TongTien()
        {
            double tongTien = 0;
            List<CartItem> cart = (List<CartItem>)Session["cart"];
            if (cart != null)
            {
                tongTien = cart.Sum(n => n.totalprice);
            }
            return tongTien;
        }


        private int isExisting(int id)
        {
            List<CartItem> cart = (List<CartItem>)Session["cart"];
            for(int i=0;i<cart.Count; i++)
            {
                if(cart[i].product.id == id)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}