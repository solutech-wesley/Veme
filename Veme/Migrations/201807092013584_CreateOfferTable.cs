namespace Veme.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateOfferTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Offers",
                c => new
                    {
                        OfferId = c.Int(nullable: false, identity: true),
                        OfferBegins = c.DateTime(nullable: false, storeType: "date"),
                        OfferEnds = c.DateTime(nullable: false, storeType: "date"),
                        DiscountRate = c.Byte(nullable: false),
                        TotalOffer = c.Int(),
                        OfferName = c.String(maxLength: 255),
                        OfferDetails = c.String(maxLength: 255),
                        CouponUsed = c.Int(nullable: false),
                        MerchantID = c.String(maxLength: 128),
                        CreationDate = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.OfferId)
                .ForeignKey("dbo.Merchants", t => t.MerchantID)
                .Index(t => t.MerchantID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Offers", "MerchantID", "dbo.Merchants");
            DropIndex("dbo.Offers", new[] { "MerchantID" });
            DropTable("dbo.Offers");
        }
    }
}
