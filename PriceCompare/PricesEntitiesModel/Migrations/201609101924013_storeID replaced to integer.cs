namespace PricesEntitiesModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class storeIDreplacedtointeger : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Prices", new[] { "Store_StoreId", "Store_ChainId" }, "dbo.Stores");
            DropIndex("dbo.Prices", new[] { "Store_StoreId", "Store_ChainId" });
            DropPrimaryKey("dbo.Stores");
            AlterColumn("dbo.Stores", "StoreId", c => c.Int(nullable: false));
            AlterColumn("dbo.Prices", "Store_StoreId", c => c.Int());
            AddPrimaryKey("dbo.Stores", new[] { "StoreId", "ChainId" });
            CreateIndex("dbo.Prices", new[] { "Store_StoreId", "Store_ChainId" });
            AddForeignKey("dbo.Prices", new[] { "Store_StoreId", "Store_ChainId" }, "dbo.Stores", new[] { "StoreId", "ChainId" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Prices", new[] { "Store_StoreId", "Store_ChainId" }, "dbo.Stores");
            DropIndex("dbo.Prices", new[] { "Store_StoreId", "Store_ChainId" });
            DropPrimaryKey("dbo.Stores");
            AlterColumn("dbo.Prices", "Store_StoreId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Stores", "StoreId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Stores", new[] { "StoreId", "ChainId" });
            CreateIndex("dbo.Prices", new[] { "Store_StoreId", "Store_ChainId" });
            AddForeignKey("dbo.Prices", new[] { "Store_StoreId", "Store_ChainId" }, "dbo.Stores", new[] { "StoreId", "ChainId" });
        }
    }
}
