using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apartment_Bookeeping.Models;
using Apartment_Bookeeping.DAL;
using Apartment_Bookeeping.ViewModels;
using System.Data.Entity.Validation;
using System.Data.SqlClient;

namespace Apartment_Bookeeping.Controllers
{
    public class ManagerController : Controller
    {
        private ApartmentContext db = new ApartmentContext();

        //
        // GET: /Manager/

        public ActionResult Index()
        {
            var managers = db.Managers.Include(m => m.Location).Include(m => m.Apartments);
            return View(managers.ToList());
        }

        //
        // GET: /Manager/Details/5

        public ActionResult Details(int id = 0)
        {
            Manager manager = db.Managers.Include(m => m.Location).Include(m => m.Apartments).Where(m => m.PersonID == id).FirstOrDefault();//db.Managers.Find(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            return View(manager);
        }

        //
        // GET: /Manager/Create

        public ActionResult Create()
        {
            //not used
            //ViewBag.Apt = new SelectList(db.Apartments.Where(a => a.PersonID == null), "ApartmentID", "Name");
            return View();
        }

        //
        // POST: /Manager/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Manager manager)
        {
            try
            {
                if (ModelState.IsValid)
                {
                db.Managers.Add(manager);
                db.SaveChanges();
                return RedirectToAction("Index");
                }
                /*
                var newman = new Manager
                {
                    FirstName = manager.FirstName,
                    LastName = manager.LastName,
                    SSN = manager.SSN,
                    DOB = manager.DOB,
                    PhoneNumber = manager.PhoneNumber,
                    Address = manager.Address,
                    ZipCode = manager.ZipCode
                };

                db.Managers.Add(newman);
                //db.SaveChanges();
                
                //update apartment table to include added manager in the table
                var apt = db.Apartments.Where(a => a.ApartmentID == manager.AptID).FirstOrDefault();
                apt.PersonID = db.Managers.Where(m => m.SSN == manager.SSN).Select(m => m.PersonID).FirstOrDefault();
                db.Entry(apt).State = EntityState.Modified;
                db.SaveChanges();
                
                return RedirectToAction("Index");*/
            }
            //catching sql server errors like constraint errors
            /*catch (SqlException ex)
            {
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    //this.ModelState.AddModelError(ex.Errors[i]);
                }
                //this.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }*/
            //catching errors that violate fluent api rules
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
            //ViewBag.ZipCode = new SelectList(db.Apartments.Where(a => a.PersonID == null), "ApartmentID", "Name", manager.ApartmentID);

            return View(manager);
        }

        //
        // GET: /Manager/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Manager manager = db.Managers.Include(m => m.Location).Include(m => m.Apartments).Where(m => m.PersonID == id).FirstOrDefault(); //db.Managers.Find(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            //ViewBag.ZipCode = new SelectList(db.Locations, "ZipCode", "City", manager.ZipCode);
            return View(manager);
        }

        //
        // POST: /Manager/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Manager manager)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(manager).State = EntityState.Modified;
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

            //ViewBag.ZipCode = new SelectList(db.Locations, "ZipCode", "City", manager.ZipCode);
            return View(manager);
        }

        //
        // GET: /Manager/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Manager manager = db.Managers.Find(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            return View(manager);
        }

        //
        // POST: /Manager/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IQueryable<Apartment> apt = db.Apartments.Where(a => a.PersonID == id);

            foreach (var item in apt)
            {
                item.PersonID = null;
                db.Entry(item).State = EntityState.Modified;
            }

            db.SaveChanges();

            Manager manager = db.Managers.Find(id);
            db.Managers.Remove(manager);
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