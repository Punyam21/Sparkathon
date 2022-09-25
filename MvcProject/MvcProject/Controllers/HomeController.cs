using Microsoft.Owin.Security.Cookies;
using MvcProject.Models;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;
using System.Web.Security;
using MvcProject.App_Start;
namespace MvcProject.Controllers
{

    public class HomeController : Controller
    {

        UserDb db = new UserDb();
        EncryptDecrypt e = new EncryptDecrypt();



        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Register()
        {

            return View();
        }
        [HttpPost]
         public ActionResult Register(User stu)
        {
            if(stu!=null&& ModelState.IsValid)
            {
                User st = new User();
                string filename = Path.GetFileNameWithoutExtension(stu.ImageFile.FileName);
                string Extension = Path.GetExtension(stu.ImageFile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + Extension;
                stu.ImageUrl = "~/Images/" + filename;
                st.ImageUrl = stu.ImageUrl;
                filename = Path.Combine(Server.MapPath("~/Images/"), filename);
                stu.ImageFile.SaveAs(filename);
                stu.Password =stu.Password;
                db.Users.Add(stu);
                
                db.SaveChanges();
                return View("Login");
            }
            else
            {
                return View();
            }
           
            
         }



        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        [OutputCache(Duration =25)]
        public ActionResult Login(string email, string password)
        {
             var res = db.Users.FirstOrDefault(x => x.Emailid == email && x.Password ==password );
            if (res != null&&ModelState.IsValid)
            {
                
                FormsAuthentication.SetAuthCookie(email, false);
                Session["Email"] = res.Emailid;
                if (res.Usertype == "buyer") {
                    return RedirectToAction("Index", "Customer");
                }
                 bool isAuth = false;
                
                if (res.Usertype == "seller")
                {
                    
                    return RedirectToAction("Index", "Seller");
                }
                if (res.Usertype == "buyer")
                {
                    ModelState.Clear();

                    return RedirectToAction("Login", "Home");
                     
                }


            }
           

            
                TempData["msg"] = "Incorrect";
                return RedirectToAction("Login", "Home");
            
        }
        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}