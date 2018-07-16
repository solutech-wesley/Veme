namespace Veme.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductionCodesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductionCodes",
                c => new
                    {
                        ProductionCodeID = c.Int(nullable: false, identity: true),
                        CouponCode = c.String(maxLength: 12),
                        IsUsed = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        OfferId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductionCodeID)
                .ForeignKey("dbo.Offers", t => t.OfferId)
                .Index(t => t.OfferId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductionCodes", "OfferId", "dbo.Offers");
            DropIndex("dbo.ProductionCodes", new[] { "OfferId" });
            DropTable("dbo.ProductionCodes");
        }
    }
}
