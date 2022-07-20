using Rotativa;
using SpadesGroceryShop.Models;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace SpadesGroceryShop.Controllers
{
    public class ManagerController : Controller
    {
        // GET: Manager
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
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Manager u)
        {
            using (ManagerOfSpades db = new ManagerOfSpades())
            {
                var UserDetails = db.Managers.Where(x => x.Email == u.Email && x.Password == u.Password).FirstOrDefault();
                if(UserDetails == null)
                {
                    u.LoginErrorMessage = "Wrong User email or Password";
                    return View("Login",u);
                }
                else
                {
                    FormsAuthentication.SetAuthCookie("data", true);
                    //Session["user_mobile"] = user.Mobile;
                    
                    Session["username"] = UserDetails.Email;
                    return RedirectToAction("Home","Manager");
                }
            }
        }

        public ActionResult Home()
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

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        public ActionResult CustomerIndex()
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
            var db = new ManagerOfSpades();
            var customers = db.Customers.ToList();

            return View(customers);
        }
        public ActionResult CustomerDetails()
        {
            var db = new ManagerOfSpades();
            var customers = db.Customers.ToList();
            return View(customers);
        }
        public ActionResult PrintAll1()
        {
            var Q = new ActionAsPdf("CustomerDetails");
            return Q;
        }
        
        [HttpGet]
        public ActionResult AddCustomer()
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
        [HttpPost]
        public ActionResult AddCustomer(Customer p)
        {
            var db = new ManagerOfSpades();
            if (ModelState.IsValid)
            {
                db.Customers.Add(p);
                db.SaveChanges();

                return RedirectToAction("CustomerIndex");
            }
            return View(p);
        }

        public ActionResult EditCustomer(int id)
        {  
            var db = new ManagerOfSpades();
            var customer = (from p in db.Customers where p.Customer_Id == id select p).SingleOrDefault();

            return View(customer);
        }

        [HttpPost]
        public ActionResult UpdateCustomer(Customer p)
        {

            try
            {

                using (ManagerOfSpades db = new ManagerOfSpades())
                {
                    /*Customer entity = (from pr in db.Customers where pr.Customer_Id == p.Customer_Id select pr).SingleOrDefault();*/
                    var customer = (from pr in db.Customers where pr.Customer_Id == p.Customer_Id select pr).SingleOrDefault();
                    customer.Name = p.Name;
                    customer.Email = p.Email;
                    customer.Phone = p.Phone;
                    customer.Address = p.Address;

                    //db.Entry(p).State = System.Data.EntityState.Modified;

                    db.SaveChanges();


                }
            }
            catch
            {
                return RedirectToAction("CustomerIndex");

            }


            return View();

        }

        
        public ActionResult DeleteCustomer(int id)
        {

            var db = new ManagerOfSpades();
            var customer = (from p in db.Customers where p.Customer_Id == id select p).SingleOrDefault();
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("CustomerIndex");
        }
        public ActionResult EmployeeIndex()
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
            var db = new ManagerOfSpades();
            var Employee = db.Employees.ToList();

            return View(Employee);
        }
        public ActionResult EmployeeDetails()
        {
            var db = new ManagerOfSpades();
            var e = db.Employees.ToList();
            return View(e);
        }
        public ActionResult PrintAll()
        {

            var p = new ActionAsPdf("EmployeeIndex");
            return p;
        }

        [HttpGet]
        public ActionResult AddEmployee()
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
        [HttpPost]
        public ActionResult AddEmployee(Employee p)
        {
            var db = new ManagerOfSpades();
            if (ModelState.IsValid)
            {
                db.Employees.Add(p);
                db.SaveChanges();

                return RedirectToAction("EmployeeIndex");
            }
            return View(p);
        }
        public ActionResult DeleteEmployee(int id)
        {
            var db = new ManagerOfSpades();
            var employee = (from p in db.Employees where p.Emp_Id == id select p).SingleOrDefault();
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("EmployeeIndex");
        }
        [HttpGet]
        public ActionResult EditEmployee(int id)
        {
            var db = new ManagerOfSpades();
            var employee = (from p in db.Employees where p.Emp_Id == id select p).SingleOrDefault();

            return View(employee);
        }

        [HttpPost]
        public ActionResult EditEmployee(Employee p)
        {
            using (ManagerOfSpades db = new ManagerOfSpades())
            {
                Employee entity = (from pr in db.Employees where pr.Emp_Id == p.Emp_Id select pr).SingleOrDefault();

                db.Entry(entity).CurrentValues.SetValues(p);

                db.SaveChanges();
                return RedirectToAction("CustomerIndex");
            }
        }


    }

}