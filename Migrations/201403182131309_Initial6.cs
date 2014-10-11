namespace Apartment_Bookeeping.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial6 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.RentPayments", name: "TenantID", newName: "PersonID");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.RentPayments", name: "PersonID", newName: "TenantID");
        }
    }
}
