namespace Apartment_Bookeeping.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial11 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.People", "FirstName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.People", "LastName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.People", "Address", c => c.String(maxLength: 100));
            AlterColumn("dbo.Apartments", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Apartments", "Address", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Lawsuits", "Descrition", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.Lawsuits", "Verdict", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.RentPayments", "Receipt", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.Bills", "AccountNum", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.BillingCompanies", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.BillingCompanies", "Address", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.BillingCompanies", "PhoneNumber", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.BillDetails", "Service", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Expenditures", "Store", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.ExpDetails", "Item", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ExpDetails", "Item", c => c.String(nullable: false));
            AlterColumn("dbo.Expenditures", "Store", c => c.String(nullable: false));
            AlterColumn("dbo.BillDetails", "Service", c => c.String(nullable: false));
            AlterColumn("dbo.BillingCompanies", "PhoneNumber", c => c.String(nullable: false));
            AlterColumn("dbo.BillingCompanies", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.BillingCompanies", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Bills", "AccountNum", c => c.String(nullable: false));
            AlterColumn("dbo.RentPayments", "Receipt", c => c.String(nullable: false));
            AlterColumn("dbo.Lawsuits", "Verdict", c => c.String(nullable: false));
            AlterColumn("dbo.Lawsuits", "Descrition", c => c.String(nullable: false));
            AlterColumn("dbo.Apartments", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Apartments", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.People", "Address", c => c.String());
            AlterColumn("dbo.People", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.People", "FirstName", c => c.String(nullable: false));
        }
    }
}
