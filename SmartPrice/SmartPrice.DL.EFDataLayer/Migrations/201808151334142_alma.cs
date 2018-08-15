namespace SmartPrice.DL.EFDataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alma : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Markers",
                c => new
                    {
                        MarkerId = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 50),
                        Lattitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Longitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.MarkerId);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        PicturePathId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.PicturePathId);
            
            CreateTable(
                "dbo.Prices",
                c => new
                    {
                        PriceId = c.Int(nullable: false, identity: true),
                        PriceToConvert = c.String(maxLength: 50),
                        FromCurrency = c.String(maxLength: 50),
                        ToCurrency = c.String(maxLength: 50),
                        ExchangedValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DefaultValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Shop = c.String(maxLength: 50),
                        Date = c.DateTime(nullable: false),
                        PicturePathId = c.Int(nullable: false),
                        PRODUCT_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PriceId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        PRODUCT_ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        DESCRIPTION = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.PRODUCT_ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Products");
            DropTable("dbo.Prices");
            DropTable("dbo.Pictures");
            DropTable("dbo.Markers");
        }
    }
}
