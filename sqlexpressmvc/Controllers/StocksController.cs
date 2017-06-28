using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using sqlexpressmvc.Models;
using PagedList.Mvc;
using PagedList;

namespace sqlexpressmvc.Controllers
{
    public class StocksController : Controller
    {
        private SalonContext db = new SalonContext();


        // GET: Stocks
        public ActionResult Index(string searchString, string currentFilter, string sortOrder, int? page)
        {
            var Result = from stc in db.Stocks
                         select stc;
            ViewBag.StockList = Result;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            switch (sortOrder)
            {
                case "name_desc":
                    Result = Result.OrderByDescending(s => s.Title);
                    break;
                case "Date":
                    Result = Result.OrderBy(s => s.dateAdded);
                    break;
                case "date_desc":
                    Result = Result.OrderByDescending(s => s.dateAdded);
                    break;
                default:
                    Result = Result.OrderBy(s => s.Title);
                    break;
            }


            if (!String.IsNullOrEmpty(searchString))
            {
                Result = Result.Where(s => s.Title.Contains(searchString));
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(Result.ToPagedList(pageNumber, pageSize));
        }

        // GET: Stocks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // GET: Stocks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude  = "dateAdded,dateModified")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                stock.dateAdded = DateTime.Now;
                stock.dateModified = new DateTime();
                db.Stocks.Add(stock);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stock);
        }

       // GET: Stocks/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Stock stock = db.Stocks.Find(id);
        //    if (stock == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(stock);
        //}

        // POST: Stocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Exclude = "dateModified")] Stock stock)
        //{
        //    if (ModelState.IsValid)
        //    {                
        //            stock.dateModified = DateTime.Now;
        //            db.Entry(stock).State = EntityState.Modified;
        //            db.SaveChanges();
        //            return RedirectToAction("Index");     
        //    }
        //    return View(stock);
        //}

        // GET: Stocks/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Stock stock = db.Stocks.Find(id);
        //    if (stock == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(stock);
        //}

        // POST: Stocks/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Stock stock = db.Stocks.Find(id);
        //    db.Stocks.Remove(stock);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}


        public ActionResult AddPurchase(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPurchase([Bind(Exclude = "dateModified")] Stock stock,int? newqty)
        {
            if (newqty==null)
            {
                newqty = 0;
            }
           
            if (ModelState.IsValid)
            {
                if (newqty <= 0)
                {
                    return Content("<script language='javascript' type='text/javascript'>alert('Can not AddPurchase as new Quantity is 0 !');</script>");
                    
                }
                else
                {
                    stock.dateModified = DateTime.Now;
                    stock.qty = stock.qty + (int)newqty;
                    db.Entry(stock).State = EntityState.Modified;
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
