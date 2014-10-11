namespace Apartment_Bookeeping.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial4 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Lawsuits", "TenantID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Lawsuits", "TenantID", c => c.Int(nullable: false));
        }
    }
}
