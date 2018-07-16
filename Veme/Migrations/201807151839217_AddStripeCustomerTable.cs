namespace Veme.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStripeCustomerTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StripeCustomers",
                c => new
                    {
                        StripeCustomerID = c.String(nullable: false, maxLength: 50),
                        UserId = c.String(maxLength: 128),
                        CreationDate = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.StripeCustomerID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StripeCustomers", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.StripeCustomers", new[] { "UserId" });
            DropTable("dbo.StripeCustomers");
        }
    }
}
