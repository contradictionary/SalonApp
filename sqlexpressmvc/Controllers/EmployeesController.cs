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
    public class EmployeesController : Controller
    {
        private SalonContext db = new SalonContext();

        // GET: Employees
        public ActionResult Index(string currentFilter, string sortOrder)
        {
            var Result = from s in db.Employees
                         select s;
           
            ViewBag.CurrentSort = sortOrder;
            ViewBag.FNAMESPM = String.IsNullOrEmpty(sortOrder) ? "fname_desc" : "";
            ViewBag.DATEADDEDSPM = String.IsNullOrEmpty(sortOrder) ? "lname_dsc" : "";


            switch (sortOrder)
            {
                case "fname_desc":
                    Result = Result.OrderByDescending(s => s.fname);
                    break;
                case "fName":
                    Result = Result.OrderBy(s => s.fname);
                    break;
                case "lname_dsc":
                    Result = Result.OrderByDescending(s => s.lname);
                    break;
                case "lname":
                    Result = Result.OrderBy(s => s.lname);break;

                default:
                    Result = Result.OrderBy(s => s.dateAdded);
                    break;
            }


            return View(Result.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = db.Employees.Find(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            return View(employees);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude  = "dateAdded,dateModified")] Employees employees)
        {
            if (ModelState.IsValid)
            {
                employees.dateAdded= DateTime.Now;
                employees.dateModified = new DateTime();
                db.Employees.Add(employees);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employees);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = db.Employees.Find(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            return View(employees);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Exclude = "dateModified")] Employees employees)
        {
           


            if (ModelState.IsValid)
            {                               
                employees.dateModified= DateTime.Now;
                db.Entry(employees).State = EntityState.Modified;
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employees);
        }

        //// GET: Employees/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Employees employees = db.Employees.Find(id);
        //    if (employees == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(employees);
        //}

        //// POST: Employees/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Employees employees = db.Employees.Find(id);
        //    db.Employees.Remove(employees);
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
