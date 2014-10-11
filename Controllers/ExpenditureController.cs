using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Apartment_Bookeeping.Models;
using Apartment_Bookeeping.DAL;
using System.Data.Entity.Validation;

namespace Apartment_Bookeeping.Controllers
{
    public class ExpenditureController : Controller
    {
        private ApartmentContext db = new ApartmentContext();

        //
        // GET: /Expenditure/

        public ActionResult Index()
        {
            return View(db.Expenditures.ToList());
        }

        //
        // GET: /Expenditure/Details/5

        public ActionResult Details(int id = 0)
        {
            Expenditure expenditure = db.Expenditures.Include(e => e.ExpDetails).Where(e => e.ExpenditureID == id).FirstOrDefault();
            if (expenditure == null)
            {
                return HttpNotFound();
            }
            return View(expenditure);
        }

        //
        // GET: /Expenditure/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Expenditure/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Expenditure expenditure)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Expenditures.Add(expenditure);
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

            return View(expenditure);
        }

        //
        // GET: /Expenditure/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Expenditure expenditure = db.Expenditures.Find(id);
            if (expenditure == null)
            {
                return HttpNotFound();
            }
            return View(expenditure);
        }

        //
        // POST: /Expenditure/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Expenditure expenditure)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(expenditure).State = EntityState.Modified;
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

            return View(expenditure);
        }

        //
        // GET: /Expenditure/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Expenditure expenditure = db.Expenditures.Find(id);
            if (expenditure == null)
            {
                return HttpNotFound();
            }
            return View(expenditure);
        }

        //
        // POST: /Expenditure/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Expenditure expenditure = db.Expenditures.Find(id);
            db.Expenditures.Remove(expenditure);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddNewItem(int id)
        {
            ViewBag.ExpId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewItem(ExpDetail expdetail)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.ExpDetails.Add(expdetail);
                    db.SaveChanges();
                    
                    var totalcost = db.ExpDetails.Where(e => e.ExpenditureID == expdetail.ExpenditureID).Sum(e => e.Cost);
                    var exp = db.Expenditures.Where(e => e.ExpenditureID == expdetail.ExpenditureID).FirstOrDefault();
                    exp.TotalCost = totalcost;
                    db.Entry(exp).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Details", "Expenditure", new { id = expdetail.ExpenditureID });
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

            ViewBag.ExpId = expdetail.ExpenditureID;
            return View(expdetail);
        }

        public ActionResult EditExp(int id)
        {
            ExpDetail exp = db.ExpDetails.Where(e => e.ExpDetailID == id).FirstOrDefault();
            return View(exp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditExp(ExpDetail expdetail)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(expdetail).State = EntityState.Modified;
                    db.SaveChanges();

                    decimal totalcost = db.ExpDetails.Where(e => e.ExpenditureID == expdetail.ExpenditureID).Sum(e => e.Cost);
                    var exp = db.Expenditures.Where(e => e.ExpenditureID == expdetail.ExpenditureID).FirstOrDefault();
                    exp.TotalCost = totalcost;
                    db.Entry(exp).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Details", "Expenditure", new { id = expdetail.ExpenditureID });
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

            return View(expdetail);
        }

        public ActionResult DeleteExp(int id)
        {
            ExpDetail exp = db.ExpDetails.Where(e => e.ExpDetailID == id).FirstOrDefault();
            if (exp == null)
            {
                return HttpNotFound();
            }
            return View(exp);
        }

        [HttpPost, ActionName("DeleteExp")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteExpConfirmed(int id)
        {
            ExpDetail exp = db.ExpDetails.Where(e => e.ExpDetailID == id).FirstOrDefault();
            db.ExpDetails.Remove(exp);
            db.SaveChanges();

            decimal totalcost = db.ExpDetails.Where(e => e.ExpenditureID == exp.ExpenditureID).Sum(e => e.Cost);
            Expenditure expenditure = db.Expenditures.Where(e => e.ExpenditureID == exp.ExpenditureID).FirstOrDefault();
            expenditure.TotalCost = totalcost;
            db.Entry(expenditure).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Details", "Expenditure", new { id = exp.ExpenditureID });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}