namespace Apartment_Bookeeping.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial15 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Expenditures", "Receipt", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Expenditures", "Receipt", c => c.String());
        }
    }
}
