namespace Apartment_Bookeeping.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial16 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.People", "DOB", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.Lawsuits", "Date", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.RentPayments", "Date", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.Bills", "StatementDate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.Bills", "PaidDate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.Expenditures", "Date", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Expenditures", "Date", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Bills", "PaidDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Bills", "StatementDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.RentPayments", "Date", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Lawsuits", "Date", c => c.DateTime(nullable: false));
            AlterColumn("dbo.People", "DOB", c => c.DateTime(nullable: false));
        }
    }
}
