namespace Veme.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCouponRepositoryTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CouponRepositories",
                c => new
                    {
                        CouponRepositoryID = c.Int(nullable: false, identity: true),
                        CouponCode = c.String(maxLength: 12),
                        InProduction = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CouponRepositoryID)
                .Index(t => t.CouponCode, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.CouponRepositories", new[] { "CouponCode" });
            DropTable("dbo.CouponRepositories");
        }
    }
}
