namespace Veme.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MerchantAddressManyToManyRelationship : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MerchantLocations",
                c => new
                    {
                        Merchant_MerchantID = c.String(nullable: false, maxLength: 128),
                        MerchantAddress_MerchantAddressId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Merchant_MerchantID, t.MerchantAddress_MerchantAddressId })
                .ForeignKey("dbo.Merchants", t => t.Merchant_MerchantID, cascadeDelete: true)
                .ForeignKey("dbo.MerchantAddresses", t => t.MerchantAddress_MerchantAddressId, cascadeDelete: true)
                .Index(t => t.Merchant_MerchantID)
                .Index(t => t.MerchantAddress_MerchantAddressId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MerchantLocations", "MerchantAddress_MerchantAddressId", "dbo.MerchantAddresses");
            DropForeignKey("dbo.MerchantLocations", "Merchant_MerchantID", "dbo.Merchants");
            DropIndex("dbo.MerchantLocations", new[] { "MerchantAddress_MerchantAddressId" });
            DropIndex("dbo.MerchantLocations", new[] { "Merchant_MerchantID" });
            DropTable("dbo.MerchantLocations");
        }
    }
}
