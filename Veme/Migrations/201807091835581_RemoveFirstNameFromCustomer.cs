namespace Veme.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveFirstNameFromCustomer : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Customers", "FirstName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "FirstName", c => c.String(nullable: false));
        }
    }
}
