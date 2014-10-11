using Apartment_Bookeeping.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Apartment_Bookeeping.Controllers
{
    public class HomeController : Controller
    {
        private ApartmentContext db = new ApartmentContext();

        public ActionResult Index()
        {
            var data = db.Apartments.Include("Manager");//(from c in db.Apartments select c);
         
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View(data);
        }

        public ActionResult About()
        {
            //var data = db.ApartmentNums.Include("Tenant").Include("Apartment");//db.Tenants.Include("ApartmentNum");//.Include("Apartment");
            var data = db.Tenants.Include("RentPayments");
            ViewBag.Message = "Your app description page.";

            return View(data);
        }

        public ActionResult Contact()
        {
            //var data = db.Lawsuits.Include("Tenant");//.Include("Apartment");
            var data = db.Expenditures.Include("ExpDetails");

            ViewBag.Message = "Your contact page.";

            return View(data);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
