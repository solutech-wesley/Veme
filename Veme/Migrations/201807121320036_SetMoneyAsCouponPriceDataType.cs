namespace Veme.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetMoneyAsCouponPriceDataType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Offers", "CouponPrice", c => c.Decimal(nullable: false, storeType: "money"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Offers", "CouponPrice", c => c.Int(nullable: false));
        }
    }
}
