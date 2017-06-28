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
    public class MembershipBillsController : Controller
    {
        private SalonContext db = new SalonContext();

        // GET: MembershipBills
        public ActionResult Index()
        {
            var membershipBills = db.MembershipBills.Include(m => m.Employee).Include(m => m.Member);
            return View(membershipBills.ToList());
        }

        // GET: MembershipBills/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MembershipBill membershipBill = db.MembershipBills.Find(id);
            if (membershipBill == null)
            {
                return HttpNotFound();
            }
            return View(membershipBill);
        }

        // GET: MembershipBills/Create
        public ActionResult Create()
        {
            ViewBag.EmployeesID = new SelectList(db.Employees, "EmployeesID", "fname");
            ViewBag.MemberID = new SelectList(db.Members, "MemberID", "fname");
            return View();
        }

        // POST: MembershipBills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude  = "Amount,datesold")] MembershipBill membershipBill)
        {
            if (ModelState.IsValid)
            {
                membershipBill.Amount = 500;
                membershipBill.datesold = DateTime.Now;
                db.MembershipBills.Add(membershipBill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeesID = new SelectList(db.Employees, "EmployeesID", "fname", membershipBill.EmployeesID);
            ViewBag.MemberID = new SelectList(db.Members, "MemberID", "fname", membershipBill.MemberID);
            return View(membershipBill);
        }

        // GET: MembershipBills/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    MembershipBill membershipBill = db.MembershipBills.Find(id);
        //    if (membershipBill == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.EmployeesID = new SelectList(db.Employees, "EmployeesID", "fname", membershipBill.EmployeesID);
        //    ViewBag.MemberID = new SelectList(db.Members, "MemberID", "fname", membershipBill.MemberID);
        //    return View(membershipBill);
        //}

        // POST: MembershipBills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "MembershipBillID,MemberID,EmployeesID,Amount,datesold")] MembershipBill membershipBill)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(membershipBill).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.EmployeesID = new SelectList(db.Employees, "EmployeesID", "fname", membershipBill.EmployeesID);
        //    ViewBag.MemberID = new SelectList(db.Members, "MemberID", "fname", membershipBill.MemberID);
        //    return View(membershipBill);
        //}

        // GET: MembershipBills/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    MembershipBill membershipBill = db.MembershipBills.Find(id);
        //    if (membershipBill == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(membershipBill);
        //}

        // POST: MembershipBills/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    MembershipBill membershipBill = db.MembershipBills.Find(id);
        //    db.MembershipBills.Remove(membershipBill);
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
