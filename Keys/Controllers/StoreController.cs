using Keys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Keys.Controllers
{
    public class StoreController : Controller
    {
       // private MarsDBEntities db = new MarsDBEntities();
       private hilandDBEntities db = new hilandDBEntities();
   

        public ActionResult Index()
        {

            return View();
        }



        public JsonResult GetAllStores()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Stroe> stroes = db.Stroe.ToList();
            // JavaScriptSerializer js = new JavaScriptSerializer();
            // string strList = js.Serialize(customerList);

            return Json(stroes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStoreById(int Id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            Stroe stroe = db.Stroe.Where(x => x.Id == Id).SingleOrDefault();
            return Json(stroe, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int Id)
        {
            Stroe customer = db.Stroe.Where(x => x.Id == Id).SingleOrDefault();
            return View(customer);
        }

        public JsonResult UpdateStore(Stroe cus)
        {
            var result = false;
            try
            {
                Stroe stroe = db.Stroe.Where(x => x.Id == cus.Id).SingleOrDefault();
                stroe.Name = cus.Name;
                stroe.Address = cus.Address;
                db.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddStore(Stroe cus)
        {
            var result = false;
            try
            {
                db.Stroe.Add(cus);
                db.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteStore(int id)
        {
            bool result = false;
            Stroe stroe = db.Stroe.Where(x => x.Id == id).SingleOrDefault();
            if (stroe != null)
            {
                db.Stroe.Remove(stroe);
                db.SaveChanges();
                result = true;
            }
            return Json(stroe, JsonRequestBehavior.AllowGet);
        }
    }
}