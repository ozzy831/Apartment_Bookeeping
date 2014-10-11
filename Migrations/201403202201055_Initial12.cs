namespace Apartment_Bookeeping.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial12 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.People", "ApartmentID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "ApartmentID", c => c.Int());
        }
    }
}
