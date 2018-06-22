using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Keys.Models;

namespace Keys.Controllers
{
    public class SalesController : Controller
    {
        // private MarsDBEntities db = new MarsDBEntities();
        private hilandDBEntities db = new hilandDBEntities();


        // GET: Sales
        public ActionResult Index()
        {
            var productSold = db.ProductSold.Include(p => p.Customer).Include(p => p.Product).Include(p => p.Stroe);
            return View(productSold.ToList());
        }

        // GET: Sales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSold productSold = db.ProductSold.Find(id);
            if (productSold == null)
            {
                return HttpNotFound();
            }
            return View(productSold);
        }

        // GET: Sales/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customer, "Id", "Name");
            ViewBag.ProductId = new SelectList(db.Product, "Id", "Name");
            ViewBag.StoreId = new SelectList(db.Stroe, "Id", "Name");
            return View();
        }

        // POST: Sales/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductId,CustomerId,StoreId,DateSold")] ProductSold productSold)
        {
            productSold.DateSold = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.ProductSold.Add(productSold);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customer, "Id", "Name", productSold.CustomerId);
            ViewBag.ProductId = new SelectList(db.Product, "Id", "Name", productSold.ProductId);
            ViewBag.StoreId = new SelectList(db.Stroe, "Id", "Name", productSold.StoreId);
            return View(productSold);
        }

        // GET: Sales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSold productSold = db.ProductSold.Find(id);
            if (productSold == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customer, "Id", "Name", productSold.CustomerId);
            ViewBag.ProductId = new SelectList(db.Product, "Id", "Name", productSold.ProductId);
            ViewBag.StoreId = new SelectList(db.Stroe, "Id", "Name", productSold.StoreId);
            return View(productSold);
        }




        public JsonResult GetSalesById(int salesId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            ProductSold product = db.ProductSold.Where(x => x.Id == salesId).SingleOrDefault();
            return Json(product, JsonRequestBehavior.AllowGet);
        }




        // POST: Sales/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductId,CustomerId,StoreId,DateSold")] ProductSold productSold)
        {
            productSold.DateSold = DateTime.Now;
            //if (ModelState.IsValid)
            //{
            //    db.Entry(productSold).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            db.Entry(productSold).State = EntityState.Modified;
            db.SaveChanges();

            ViewBag.CustomerId = new SelectList(db.Customer, "Id", "Name", productSold.CustomerId);
            ViewBag.ProductId = new SelectList(db.Product, "Id", "Name", productSold.ProductId);
            ViewBag.StoreId = new SelectList(db.Stroe, "Id", "Name", productSold.StoreId);

        
            return View(productSold);
        }

        // GET: Sales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSold productSold = db.ProductSold.Find(id);
            if (productSold == null)
            {
                return HttpNotFound();
            }
            return View(productSold);
        }

        // POST: Sales/Delete/5
       // [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductSold productSold = db.ProductSold.Find(id);
            db.ProductSold.Remove(productSold);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
