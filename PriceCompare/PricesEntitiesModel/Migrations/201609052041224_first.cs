namespace PricesEntitiesModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chains",
                c => new
                    {
                        ChainId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ChainId);
            
            CreateTable(
                "dbo.Stores",
                c => new
                    {
                        StoreId = c.String(nullable: false, maxLength: 128),
                        ChainId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        StoreType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StoreId, t.ChainId })
                .ForeignKey("dbo.Chains", t => t.ChainId, cascadeDelete: true)
                .Index(t => t.ChainId);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemId = c.Int(nullable: false, identity: true),
                        ItemCode = c.String(),
                        ItemType = c.Int(nullable: false),
                        Name = c.String(),
                        ManufacturerName = c.String(),
                        ManufacturerItemDescription = c.String(),
                        UnitQuantity = c.String(),
                        IsWeighted = c.Boolean(nullable: false),
                        QuantityInPackage = c.String(),
                        Store_StoreId = c.String(maxLength: 128),
                        Store_ChainId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("dbo.Stores", t => new { t.Store_StoreId, t.Store_ChainId })
                .Index(t => new { t.Store_StoreId, t.Store_ChainId });
            
            CreateTable(
                "dbo.Prices",
                c => new
                    {
                        PriceId = c.Int(nullable: false, identity: true),
                        PriceValue = c.Double(nullable: false),
                        UnitOfMeasurePrice = c.Double(nullable: false),
                        UnitOfMeasure = c.String(),
                        Quantity = c.String(),
                        Item_ItemId = c.Int(),
                        Store_StoreId = c.String(maxLength: 128),
                        Store_ChainId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PriceId)
                .ForeignKey("dbo.Items", t => t.Item_ItemId)
                .ForeignKey("dbo.Stores", t => new { t.Store_StoreId, t.Store_ChainId })
                .Index(t => t.Item_ItemId)
                .Index(t => new { t.Store_StoreId, t.Store_ChainId });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", new[] { "Store_StoreId", "Store_ChainId" }, "dbo.Stores");
            DropForeignKey("dbo.Prices", new[] { "Store_StoreId", "Store_ChainId" }, "dbo.Stores");
            DropForeignKey("dbo.Prices", "Item_ItemId", "dbo.Items");
            DropForeignKey("dbo.Stores", "ChainId", "dbo.Chains");
            DropIndex("dbo.Prices", new[] { "Store_StoreId", "Store_ChainId" });
            DropIndex("dbo.Prices", new[] { "Item_ItemId" });
            DropIndex("dbo.Items", new[] { "Store_StoreId", "Store_ChainId" });
            DropIndex("dbo.Stores", new[] { "ChainId" });
            DropTable("dbo.Prices");
            DropTable("dbo.Items");
            DropTable("dbo.Stores");
            DropTable("dbo.Chains");
        }
    }
}
