using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apartment_Bookeeping.Models;
using Apartment_Bookeeping.DAL;

namespace Apartment_Bookeeping.Controllers
{
    public class BillController : Controller
    {
        private ApartmentContext db = new ApartmentContext();

        //
        // GET: /Bill/

        public ActionResult Index()
        {
            ViewBag.Year = new SelectList(db.Bills, db.Bills.Select(b=>b.StatementDate.Year));
            //var bills = db.Bills.Include(b => b.Apartment).Include(b => b.BillingCompany);
            //return View(bills.ToList());
            var companies = db.BillingCompanies.Include(bc => bc.Location);
            return View(companies.ToList());
        }

        //
        // GET: /Bill/Details/5

        public ActionResult Details(int id = 0)
        {
            var bill = db.Bills.Include(b=>b.BillingCompany).Include(b=>b.Apartment).Where(b => b.BillingCompanyID == id).OrderBy(b=>b.StatementDate);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(bill.ToList());
        }

        //
        // GET: /Bill/Create

        public ActionResult Create()
        {
            ViewBag.ApartmentID = new SelectList(db.Apartments, "ApartmentID", "Name");
            ViewBag.BillingCompanyID = new SelectList(db.BillingCompanies, "BillingCompanyID", "Name");
            return View();
        }

        //
        // POST: /Bill/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Bill bill)
        {
            if (ModelState.IsValid)
            {
                db.Bills.Add(bill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApartmentID = new SelectList(db.Apartments, "ApartmentID", "Name", bill.ApartmentID);
            ViewBag.BillingCompanyID = new SelectList(db.BillingCompanies, "BillingCompanyID", "Name", bill.BillingCompanyID);
            return View(bill);
        }

        //
        // GET: /Bill/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApartmentID = new SelectList(db.Apartments, "ApartmentID", "Name", bill.ApartmentID);
            ViewBag.BillingCompanyID = new SelectList(db.BillingCompanies, "BillingCompanyID", "Name", bill.BillingCompanyID);
            return View(bill);
        }

        //
        // POST: /Bill/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Bill bill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApartmentID = new SelectList(db.Apartments, "ApartmentID", "Name", bill.ApartmentID);
            ViewBag.BillingCompanyID = new SelectList(db.BillingCompanies, "BillingCompanyID", "Name", bill.BillingCompanyID);
            return View(bill);
        }

        //
        // GET: /Bill/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(bill);
        }

        //
        // POST: /Bill/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bill bill = db.Bills.Find(id);
            db.Bills.Remove(bill);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}