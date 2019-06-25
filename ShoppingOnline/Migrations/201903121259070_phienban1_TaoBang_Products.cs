namespace ShoppingOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class phienban1_TaoBang_Products : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50),
                        price = c.Double(nullable: false),
                        amount = c.Int(nullable: false),
                        description = c.String(maxLength: 500),
                        thumbnail = c.String(maxLength: 250),
                        valid = c.Boolean(nullable: false),
                        cateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Categories", t => t.cateId, cascadeDelete: true)
                .Index(t => t.cateId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "cateId", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "cateId" });
            DropTable("dbo.Products");
        }
    }
}
