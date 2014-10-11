using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apartment_Bookeeping.Models;
using Apartment_Bookeeping.DAL;
using System.Data.Entity.Validation;

namespace Apartment_Bookeeping.Controllers
{
    public class ApartmentController : Controller
    {
        private ApartmentContext db = new ApartmentContext();

        //
        // GET: /Default1/

        public ActionResult Index()
        {
            var apartments = db.Apartments.Include(a => a.Location).Include(a => a.Manager).Include(a => a.ApartmentNums);
            return View(apartments.ToList());
        }

        //
        // GET: /Default1/Details/5

        public ActionResult Details(int id = 0)
        {
            Apartment apartment = db.Apartments.Include(a => a.ApartmentNums).Where(a => a.ApartmentID == id).FirstOrDefault();
            if (apartment == null)
            {
                return HttpNotFound();
            }
            return View(apartment);
        }

        public ActionResult CreateAptNum(int id = 0)
        {
            //id used for inserting apartment numbers into the right apartment complex
            ViewBag.AptID = id;
            //used to display the name of the apartment complex that is being affected by the data insertion
            ViewBag.AptName = db.Apartments.Where(a => a.ApartmentID == id).Select(a => a.Name).FirstOrDefault();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAptNum(ApartmentNum apartmentnum)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //if model state is valid but there is an apartment number duplicate in an apartment complex throw error and return to view
                    /*if (db.ApartmentNums.Where(a => a.AptNum == apartmentnum.AptNum).Where(a => a.ApartmentID == apartmentnum.ApartmentID).FirstOrDefault() != null)
                    {
                        ModelState.AddModelError("AptExists", "Apartment Number exists in Apartment Complex. Please enter another number.");
                        //recreate viewbags before returning to view
                        ViewBag.AptName = db.Apartments.Where(a => a.ApartmentID == apartmentnum.ApartmentID).Select(a => a.Name).FirstOrDefault();
                        ViewBag.AptID = db.Apartments.Where(a => a.ApartmentID == apartmentnum.ApartmentID).Select(a => a.ApartmentID).FirstOrDefault();
                        return View(apartmentnum);
                    }*/
                    db.ApartmentNums.Add(apartmentnum);
                    db.SaveChanges();
                    return RedirectToAction("Details", "Apartment", new { id = apartmentnum.ApartmentID });
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var error in ex.EntityValidationErrors)
                {
                    foreach (var err in error.ValidationErrors)
                    {
                        this.ModelState.AddModelError(err.PropertyName, err.ErrorMessage);
                    }
                }
            }

            //recreate viewbags before returning to view
            ViewBag.AptName = db.Apartments.Where(a => a.ApartmentID == apartmentnum.ApartmentID).Select(a => a.Name).FirstOrDefault();
            ViewBag.AptID = db.Apartments.Where(a => a.ApartmentID == apartmentnum.ApartmentID).Select(a => a.ApartmentID).FirstOrDefault();
            return View(apartmentnum);
        }

        public ActionResult EditAptNum(int id = 0)
        {
            //ApartmentNum apartmentnum = db.ApartmentNums.Find(id);
            ApartmentNum apartmentnum = db.ApartmentNums.Include(a => a.Apartment).Where(a => a.ANID == id).FirstOrDefault();//.Find(id);
            if (apartmentnum == null)
            {
                return HttpNotFound();
            }
            //used for selecting the apartment which will be edited (does not work)
            //ViewBag.Apt = new SelectList(db.Apartments, "ApartmentID", "Name");
            return View(apartmentnum);
            }

        //
        // POST: /Default1/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAptNum(ApartmentNum apartmentnum)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(apartmentnum).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Details", "Apartment", new { id = apartmentnum.ApartmentID });
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var error in ex.EntityValidationErrors)
                {
                    foreach (var err in error.ValidationErrors)
                    {
                        this.ModelState.AddModelError(err.PropertyName, err.ErrorMessage);
                    }
                }
            }
            //recreate viewbags before returning to view
            ViewBag.AptName = db.Apartments.Where(a => a.ApartmentID == apartmentnum.ApartmentID).Select(a => a.Name).FirstOrDefault();
            ViewBag.AptID = db.Apartments.Where(a => a.ApartmentID == apartmentnum.ApartmentID).Select(a => a.ApartmentID).FirstOrDefault();
            return View(apartmentnum);
        }

        public ActionResult DeleteAptNum(int id = 0)
        {
            ApartmentNum apartment = db.ApartmentNums.Find(id);
            if (apartment == null)
            {
                return HttpNotFound();
            }
            return View(apartment);
        }

        //
        // POST: /Default1/Delete/5

        [HttpPost, ActionName("DeleteAptNum")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAptNumConfirmed(int id)
        {
            ApartmentNum apartmentnum = db.ApartmentNums.Find(id);
            db.ApartmentNums.Remove(apartmentnum);
            db.SaveChanges();
            //return to details page of affected apartment
            return RedirectToAction("Details", "Apartment", new { id = apartmentnum.ApartmentID });
        }

        //
        // GET: /Default1/Create

        public ActionResult Create()
        {
            //used to select apartment complex's manager
            ViewBag.PersonID = new SelectList(db.Managers, "PersonID", "FullName");
            return View();
        }

        //
        // POST: /Default1/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Apartment apartment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Apartments.Add(apartment);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var error in ex.EntityValidationErrors)
                {
                    foreach (var err in error.ValidationErrors)
                    {
                        this.ModelState.AddModelError(err.PropertyName, err.ErrorMessage);
                    }
                }
            }

            //selects current manager before return to view
            ViewBag.PersonID = new SelectList(db.Managers, "PersonID", "FullName", apartment.PersonID);
            return View(apartment);
        }

        //
        // GET: /Default1/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Apartment apartment = db.Apartments.Find(id);
            if (apartment == null)
            {
                return HttpNotFound();
            }

            ViewBag.PersonID = new SelectList(db.Managers, "PersonID", "FullName", apartment.PersonID);
            return View(apartment);
        }

        //
        // POST: /Default1/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Apartment apartment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(apartment).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var error in ex.EntityValidationErrors)
                {
                    foreach (var err in error.ValidationErrors)
                    {
                        this.ModelState.AddModelError(err.PropertyName, err.ErrorMessage);
                    }
                }
            }

            ViewBag.PersonID = new SelectList(db.Managers, "PersonID", "FullName", apartment.PersonID);
            return View(apartment);
        }

        //
        // GET: /Default1/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Apartment apartment = db.Apartments.Find(id);
            if (apartment == null)
            {
                return HttpNotFound();
            }
            return View(apartment);
        }

        //
        // POST: /Default1/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Apartment apartment = db.Apartments.Find(id);
            db.Apartments.Remove(apartment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Stuff()
        {
            var apartment = db.ApartmentNums.Include(a => a.Apartment).Where(a => a.ApartmentID >= 2).FirstOrDefault();
            return View(apartment);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}