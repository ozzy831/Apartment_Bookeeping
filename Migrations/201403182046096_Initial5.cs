namespace Apartment_Bookeeping.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial5 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Lawsuits", name: "LawsuitID", newName: "PersonID");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.Lawsuits", name: "PersonID", newName: "LawsuitID");
        }
    }
}
