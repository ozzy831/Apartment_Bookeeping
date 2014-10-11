namespace Apartment_Bookeeping.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial14 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ApartmentNums", "ApartmentID", "dbo.Apartments");
            DropIndex("dbo.ApartmentNums", new[] { "ApartmentID" });
            AddForeignKey("dbo.ApartmentNums", "ApartmentID", "dbo.Apartments", "ApartmentID", cascadeDelete: true);
            CreateIndex("dbo.ApartmentNums", "ApartmentID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ApartmentNums", new[] { "ApartmentID" });
            DropForeignKey("dbo.ApartmentNums", "ApartmentID", "dbo.Apartments");
            CreateIndex("dbo.ApartmentNums", "ApartmentID");
            AddForeignKey("dbo.ApartmentNums", "ApartmentID", "dbo.Apartments", "ApartmentID");
        }
    }
}
