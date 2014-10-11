namespace Apartment_Bookeeping.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial17 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.People", "AmountOwed", c => c.Decimal(storeType: "money"));
            AlterColumn("dbo.RentPayments", "AmountPaid", c => c.Decimal(nullable: false, storeType: "money"));
            AlterColumn("dbo.Bills", "AmountOwed", c => c.Decimal(nullable: false, storeType: "money"));
            AlterColumn("dbo.Bills", "AmountPaid", c => c.Decimal(nullable: false, storeType: "money"));
            AlterColumn("dbo.BillDetails", "Cost", c => c.Decimal(nullable: false, storeType: "money"));
            AlterColumn("dbo.Expenditures", "TotalCost", c => c.Decimal(nullable: false, storeType: "money"));
            AlterColumn("dbo.ExpDetails", "Cost", c => c.Decimal(nullable: false, storeType: "money"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ExpDetails", "Cost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Expenditures", "TotalCost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BillDetails", "Cost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Bills", "AmountPaid", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Bills", "AmountOwed", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.RentPayments", "AmountPaid", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.People", "AmountOwed", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
