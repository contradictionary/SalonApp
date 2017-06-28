using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using sqlexpressmvc.Models;

namespace sqlexpressmvc.Controllers
{
    public class UsedProductsController : Controller
    {
        private SalonContext db = new SalonContext();

        // GET: UsedProducts
        public ActionResult Index()
        {
            var usedProducts = db.UsedProducts.Include(u => u.Stock).Include(u=>u.Employeee);
            return View(usedProducts.ToList());
        }

        // GET: UsedProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsedProducts usedProducts = db.UsedProducts.Find(id);
            if (usedProducts == null)
            {
                return HttpNotFound();
            }
            return View(usedProducts);
        }

        // GET: UsedProducts/Create
        public ActionResult Create()
        {

            //Check if Qty is already 0 and skip that item from list
            var stockdata = db.Stocks.Where(s => s.qty > 0);

            ViewBag.StockID = new SelectList(stockdata, "StockID", "Title");
            ViewBag.EmployeesID = new SelectList(db.Employees, "EmployeesID", "FullName");

            return View();
        }

        // POST: UsedProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude  = "Usedqty,Usedvolume,dateAdded,dateModified")] UsedProducts usedProducts)
        {
            var ogStock = db.Stocks.Find(usedProducts.StockID);
            if (ModelState.IsValid)
            {
                usedProducts.Usedvolume = ogStock.volume;
                usedProducts.Usedqty = 1;
                ogStock.qty = ogStock.qty -1;                
                db.Entry(ogStock).State = EntityState.Modified;                
                usedProducts.dateAdded = DateTime.Now;
                usedProducts.dateModified = new DateTime();
                db.UsedProducts.Add(usedProducts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StockID = new SelectList(db.Stocks, "StockID", "Title", usedProducts.StockID);
            ViewBag.EmployeesID = new SelectList(db.Employees, "EmployeesID", "FullName", usedProducts.EmployeesID);
            return View(usedProducts);
        }


        /// <summary>
        /// Validation Disabled as by defaul only qty will be allowed to be used.
        /// </summary>
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.None, NoStore = true)]
        //public ActionResult ValidateNewQty(int? Usedqty, int? StockID)
        //{            
        //    var stockqty = db.Stocks.Find(StockID).qty;
        //    var nqty = (int)Usedqty;

        //  if (nqty >= 1 && nqty <= stockqty)
        //    {
        //        return Json(true, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(false, JsonRequestBehavior.AllowGet);
        //    }

           
        //}


        // GET: UsedProducts/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    UsedProducts usedProducts = db.UsedProducts.Find(id);
        //    if (usedProducts == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.StockID = new SelectList(db.Stocks, "StockID", "Title", usedProducts.StockID);
        //    return View(usedProducts);
        //}

        //// POST: UsedProducts/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "UsedProductsID,StockID,Usedqty,Usedvolume,dateAdded,dateModified")] UsedProducts usedProducts)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(usedProducts).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.StockID = new SelectList(db.Stocks, "StockID", "Title", usedProducts.StockID);
        //    return View(usedProducts);
        //}

        // GET: UsedProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsedProducts usedProducts = db.UsedProducts.Find(id);
            if (usedProducts == null)
            {
                return HttpNotFound();
            }
            return View(usedProducts);
        }

        // POST: UsedProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UsedProducts usedProducts = db.UsedProducts.Find(id);
            db.UsedProducts.Remove(usedProducts);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UpdateVolume(int? id)
        {
            ViewBag.EmployeesID = db.Employees.ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsedProducts stock = db.UsedProducts.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeesID = new SelectList(db.Employees, "EmployeesID", "FullName", stock.EmployeesID);
            return View(stock);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateVolume([Bind(Exclude = "dateModified")] UsedProducts stock, string newVolume)
        {
            ////TODO : Add Validation to prevent negative values to be updated
            ViewBag.EmployeesID = new SelectList(db.Employees, "EmployeesID", "FullName", stock.EmployeesID);
            int s = int.Parse(newVolume);

            //if (!(newVolume == null))
            //{
            //    s= (int)newVolume;
            //}
            if (ModelState.IsValid)
            {
                if (s <= 0)
                {
                    return Content("<script language='javascript' type='text/javascript'>alert('Couldnt update Volume as new Volume is 0 ! Go Back and try Agains!');</script>");
                }
                else
                if (s > stock.Usedvolume)
                {
                    return Content("<script language='javascript' type='text/javascript'>alert('Couldnt update Volume as new Volume is Greater than Current Volume ! Go Back and try Agains!');</script>");
                }
                else
                {
                    stock.dateModified = DateTime.Now;
                    stock.Usedvolume = stock.Usedvolume - s;
                    db.Entry(stock).State = EntityState.Modified;

                    var newupdtmdl = new UpdateVolume();
                    newupdtmdl.dateUpdated = DateTime.Now;
                    newupdtmdl.EmployeesID = stock.EmployeesID;
                    newupdtmdl.newVolume = s;
                    newupdtmdl.UsedProductsID = stock.UsedProductsID;
                    newupdtmdl.Employee = db.Employees.Find(stock.EmployeesID);
                    newupdtmdl.UsedProducts = db.UsedProducts.Find(stock.UsedProductsID);

                    db.UpdateVolume.Add(newupdtmdl);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(stock);
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
