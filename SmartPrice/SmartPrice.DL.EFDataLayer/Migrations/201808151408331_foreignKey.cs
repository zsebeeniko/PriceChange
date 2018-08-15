namespace SmartPrice.DL.EFDataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foreignKey : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Prices", "PRODUCT_ID");
            AddForeignKey("dbo.Prices", "PRODUCT_ID", "dbo.Products", "PRODUCT_ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Prices", "PRODUCT_ID", "dbo.Products");
            DropIndex("dbo.Prices", new[] { "PRODUCT_ID" });
        }
    }
}
