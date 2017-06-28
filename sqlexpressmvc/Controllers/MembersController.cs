using sqlexpressmvc.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace sqlexpressmvc.Controllers
{
    public class MembersController : Controller
    {
        private SalonContext db = new SalonContext();

        // GET: Members
        public ActionResult Index( string searchString, string currentFilter, string sortOrder,int? page)
        {
            var Result = from mems in db.Members
                         select mems;

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
                    Result = Result.OrderByDescending(s => s.fname);
                    break;
                case "Date":
                    Result = Result.OrderBy(s => s.dateAdded);
                    break;
                case "date_desc":
                    Result = Result.OrderByDescending(s => s.dateAdded);
                    break;                
                default:
                    Result = Result.OrderBy(s => s.lname);
                    break;
            }

            
            if (!String.IsNullOrEmpty(searchString))
            {
                Result = Result.Where(s => s.mobile.Contains(searchString));
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(Result.ToPagedList(pageNumber, pageSize));

            //return View(db.Members.ToList());
        }

        // GET: Members/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Members/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude  = "dateAdded,dateModified,mexpirydate")] Member member)
        {
            if (ModelState.IsValid)
            {
                member.mexpirydate = DateTime.Now.AddYears(1);
                member.dateAdded = DateTime.Now;
                member.dateModified = new DateTime();
                db.Members.Add(member);
                db.SaveChanges();
                member.MembershipID = 1000 + member.MemberID;
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(member);
        }

       

        // GET: Members/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Exclude  = "dateModified")] Member member)
        {
            if (ModelState.IsValid)
            {
                member.dateModified = DateTime.Now;                
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(member);
        }

        // GET: Members/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Member member = db.Members.Find(id);
        //    if (member == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(member);
        //}

        // POST: Members/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Member member = db.Members.Find(id);
        //    db.Members.Remove(member);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

         public ActionResult RenewMembership(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RenewMembership([Bind(Exclude  = "dateModified")] Member member)
        {
            if (ModelState.IsValid)
            {
                var s = DateTime.Compare((DateTime)member.mexpirydate.Value.AddDays(-1), DateTime.Now);
                return Content("<script language='javascript' type='text/javascript'>alert('Dont know how to validate the expiry date with the Current, Hence not implemented Yet :(  -Comparison Result : "+s+"');</script>");

                //member.mexpirydate = DateTime.Now.AddYears(1);
                //db.Entry(member).State = EntityState.Modified;
                //db.SaveChanges();
                //return RedirectToAction("Index");
            }
            return View(member);
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
