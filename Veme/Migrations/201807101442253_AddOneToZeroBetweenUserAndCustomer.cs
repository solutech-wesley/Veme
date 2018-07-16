namespace Veme.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOneToZeroBetweenUserAndCustomer : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "CustomerId", "dbo.AspNetUsers");
            AddForeignKey("dbo.Customers", "CustomerId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "CustomerId", "dbo.AspNetUsers");
            AddForeignKey("dbo.Customers", "CustomerId", "dbo.AspNetUsers", "Id");
        }
    }
}
