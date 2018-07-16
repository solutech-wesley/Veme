namespace Veme.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMerchantAddressTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MerchantAddresses",
                c => new
                    {
                        MerchantAddressId = c.Int(nullable: false, identity: true),
                        StreetAddress1 = c.String(maxLength: 255),
                        StreetAddress2 = c.String(maxLength: 255),
                        City = c.String(maxLength: 50),
                        Parish = c.String(maxLength: 50),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.MerchantAddressId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MerchantAddresses");
        }
    }
}
