using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Keys.Models;

namespace Keys.Controllers
{
    public class CustomersController : Controller
    {
        private MarsDBEntities db = new MarsDBEntities();

        // GET: Customers
        public ActionResult Index()
        {

            return View();
        }

   

        public JsonResult GetAllCustomers()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Customer> customerList = db.Customer.ToList();
            // JavaScriptSerializer js = new JavaScriptSerializer();
            // string strList = js.Serialize(customerList);
           
            return Json(customerList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCustomerById(int Id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            Customer customer = db.Customer.Where(x => x.Id == Id).SingleOrDefault();
            return Json(customer, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int Id)
        {
            Customer customer = db.Customer.Where(x => x.Id == Id).SingleOrDefault();
            return View(customer);
        }

        public JsonResult UpdateCustomer(Customer cus)
        {
            var result = false;
            try
            {
                Customer customer = db.Customer.Where(x => x.Id == cus.Id).SingleOrDefault();
                customer.Name = cus.Name;
                customer.Address = cus.Address;
                db.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddCustomer(Customer cus)
        {
            var result = false;
            try
            {
                db.Customer.Add(cus);
                db.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteCustomer(int id)
        {
            bool result = false;
            Customer customer = db.Customer.Where(x => x.Id == id).SingleOrDefault();
            if (customer!=null)
            {
                db.Customer.Remove(customer);
                db.SaveChanges();
                result = true;
            }
            return Json(customer, JsonRequestBehavior.AllowGet);
        }
    }
}
