namespace Veme.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCheckPointsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CheckPoints",
                c => new
                    {
                        CheckPointID = c.Int(nullable: false, identity: true),
                        LastPoint = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CheckPointID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CheckPoints");
        }
    }
}
