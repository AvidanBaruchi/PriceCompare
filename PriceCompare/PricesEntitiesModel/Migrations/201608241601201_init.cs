namespace PricesEntitiesModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
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
                        ReportedChainId = c.String(),
                        Name = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        StoreType = c.Int(nullable: false),
                        Chain_ChainId = c.String(maxLength: 128),
                        SubChainId_ChainId = c.String(maxLength: 128),
                        Chain_ChainId1 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.StoreId)
                .ForeignKey("dbo.Chains", t => t.Chain_ChainId)
                .ForeignKey("dbo.Chains", t => t.SubChainId_ChainId)
                .ForeignKey("dbo.Chains", t => t.Chain_ChainId1)
                .Index(t => t.Chain_ChainId)
                .Index(t => t.SubChainId_ChainId)
                .Index(t => t.Chain_ChainId1);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemId = c.Int(nullable: false, identity: true),
                        ItemCode = c.String(),
                        ItemType = c.Int(nullable: false),
                        Name = c.String(),
                        SManufacturerName = c.String(),
                        ManufacturerItemDescription = c.String(),
                        UnitQuantity = c.String(),
                        IsWeighted = c.Boolean(nullable: false),
                        QuantityInPackage = c.String(),
                        Store_StoreId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("dbo.Stores", t => t.Store_StoreId)
                .Index(t => t.Store_StoreId);
            
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
                    })
                .PrimaryKey(t => t.PriceId)
                .ForeignKey("dbo.Items", t => t.Item_ItemId)
                .ForeignKey("dbo.Stores", t => t.Store_StoreId)
                .Index(t => t.Item_ItemId)
                .Index(t => t.Store_StoreId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stores", "Chain_ChainId1", "dbo.Chains");
            DropForeignKey("dbo.Stores", "SubChainId_ChainId", "dbo.Chains");
            DropForeignKey("dbo.Items", "Store_StoreId", "dbo.Stores");
            DropForeignKey("dbo.Prices", "Store_StoreId", "dbo.Stores");
            DropForeignKey("dbo.Prices", "Item_ItemId", "dbo.Items");
            DropForeignKey("dbo.Stores", "Chain_ChainId", "dbo.Chains");
            DropIndex("dbo.Prices", new[] { "Store_StoreId" });
            DropIndex("dbo.Prices", new[] { "Item_ItemId" });
            DropIndex("dbo.Items", new[] { "Store_StoreId" });
            DropIndex("dbo.Stores", new[] { "Chain_ChainId1" });
            DropIndex("dbo.Stores", new[] { "SubChainId_ChainId" });
            DropIndex("dbo.Stores", new[] { "Chain_ChainId" });
            DropTable("dbo.Prices");
            DropTable("dbo.Items");
            DropTable("dbo.Stores");
            DropTable("dbo.Chains");
        }
    }
}
