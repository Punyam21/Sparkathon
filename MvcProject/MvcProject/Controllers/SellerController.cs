using MvcProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProject.Controllers
{
    [Authorize]
    public class SellerController : Controller
    {
        // GET: Seller
        UserDb db = new UserDb();
         public ActionResult Index()
        {
            return View();
        }
         
        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(Product stu)
        {
            if (stu != null && ModelState.IsValid)
            {
                string r = ControllerContext.HttpContext.User.Identity.Name;
                var res = db.Users.FirstOrDefault(x => x.Emailid == r);
                stu.Users_Id = res.Id;
                string filename = Path.GetFileNameWithoutExtension(stu.ImageFile.FileName);
                string Extension = Path.GetExtension(stu.ImageFile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + Extension;
                stu.ImageUrl = "~/Images/" + filename;
                filename = Path.Combine(Server.MapPath("~/Images/"), filename);
                stu.ImageFile.SaveAs(filename);
                DateTime now = DateTime.Now;

                stu.Publish = now;
                db.Products.Add(stu);
                db.SaveChanges();
                ModelState.Clear();
                return View();
            }
            else
            {
                return View();
            }
        }

         
        public ActionResult ViewProduct()
        {
            string r=ControllerContext.HttpContext.User.Identity.Name;
            var res = db.Products.Where(x=>x.User.Emailid==r).ToList();
            return View(res);
        }
        public ActionResult Deleteroduct(int id)
        {
            var res = db.Products.FirstOrDefault(x=>x.Pid==id);
            db.Products.Remove(res);
            db.SaveChanges();
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
        public ActionResult Profile(HttpPostedFileBase ImageFile,string Name,int Mobile,string Address)
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

                return RedirectToAction("Profile", "Seller");
            }
            else
            {
                return RedirectToAction("Profile", "Seller");

            }
        }


        public ActionResult UpdatePassword()
        {
             
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePassword(string old,string newpassword)
        {            
            string s = ControllerContext.HttpContext.User.Identity.Name;
            var res = db.Users.FirstOrDefault(x => x.Emailid == s.ToString()&& x.Password==old);
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
        public ActionResult DeleteProduct(int Id)
        {
            var res=db.Products.FirstOrDefault(x => x.Pid == Id);
            db.Products.Remove(res);
            db.SaveChangesAsync();
            return RedirectToAction("ViewProduct","Seller");
        }
        [HttpGet]

        public ActionResult EditProduct(int Id)
        {
            var res = db.Products.FirstOrDefault(x => x.Pid == Id);
            
            return View(res);
        }

        [HttpPost]
        public ActionResult EditProduct(Product stu)
        {
             
            if (stu != null && ModelState.IsValid)
            {
                string r = ControllerContext.HttpContext.User.Identity.Name;
                var res = db.Users.FirstOrDefault(x => x.Emailid == r);
                stu.Users_Id = res.Id;
                if (stu.ImageFile != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(stu.ImageFile.FileName);
                    string Extension = Path.GetExtension(stu.ImageFile.FileName);
                    filename = filename + DateTime.Now.ToString("yymmssfff") + Extension;
                    stu.ImageUrl = "~/Images/" + filename; 
            
                    filename = Path.Combine(Server.MapPath("~/Images/"), filename);
                    stu.ImageFile.SaveAs(filename);
                }
                DateTime now = DateTime.Now;
                stu.Publish = now;
                db.Entry(stu).State = EntityState.Modified;
                db.SaveChangesAsync();
                ModelState.Clear();
                return RedirectToAction("ViewProduct");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

    }
}