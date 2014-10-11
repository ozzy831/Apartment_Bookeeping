namespace Apartment_Bookeeping.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.People", name: "ApartmentNum_ANID", newName: "ANID");
            RenameColumn(table: "dbo.ApartmentNums", name: "Tenant_PersonID", newName: "PersonID");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.ApartmentNums", name: "PersonID", newName: "Tenant_PersonID");
            RenameColumn(table: "dbo.People", name: "ANID", newName: "ApartmentNum_ANID");
        }
    }
}
