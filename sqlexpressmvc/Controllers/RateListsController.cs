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
    public class RateListsController : Controller
    {

        private SalonContext db = new SalonContext();

        // GET: RateLists
        public ActionResult Index(string searchString, string currentFilter, string sortOrder, int? page)
        {
            var Result = from s in db.RateLists
                         select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                Result = Result.Where(s => s.Title.Contains(searchString));
            }

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
                case "date_dsc":
                    Result = Result.OrderByDescending(s => s.dateAdded);
                    break;
                default:
                    Result = Result.OrderBy(s => s.Title);
                    break;
            }

            int pageSize = 20;
            int pageNumber = (page ?? 1);

            return View(Result.ToPagedList(pageNumber, pageSize));
        }

        // GET: RateLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RateList rateList = db.RateLists.Find(id);
            if (rateList == null)
            {
                return HttpNotFound();
            }
            return View(rateList);
        }

        //public ActionResult SearchByMobile(string mobile)
        //{
        //}
        // GET: RateLists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RateLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "dateAdded,dateModified")] RateList rateList)
        {
            if (ModelState.IsValid)
            {
                rateList.dateAdded = DateTime.Now;
                rateList.dateModified = new DateTime();
                db.RateLists.Add(rateList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rateList);
        }

        // GET: RateLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RateList rateList = db.RateLists.Find(id);
            if (rateList == null)
            {
                return HttpNotFound();
            }
            return View(rateList);
        }

        // POST: RateLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Exclude ="dateModified")] RateList rateList)
        {
            if (ModelState.IsValid)
            {
                rateList.dateModified = DateTime.Now;
                db.Entry(rateList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rateList);
        }

        //// GET: RateLists/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    RateList rateList = db.RateLists.Find(id);
        //    if (rateList == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(rateList);
        //}

        //// POST: RateLists/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    RateList rateList = db.RateLists.Find(id);
        //    db.RateLists.Remove(rateList);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
