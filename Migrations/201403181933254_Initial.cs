namespace Apartment_Bookeeping.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.People",
                c => new
                    {
                        PersonID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        SSN = c.String(nullable: false),
                        DOB = c.DateTime(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        Address = c.String(),
                        ZipCode = c.Int(),
                        AmountOwed = c.Decimal(precision: 18, scale: 2),
                        isCurrentlyTenant = c.Boolean(),
                        ANID = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        ApartmentNum_ANID = c.Int(),
                    })
                .PrimaryKey(t => t.PersonID)
                .ForeignKey("dbo.Locations", t => t.ZipCode)
                .ForeignKey("dbo.ApartmentNums", t => t.ApartmentNum_ANID)
                .Index(t => t.ZipCode)
                .Index(t => t.ApartmentNum_ANID);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        ZipCode = c.Int(nullable: false),
                        City = c.String(),
                        State = c.String(),
                    })
                .PrimaryKey(t => t.ZipCode);
            
            CreateTable(
                "dbo.Apartments",
                c => new
                    {
                        ApartmentID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        ZipCode = c.Int(nullable: false),
                        PersonID = c.Int(),
                    })
                .PrimaryKey(t => t.ApartmentID)
                .ForeignKey("dbo.Locations", t => t.ZipCode)
                .ForeignKey("dbo.People", t => t.PersonID)
                .Index(t => t.ZipCode)
                .Index(t => t.PersonID);
            
            CreateTable(
                "dbo.ApartmentNums",
                c => new
                    {
                        ANID = c.Int(nullable: false, identity: true),
                        AptNum = c.String(nullable: false),
                        NumRooms = c.Int(nullable: false),
                        NumBaths = c.Int(nullable: false),
                        RentPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ApartmentID = c.Int(nullable: false),
                        PersonID = c.Int(),
                        Tenant_PersonID = c.Int(),
                    })
                .PrimaryKey(t => t.ANID)
                .ForeignKey("dbo.People", t => t.Tenant_PersonID)
                .ForeignKey("dbo.Apartments", t => t.ApartmentID, cascadeDelete: true)
                .Index(t => t.Tenant_PersonID)
                .Index(t => t.ApartmentID);
            
            CreateTable(
                "dbo.Lawsuits",
                c => new
                    {
                        LawsuitID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Descrition = c.String(nullable: false),
                        Verdict = c.String(nullable: false),
                        TenantID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LawsuitID)
                .ForeignKey("dbo.People", t => t.LawsuitID)
                .Index(t => t.LawsuitID);
            
            CreateTable(
                "dbo.RentPayments",
                c => new
                    {
                        RentPaymentID = c.Int(nullable: false, identity: true),
                        Receipt = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        AmountPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TenantID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RentPaymentID)
                .ForeignKey("dbo.People", t => t.TenantID, cascadeDelete: true)
                .Index(t => t.TenantID);
            
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        BillID = c.Int(nullable: false, identity: true),
                        AccountNum = c.String(nullable: false),
                        StatementDate = c.DateTime(nullable: false),
                        PaidDate = c.DateTime(nullable: false),
                        AmountOwed = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AmountPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ApartmentID = c.Int(nullable: false),
                        BillingCompanyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BillID)
                .ForeignKey("dbo.BillingCompanies", t => t.BillingCompanyID, cascadeDelete: true)
                .ForeignKey("dbo.Apartments", t => t.ApartmentID, cascadeDelete: true)
                .Index(t => t.BillingCompanyID)
                .Index(t => t.ApartmentID);
            
            CreateTable(
                "dbo.BillingCompanies",
                c => new
                    {
                        BillingCompanyID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        ZipCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BillingCompanyID)
                .ForeignKey("dbo.Locations", t => t.ZipCode)
                .Index(t => t.ZipCode);
            
            CreateTable(
                "dbo.BillDetails",
                c => new
                    {
                        BillDetailID = c.Int(nullable: false, identity: true),
                        Service = c.String(nullable: false),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BillID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BillDetailID)
                .ForeignKey("dbo.Bills", t => t.BillID, cascadeDelete: true)
                .Index(t => t.BillID);
            
            CreateTable(
                "dbo.Expenditures",
                c => new
                    {
                        ExpenditureID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        TotalCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Store = c.String(nullable: false),
                        Receipt = c.String(),
                    })
                .PrimaryKey(t => t.ExpenditureID);
            
            CreateTable(
                "dbo.ExpDetails",
                c => new
                    {
                        ExpDetailID = c.Int(nullable: false, identity: true),
                        Item = c.String(nullable: false),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        ExpenditureID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ExpDetailID)
                .ForeignKey("dbo.Expenditures", t => t.ExpenditureID, cascadeDelete: true)
                .Index(t => t.ExpenditureID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.ExpDetails", new[] { "ExpenditureID" });
            DropIndex("dbo.BillDetails", new[] { "BillID" });
            DropIndex("dbo.BillingCompanies", new[] { "ZipCode" });
            DropIndex("dbo.Bills", new[] { "ApartmentID" });
            DropIndex("dbo.Bills", new[] { "BillingCompanyID" });
            DropIndex("dbo.RentPayments", new[] { "TenantID" });
            DropIndex("dbo.Lawsuits", new[] { "LawsuitID" });
            DropIndex("dbo.ApartmentNums", new[] { "ApartmentID" });
            DropIndex("dbo.ApartmentNums", new[] { "Tenant_PersonID" });
            DropIndex("dbo.Apartments", new[] { "PersonID" });
            DropIndex("dbo.Apartments", new[] { "ZipCode" });
            DropIndex("dbo.People", new[] { "ApartmentNum_ANID" });
            DropIndex("dbo.People", new[] { "ZipCode" });
            DropForeignKey("dbo.ExpDetails", "ExpenditureID", "dbo.Expenditures");
            DropForeignKey("dbo.BillDetails", "BillID", "dbo.Bills");
            DropForeignKey("dbo.BillingCompanies", "ZipCode", "dbo.Locations");
            DropForeignKey("dbo.Bills", "ApartmentID", "dbo.Apartments");
            DropForeignKey("dbo.Bills", "BillingCompanyID", "dbo.BillingCompanies");
            DropForeignKey("dbo.RentPayments", "TenantID", "dbo.People");
            DropForeignKey("dbo.Lawsuits", "LawsuitID", "dbo.People");
            DropForeignKey("dbo.ApartmentNums", "ApartmentID", "dbo.Apartments");
            DropForeignKey("dbo.ApartmentNums", "Tenant_PersonID", "dbo.People");
            DropForeignKey("dbo.Apartments", "PersonID", "dbo.People");
            DropForeignKey("dbo.Apartments", "ZipCode", "dbo.Locations");
            DropForeignKey("dbo.People", "ApartmentNum_ANID", "dbo.ApartmentNums");
            DropForeignKey("dbo.People", "ZipCode", "dbo.Locations");
            DropTable("dbo.ExpDetails");
            DropTable("dbo.Expenditures");
            DropTable("dbo.BillDetails");
            DropTable("dbo.BillingCompanies");
            DropTable("dbo.Bills");
            DropTable("dbo.RentPayments");
            DropTable("dbo.Lawsuits");
            DropTable("dbo.ApartmentNums");
            DropTable("dbo.Apartments");
            DropTable("dbo.Locations");
            DropTable("dbo.People");
        }
    }
}
