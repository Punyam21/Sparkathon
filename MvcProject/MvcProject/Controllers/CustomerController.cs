using MvcProject.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;

namespace MvcProject.Controllers
{
    
    public class CustomerController : Controller
    {
        UserDb db = new UserDb();
     
        public ActionResult Index()
        { 
            return View();
        }
       

        [HttpGet]
        public ActionResult ViewProduct()
        {
            var res = db.Products.ToList();
            return View(res);
        }
        [HttpGet]
        public ActionResult AddCarts(int Id)
        {

            string email = ControllerContext.HttpContext.User.Identity.Name;
            Cart c = new Cart();
            var products = db.Products.Where(x => x.Pid == Id).FirstOrDefault();

            var e = db.Users.Where(x => x.Emailid == email).FirstOrDefault();
            c.Users_Id = e.Id; ;
            c.Quantity = 1;
            c.TotalPrice = products.Price;
            c.Products_Pid = Id;
            db.Carts.Add(c);
            db.SaveChanges();
            return RedirectToAction("AddCart","Customer");
        }
        public PartialViewResult SearchProduct(string search)
        {
            List<Product> res = db.Products.Where(x => x.Name.ToLower() == search).ToList();
            return PartialView("_Product_View", res);
        }

        [HttpGet]
        public ActionResult AddCart()
        {
            string s = ControllerContext.HttpContext.User.Identity.Name;
            var st = db.Users.FirstOrDefault(x => x.Emailid == s);
            var res = db.Carts.Where(x=>x.Users_Id==st.Id).ToList();
            return View(res);
        }
       
        [HttpGet]
        public ActionResult Profile()
        {
            ViewBag.r = ControllerContext.HttpContext.User.Identity.Name;
            string s = ControllerContext.HttpContext.User.Identity.Name;
            var st = db.Users.FirstOrDefault(x => x.Emailid == s.ToString());
            return View(st);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Profile(HttpPostedFileBase ImageFile, string Name, int Mobile, string Address)
        {
            string s = ControllerContext.HttpContext.User.Identity.Name;
            var st = db.Users.FirstOrDefault(x => x.Emailid == s.ToString());
            if (st != null)
            {
                if (ImageFile != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                    string Extension = Path.GetExtension(ImageFile.FileName);
                    filename = filename + DateTime.Now.ToString("yymmssfff") + Extension;
                    st.ImageUrl = "~/Images/" + filename;

                    filename = Path.Combine(Server.MapPath("~/Images/"), filename);
                    ImageFile.SaveAs(filename);

                }
                if (Name != "")
                {
                    st.Name = Name;
                }
                if (Mobile!=st.Mobileno)
                {
                    st.Mobileno = Mobile;
                }
                if (Address != "")
                {
                    st.Address = Address;
                }
                db.Entry(st).State = EntityState.Modified;
                db.SaveChangesAsync();

                return RedirectToAction("Profile", "Customer");
            }
            else
            {
                return RedirectToAction("Profile", "Customer");

            }
        }
        public ActionResult UpdatePassword()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePassword(string old, string newpassword)
        {
            string s = ControllerContext.HttpContext.User.Identity.Name;
            var res = db.Users.FirstOrDefault(x => x.Emailid == s.ToString() && x.Password == old);
            if (res != null)
            {
                res.Password = newpassword;
                db.Entry(res).State = EntityState.Modified;
                db.SaveChangesAsync();

                return RedirectToAction("Index", "Seller");
            }
            else
            {
                return View();
            }

        }

        public ActionResult DeleteFromCart(int Id)
        {
            var res = db.Carts.Where(x => x.cid == Id).FirstOrDefault();
            db.Carts.Remove(res);
            db.SaveChanges();
            return RedirectToAction("AddCart", "Customer");
        }


        ///////////////////Json
        ///
        //public JsonResult GetProductAjax()
        //{
        //    db.Configuration.ProxyCreationEnabled = false;
        //    var res=db.Products.ToList();
        //    return Json(res, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public JsonResult AddProductAjax(Product p )
        //{
        //    db.Configuration.ProxyCreationEnabled = false;
        //    var res = db.Products.ToList();
        //    return  Json((res)new { success = false });
        //}

    }
}