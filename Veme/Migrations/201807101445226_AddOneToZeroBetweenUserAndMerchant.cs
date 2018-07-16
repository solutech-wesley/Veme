namespace Veme.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOneToZeroBetweenUserAndMerchant : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Merchants", "MerchantID", "dbo.AspNetUsers");
            AddForeignKey("dbo.Merchants", "MerchantID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Merchants", "MerchantID", "dbo.AspNetUsers");
            AddForeignKey("dbo.Merchants", "MerchantID", "dbo.AspNetUsers", "Id");
        }
    }
}
