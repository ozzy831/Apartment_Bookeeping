using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Apartment_Bookeeping.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using System.Data;

namespace Apartment_Bookeeping.DAL
{
    public class ApartmentContext : DbContext
    {
        public DbSet<Person> People { set; get; }
        //public DbSet<Phone> Phones { set; get; }
        public DbSet<Manager> Managers { set; get; }
        public DbSet<Location> Locations { set; get; }
        public DbSet<Tenant> Tenants { set; get; }
        public DbSet<Lawsuit> Lawsuits { set; get; }
        public DbSet<Apartment> Apartments { set; get; }
        public DbSet<ApartmentNum> ApartmentNums { set; get; }
        public DbSet<RentPayment> RentPayments { set; get; }
        public DbSet<Bill> Bills { set; get; }
        public DbSet<BillingCompany> BillingCompanies { set; get; }
        public DbSet<BillDetail> BillDetails { set; get; }
        public DbSet<Expenditure> Expenditures { set; get; }
        public DbSet<ExpDetail> ExpDetails { set; get; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //person table
            modelBuilder.Entity<Person>().Property(p => p.SSN).IsRequired().HasMaxLength(9);
            modelBuilder.Entity<Person>().Property(p => p.FirstName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Person>().Property(p => p.LastName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Person>().Property(p => p.DOB).IsRequired().HasColumnType("date");
            modelBuilder.Entity<Person>().Property(p => p.PhoneNumber).IsRequired().HasMaxLength(10);
            modelBuilder.Entity<Person>().HasKey(p => p.PersonID);

            //location table
            modelBuilder.Entity<Location>().HasKey(l => l.ZipCode);
            modelBuilder.Entity<Location>().Property(l => l.ZipCode).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            
            //location relationships
            modelBuilder.Entity<Location>().HasMany(l => l.Apartments).WithRequired(a => a.Location).HasForeignKey(a => a.ZipCode).WillCascadeOnDelete(false);
            modelBuilder.Entity<Location>().HasMany(l => l.BillingCompanies).WithRequired(b => b.Location).HasForeignKey(b => b.ZipCode).WillCascadeOnDelete(false);
            modelBuilder.Entity<Location>().HasMany(l => l.Managers).WithRequired(m => m.Location).HasForeignKey(m => m.ZipCode).WillCascadeOnDelete(false);

            //apartment table
            modelBuilder.Entity<Apartment>().HasKey(a => a.ApartmentID);
            modelBuilder.Entity<Apartment>().Property(a => a.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Apartment>().Property(a => a.Address).IsRequired().HasMaxLength(100);

            //apartment relationship
            modelBuilder.Entity<Apartment>().HasMany(a => a.ApartmentNums).WithRequired(an => an.Apartment).HasForeignKey(an => an.ApartmentID);
            modelBuilder.Entity<Apartment>().HasMany(a => a.Bills).WithRequired(b => b.Apartment).HasForeignKey(b => b.ApartmentID);
            
            //apartment number table
            modelBuilder.Entity<ApartmentNum>().HasKey(a => a.ANID);
            modelBuilder.Entity<ApartmentNum>().Property(a => a.AptNum).IsRequired().HasMaxLength(5);
            modelBuilder.Entity<ApartmentNum>().Property(a => a.NumBaths).IsRequired();
            modelBuilder.Entity<ApartmentNum>().Property(a => a.NumRooms).IsRequired();
            modelBuilder.Entity<ApartmentNum>().Property(a => a.RentPrice).IsRequired();

            //apartment number relationship
            //zero or one-to-zero or one for tenant and apartment number
            modelBuilder.Entity<ApartmentNum>().HasOptional(a => a.Tenant).WithOptionalPrincipal().Map(m => m.MapKey("ANID"));
            
            //manager table
            modelBuilder.Entity<Manager>().Property(m => m.Address).IsRequired().HasMaxLength(100);

            //manager relationship
            modelBuilder.Entity<Manager>().HasMany(m => m.Apartments).WithOptional(a => a.Manager).WillCascadeOnDelete(false);
            
            //tenant table
            modelBuilder.Entity<Tenant>().Property(t => t.AmountOwed).IsRequired().HasColumnType("money");
            modelBuilder.Entity<Tenant>().Property(t => t.isCurrentlyTenant).IsRequired();

            //tenant relationship
            //PersonID in Lawsuit becomes the primary key of table and is the foreign key of People (Tenant)
            modelBuilder.Entity<Tenant>().HasOptional(t => t.Lawsuit).WithRequired(l => l.Tenant);
            //zero or one-to-zero or one for tenant and apartment number
            modelBuilder.Entity<Tenant>().HasOptional(a => a.ApartmentNum).WithOptionalPrincipal().Map(m => m.MapKey("PersonID"));
            modelBuilder.Entity<Tenant>().HasMany(t => t.RentPayments).WithRequired(r => r.Tenant).HasForeignKey(r => r.PersonID);

            //lawsuit table
            modelBuilder.Entity<Lawsuit>().HasKey(l => l.PersonID);
            modelBuilder.Entity<Lawsuit>().Property(l => l.Date).IsRequired().HasColumnType("date");
            modelBuilder.Entity<Lawsuit>().Property(l => l.Descrition).IsRequired().HasMaxLength(250);
            modelBuilder.Entity<Lawsuit>().Property(l => l.Verdict).IsRequired().HasMaxLength(100);

            //rent payment table
            modelBuilder.Entity<RentPayment>().HasKey(r => r.RentPaymentID);
            modelBuilder.Entity<RentPayment>().Property(r => r.Receipt).IsRequired().HasMaxLength(15);
            modelBuilder.Entity<RentPayment>().Property(r => r.Date).IsRequired().HasColumnType("date");
            modelBuilder.Entity<RentPayment>().Property(r => r.AmountPaid).IsRequired().HasColumnType("money");

            //bill table
            modelBuilder.Entity<Bill>().HasKey(b => b.BillID);
            modelBuilder.Entity<Bill>().Property(b => b.AccountNum).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Bill>().Property(b => b.StatementDate).IsRequired().HasColumnType("date");
            modelBuilder.Entity<Bill>().Property(b => b.PaidDate).IsRequired().HasColumnType("date");
            modelBuilder.Entity<Bill>().Property(b => b.AmountOwed).IsRequired().HasColumnType("money");
            modelBuilder.Entity<Bill>().Property(b => b.AmountPaid).IsRequired().HasColumnType("money");

            //bill relationtionship
            modelBuilder.Entity<Bill>().HasMany(b => b.BillDetails).WithRequired(bd => bd.Bill).HasForeignKey(bd => bd.BillID);
            modelBuilder.Entity<Bill>().HasRequired(b => b.BillingCompany).WithMany(bc => bc.Bills).HasForeignKey(b => b.BillingCompanyID);

            //billing company table
            modelBuilder.Entity<BillingCompany>().HasKey(b => b.BillingCompanyID);
            modelBuilder.Entity<BillingCompany>().Property(b => b.Address).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<BillingCompany>().Property(b => b.PhoneNumber).IsRequired().HasMaxLength(10);
            modelBuilder.Entity<BillingCompany>().Property(b => b.Name).IsRequired().HasMaxLength(50);

            //bill details table
            modelBuilder.Entity<BillDetail>().HasKey(b => b.BillDetailID);
            modelBuilder.Entity<BillDetail>().Property(b => b.Service).IsRequired().HasMaxLength(150);
            modelBuilder.Entity<BillDetail>().Property(b => b.Cost).IsRequired().HasColumnType("money");

            //expenditures table
            modelBuilder.Entity<Expenditure>().HasKey(e => e.ExpenditureID);
            modelBuilder.Entity<Expenditure>().Property(e => e.Date).IsRequired().HasColumnType("date");
            modelBuilder.Entity<Expenditure>().Property(e => e.TotalCost).IsRequired().HasColumnType("money");
            modelBuilder.Entity<Expenditure>().Property(e => e.Receipt).IsRequired();
            modelBuilder.Entity<Expenditure>().Property(e => e.Store).IsRequired().HasMaxLength(20);

            //expenditures relationship
            modelBuilder.Entity<Expenditure>().HasMany(e => e.ExpDetails).WithRequired(ed => ed.Expenditure).HasForeignKey(ed => ed.ExpenditureID);

            //expenditure details table
            modelBuilder.Entity<ExpDetail>().HasKey(e => e.ExpDetailID);
            modelBuilder.Entity<ExpDetail>().Property(e => e.Item).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<ExpDetail>().Property(e => e.Cost).IsRequired().HasColumnType("money");
            modelBuilder.Entity<ExpDetail>().Property(e => e.Quantity).IsRequired();
        }

        //used for server-side validation and enforcing business constraints
        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            var result = new DbEntityValidationResult(entityEntry, new List<DbValidationError>());

            if (entityEntry.Entity is Person && entityEntry.State == EntityState.Added)
            {
                Person person = entityEntry.Entity as Person;
                //check for uniqueness of ssn 
                if (People.Where(p => p.SSN == person.SSN).Count() > 0)
                {
                    result.ValidationErrors.Add(new DbValidationError("SSN", "SSN must be unique."));
                }
            }
            
            if (entityEntry.Entity is Person && entityEntry.State == EntityState.Modified)
            {
                Person person = entityEntry.Entity as Person;
                //check for uniqueness of ssn 
                if (People.Where(p => p.SSN == person.SSN).Where(p => p.PersonID != person.PersonID).Count() > 0)
                {
                    result.ValidationErrors.Add(new DbValidationError("SSN", "SSN must be unique."));
                }
            }

            if (entityEntry.Entity is Manager && (entityEntry.State == EntityState.Added || entityEntry.State == EntityState.Modified))
            {
                Manager manager = entityEntry.Entity as Manager;
                if (manager.ZipCode.ToString().Length != 5)
                {
                    result.ValidationErrors.Add(new DbValidationError("ZipCode", "Zip Code must have 5 digits."));
                }
                else if(Managers.Where(m => m.ZipCode == manager.ZipCode).Count() == 0)
                {
                    result.ValidationErrors.Add(new DbValidationError("ZipCode", "Zip Code entered does not exist."));
                }
            }

            if (entityEntry.Entity is ApartmentNum && entityEntry.State == EntityState.Added)
            {
                ApartmentNum aptnum = entityEntry.Entity as ApartmentNum;
                
                if (ApartmentNums.Where(a => a.AptNum == aptnum.AptNum).Where(a => a.ApartmentID == aptnum.ApartmentID).FirstOrDefault() != null)
                {
                    result.ValidationErrors.Add(new DbValidationError("AptNum", "Apartment Number exists in Apartment Complex. Please enter another number."));
                }
                
            }

            if (entityEntry.Entity is ApartmentNum && entityEntry.State == EntityState.Modified)
            {
                ApartmentNum aptnum = entityEntry.Entity as ApartmentNum;

                if (ApartmentNums.Where(a => a.AptNum == aptnum.AptNum).Where(a => a.ApartmentID == aptnum.ApartmentID).Where(a => a.ANID != aptnum.ANID).FirstOrDefault() != null)
                {
                    result.ValidationErrors.Add(new DbValidationError("AptNum", "Apartment Number exists in Apartment Complex. Please enter another number."));
                }

            }

            if (entityEntry.Entity is Apartment && (entityEntry.State == EntityState.Added || entityEntry.State == EntityState.Modified))
            {
                Apartment apartment = entityEntry.Entity as Apartment;
                if (apartment.ZipCode.ToString().Length != 5)
                {
                    result.ValidationErrors.Add(new DbValidationError("ZipCode", "Zip Code must have 5 digits."));
                }
                else if (Managers.Where(m => m.ZipCode == apartment.ZipCode).Count() == 0)
                {
                    result.ValidationErrors.Add(new DbValidationError("ZipCode", "Zip Code entered does not exist."));
                }
            }

            if (result.ValidationErrors.Count > 0)
            {
                return result;
            }
            else
            {
                return base.ValidateEntity(entityEntry, items);
            }
        }
    }
}