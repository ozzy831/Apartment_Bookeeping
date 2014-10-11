namespace Apartment_Bookeeping.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial10 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.People", "SSN", c => c.String(nullable: false, maxLength: 9));
            AlterColumn("dbo.People", "PhoneNumber", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.People", "PhoneNumber", c => c.String(nullable: false));
            AlterColumn("dbo.People", "SSN", c => c.String(nullable: false));
        }
    }
}
