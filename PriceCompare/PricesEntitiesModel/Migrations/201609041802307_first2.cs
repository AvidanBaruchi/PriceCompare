namespace PricesEntitiesModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Stores", "Chain_ChainId", "dbo.Chains");
            DropForeignKey("dbo.Items", "Store_StoreId", "dbo.Stores");
            DropForeignKey("dbo.Prices", "Store_StoreId", "dbo.Stores");
            DropIndex("dbo.Stores", new[] { "Chain_ChainId" });
            DropIndex("dbo.Items", new[] { "Store_StoreId" });
            DropIndex("dbo.Prices", new[] { "Store_StoreId" });
            RenameColumn(table: "dbo.Stores", name: "Chain_ChainId", newName: "ChainId");
            DropPrimaryKey("dbo.Stores");
            AddColumn("dbo.Items", "Store_ChainId", c => c.String(maxLength: 128));
            AddColumn("dbo.Prices", "Store_ChainId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Stores", "ChainId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Stores", new[] { "StoreId", "ChainId" });
            CreateIndex("dbo.Stores", "ChainId");
            CreateIndex("dbo.Items", new[] { "Store_StoreId", "Store_ChainId" });
            CreateIndex("dbo.Prices", new[] { "Store_StoreId", "Store_ChainId" });
            AddForeignKey("dbo.Stores", "ChainId", "dbo.Chains", "ChainId", cascadeDelete: true);
            AddForeignKey("dbo.Items", new[] { "Store_StoreId", "Store_ChainId" }, "dbo.Stores", new[] { "StoreId", "ChainId" });
            AddForeignKey("dbo.Prices", new[] { "Store_StoreId", "Store_ChainId" }, "dbo.Stores", new[] { "StoreId", "ChainId" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Prices", new[] { "Store_StoreId", "Store_ChainId" }, "dbo.Stores");
            DropForeignKey("dbo.Items", new[] { "Store_StoreId", "Store_ChainId" }, "dbo.Stores");
            DropForeignKey("dbo.Stores", "ChainId", "dbo.Chains");
            DropIndex("dbo.Prices", new[] { "Store_StoreId", "Store_ChainId" });
            DropIndex("dbo.Items", new[] { "Store_StoreId", "Store_ChainId" });
            DropIndex("dbo.Stores", new[] { "ChainId" });
            DropPrimaryKey("dbo.Stores");
            AlterColumn("dbo.Stores", "ChainId", c => c.String(maxLength: 128));
            DropColumn("dbo.Prices", "Store_ChainId");
            DropColumn("dbo.Items", "Store_ChainId");
            AddPrimaryKey("dbo.Stores", "StoreId");
            RenameColumn(table: "dbo.Stores", name: "ChainId", newName: "Chain_ChainId");
            CreateIndex("dbo.Prices", "Store_StoreId");
            CreateIndex("dbo.Items", "Store_StoreId");
            CreateIndex("dbo.Stores", "Chain_ChainId");
            AddForeignKey("dbo.Prices", "Store_StoreId", "dbo.Stores", "StoreId");
            AddForeignKey("dbo.Items", "Store_StoreId", "dbo.Stores", "StoreId");
            AddForeignKey("dbo.Stores", "Chain_ChainId", "dbo.Chains", "ChainId");
        }
    }
}
