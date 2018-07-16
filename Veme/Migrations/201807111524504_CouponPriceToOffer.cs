namespace Veme.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CouponPriceToOffer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Offers", "CouponPrice", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Offers", "CouponPrice");
        }
    }
}
