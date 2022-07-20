using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpadesGroceryShop.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                if (Session["username"] == null)
                {
                    return RedirectToAction("Login", "Manager");
                }
            }
            catch
            {
                return RedirectToAction("Login", "Manager");
            }
            
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
    }
}