namespace Veme.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveLastNameFromCustomer : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Customers", "LastName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "LastName", c => c.String(nullable: false));
        }
    }
}
