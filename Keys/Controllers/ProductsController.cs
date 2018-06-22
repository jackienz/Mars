using Keys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Keys.Controllers
{
    public class ProductsController : Controller
    {
         private hilandDBEntities db = new hilandDBEntities();
       // private MarsDBEntities db = new MarsDBEntities();
          // GET: Products
        public ActionResult Index()
        {

            return View();
        }



        public JsonResult GetAllProducts()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Product> products = db.Product.ToList();
   

            return Json(products, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProductsById(int Id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            Product product = db.Product.Where(x => x.Id == Id).SingleOrDefault();
            return Json(product, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int Id)
        {
            Product product = db.Product.Where(x => x.Id == Id).SingleOrDefault();
            return View(product);
        }

        public JsonResult UpdateProducts(Product pros)
        {
            var result = false;
            try
            {
                Product product = db.Product.Where(x => x.Id == pros.Id).SingleOrDefault();
                product.Name = pros.Name;
                product.Price = pros.Price;
                db.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddProduct(Product pros)
        {
            var result = false;
            try
            {
                db.Product.Add(pros);
                db.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteProduct(int id)
        {
            bool result = false;
            Product product = db.Product.Where(x => x.Id == id).SingleOrDefault();
            if (product != null)
            {
                db.Product.Remove(product);
                db.SaveChanges();
                result = true;
            }
            return Json(product, JsonRequestBehavior.AllowGet);
        }
    }
}
