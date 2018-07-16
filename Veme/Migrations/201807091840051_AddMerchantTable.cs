namespace Veme.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMerchantTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Merchants",
                c => new
                    {
                        MerchantID = c.String(nullable: false, maxLength: 128),
                        CompanyName = c.String(),
                        CompanyWebsite = c.String(),
                        CompanyDescriptiton = c.String(),
                    })
                .PrimaryKey(t => t.MerchantID)
                .ForeignKey("dbo.AspNetUsers", t => t.MerchantID)
                .Index(t => t.MerchantID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Merchants", "MerchantID", "dbo.AspNetUsers");
            DropIndex("dbo.Merchants", new[] { "MerchantID" });
            DropTable("dbo.Merchants");
        }
    }
}
