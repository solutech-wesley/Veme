namespace Veme.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveMobileNumberFromCustomer : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Customers", "MobileNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "MobileNumber", c => c.String(maxLength: 13));
        }
    }
}
