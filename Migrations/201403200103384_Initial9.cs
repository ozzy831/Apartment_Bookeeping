namespace Apartment_Bookeeping.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial9 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ApartmentNums", "AptNum", c => c.String(nullable: false, maxLength: 5));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ApartmentNums", "AptNum", c => c.String(nullable: false));
        }
    }
}
