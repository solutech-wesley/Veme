namespace Veme.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCouponValidDurationToOfferTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Offers", "CouponDurationInMonths", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Offers", "CouponDurationInMonths");
        }
    }
}
