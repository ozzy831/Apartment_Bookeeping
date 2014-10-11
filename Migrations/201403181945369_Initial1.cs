namespace Apartment_Bookeeping.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.People", "ANID");
            DropColumn("dbo.ApartmentNums", "PersonID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApartmentNums", "PersonID", c => c.Int());
            AddColumn("dbo.People", "ANID", c => c.Int());
        }
    }
}
