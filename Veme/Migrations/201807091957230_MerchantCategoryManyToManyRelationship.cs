namespace Veme.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MerchantCategoryManyToManyRelationship : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MerchantCategories",
                c => new
                    {
                        Merchant_MerchantID = c.String(nullable: false, maxLength: 128),
                        Category_CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Merchant_MerchantID, t.Category_CategoryId })
                .ForeignKey("dbo.Merchants", t => t.Merchant_MerchantID, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_CategoryId, cascadeDelete: true)
                .Index(t => t.Merchant_MerchantID)
                .Index(t => t.Category_CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MerchantCategories", "Category_CategoryId", "dbo.Categories");
            DropForeignKey("dbo.MerchantCategories", "Merchant_MerchantID", "dbo.Merchants");
            DropIndex("dbo.MerchantCategories", new[] { "Category_CategoryId" });
            DropIndex("dbo.MerchantCategories", new[] { "Merchant_MerchantID" });
            DropTable("dbo.MerchantCategories");
        }
    }
}
