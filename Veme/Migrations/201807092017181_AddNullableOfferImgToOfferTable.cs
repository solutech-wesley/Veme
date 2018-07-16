namespace Veme.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNullableOfferImgToOfferTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Offers", "OfferImg", c => c.Binary(storeType: "varbinary"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Offers", "OfferImg");
        }
    }
}
