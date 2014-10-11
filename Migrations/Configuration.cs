namespace Apartment_Bookeeping.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Apartment_Bookeeping.DAL.ApartmentContext>
    {
        public Configuration()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<Apartment_Bookeeping.DAL.ApartmentContext>());
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Apartment_Bookeeping.DAL.ApartmentContext context)
        {

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Locations.AddOrUpdate(
                l => l.ZipCode,
                new Location { City = "Bakersfield", State = "California", ZipCode = 93301 },
                new Location { City = "Bakersfield", State = "California", ZipCode = 93302 },
                new Location { City = "Bakersfield", State = "California", ZipCode = 93303 },
                new Location { City = "Bakersfield", State = "California", ZipCode = 93304 },
                new Location { City = "Bakersfield", State = "California", ZipCode = 93305 },
                new Location { City = "Bakersfield", State = "California", ZipCode = 93306 },
                new Location { City = "Bakersfield", State = "California", ZipCode = 93307 },
                new Location { City = "Bakersfield", State = "California", ZipCode = 93308 },
                new Location { City = "Bakersfield", State = "California", ZipCode = 93309 },
                new Location { City = "Bakersfield", State = "California", ZipCode = 93310 },
                new Location { City = "Bakersfield", State = "California", ZipCode = 93311 },
                new Location { City = "Bakersfield", State = "California", ZipCode = 93312 },
                new Location { City = "Bakersfield", State = "California", ZipCode = 93313 },
                new Location { City = "Bakersfield", State = "California", ZipCode = 93314 }
                );

            context.Managers.AddOrUpdate(
                m => m.SSN,
                new Manager
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Address = "12345 Ming Avenue",
                    ZipCode = 93311,
                    DOB = new DateTime(1988, 01, 16),
                    SSN = "999999999",
                    PhoneNumber = "6611234567",
                    //Apartments = (from c in context.Apartments where c.Name == "Sunny Apartments" select c).ToList()
                }
                    );

            context.Apartments.AddOrUpdate(
                a => a.Name,
                new Apartment
                {
                    Name = "Sunny Apartments",
                    Address = "122 Fake Street",
                    ZipCode = 93305,
                    PersonID = (from c in context.Managers where c.SSN == "999999999" select c.PersonID).FirstOrDefault(),
                    ApartmentNums = new List<ApartmentNum>
                        {
                            new ApartmentNum { AptNum = "4R", NumBaths = 2, NumRooms = 3, RentPrice = 1500.00M }, 
                                //PersonID = (from c in context.Tenants where c.SSN == "888888888" select c.PersonID).FirstOrDefault() },
                                //Tenant = (from c in context.Tenants where c.SSN == "888888888" select c).FirstOrDefault() },
                            new ApartmentNum { AptNum = "4S", NumBaths = 1, NumRooms = 2, RentPrice = 700.00M }, 
                                //PersonID = (from c in context.Tenants where c.SSN == "666666666" select c.PersonID).FirstOrDefault() },
                                //Tenant = (from c in context.Tenants where c.SSN == "666666666" select c).FirstOrDefault() },
                            new ApartmentNum { AptNum = "4T", NumBaths = 2, NumRooms = 2, RentPrice = 800.00M }
                        }
                }
                );
            
            context.Tenants.AddOrUpdate(
                t => t.SSN,
                new Tenant
                {
                    FirstName = "Kevin",
                    LastName = "Smith",
                    DOB = new DateTime(1966, 10, 25),
                    AmountOwed = 0.00M,
                    //ApartmentNum = (from c in context.ApartmentNums where c.ANID == 1 select c).FirstOrDefault(),
                    //ANID = 1,
                    isCurrentlyTenant = true,
                    PhoneNumber = "6616549785",
                    SSN = "888888888"
                },
                new Tenant
                {
                    FirstName = "Johnny",
                    LastName = "Deadbeat",
                    DOB = new DateTime(1988, 12, 07),
                    AmountOwed = 3000.00M,
                    isCurrentlyTenant = false,
                    PhoneNumber = "6618523697",
                    SSN = "777777777"
                },
                new Tenant
                {
                    FirstName = "Sophie",
                    LastName = "Sherman",
                    DOB = new DateTime(1978, 05, 27),
                    AmountOwed = 500.00M,
                    //ApartmentNum = (from c in context.ApartmentNums where c.ANID == 2 select c).FirstOrDefault(),
                    //ANID = 2,
                    isCurrentlyTenant = false,
                    PhoneNumber = "6615551234",
                    SSN = "666666666"
                }
                );

            //for zero or one-to- zero or one, raw sql queries must be used to insert data into the columns that are not mapped on the model
            context.Database.ExecuteSqlCommand("Update dbo.People set ANID = 1 where PersonID = 2");
            context.Database.ExecuteSqlCommand("Update dbo.People set ANID = 2 where PersonID = 4");

            context.Database.ExecuteSqlCommand("Update dbo.ApartmentNums set PersonID = 2 where ANID = 1");
            context.Database.ExecuteSqlCommand("Update dbo.ApartmentNums set PersonID = 4 where ANID = 2");
            
            context.Lawsuits.AddOrUpdate(
                l => l.Date,
                new Lawsuit
                {
                    Date = new DateTime(2013, 05, 22),
                    Descrition = "Johnny Deadbeat owes 3000 dollars in unpaid rent.",
                    Verdict = "Johnny Deadbeat must pay 500 dollars each month, which will be garnished from his paycheck.",
                    PersonID = (from c in context.Tenants where c.SSN == "777777777" select c.PersonID).FirstOrDefault()
                    //Tenant = (from c in context.Tenants where c.SSN == "777777777" select c).FirstOrDefault()
                }
                );

            context.Expenditures.AddOrUpdate(
                e => e.Receipt,
                new Expenditure
                {
                    Store = "Home Depot",
                    Receipt = "11-dee3-23-dfdf4",
                    Date = new DateTime(2014, 03, 01),
                    TotalCost = 35.69M,
                    ExpDetails = new List<ExpDetail> {
                        new ExpDetail { Item = "Hammer", Quantity = 2, Cost = 9.00M },
                        new ExpDetail { Item = "Box of 250 Nails", Quantity = 10, Cost = 12.00M },
                        new ExpDetail { Item = "Latter", Quantity = 1, Cost = 13.00M },
                        new ExpDetail { Item = "Taxes", Quantity = 1, Cost = 1.69M }
                    }
                }
                );

            context.BillingCompanies.AddOrUpdate(
                b => b.Name,
                new BillingCompany { Name = "PG&E", Address = "123 Main Street", ZipCode = 93311, PhoneNumber = "6616641234" }
                );

            context.Bills.AddOrUpdate(
                b => b.AccountNum,
                new Bill
                {
                    BillingCompanyID = (from c in context.BillingCompanies where c.Name == "PG&E" select c.BillingCompanyID).FirstOrDefault(),
                    AccountNum = "123456", 
                    StatementDate = new DateTime(2014, 01, 22), 
                    AmountOwed = 33.62M, 
                    AmountPaid = 33.62M,
                    PaidDate = new DateTime(2014, 01, 25),
                    ApartmentID = (from c in context.Apartments where c.Name == "Sunny Apartments" select c.ApartmentID).FirstOrDefault(),
                    BillDetails = new List<BillDetail>
                    {
                        new BillDetail { Service = "Electricity Usage", Cost = 31.23M },
                        new BillDetail { Service = "Electricity State Taxes", Cost = 2.39M }
                    }
                }
                );

            context.RentPayments.AddOrUpdate(
                r => r.Receipt,
                new RentPayment
                {
                    Date = new DateTime(2014,01, 01),
                    Receipt = "1234",
                    AmountPaid = 500.00M,
                    PersonID = (from c in context.Tenants where c.SSN == "666666666" select c.PersonID).FirstOrDefault()
                }
                );
        }
    }
}
