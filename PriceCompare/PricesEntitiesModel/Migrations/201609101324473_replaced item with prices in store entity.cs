namespace PricesEntitiesModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class replaceditemwithpricesinstoreentity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Items", new[] { "Store_StoreId", "Store_ChainId" }, "dbo.Stores");
            DropIndex("dbo.Items", new[] { "Store_StoreId", "Store_ChainId" });
            DropColumn("dbo.Items", "Store_StoreId");
            DropColumn("dbo.Items", "Store_ChainId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "Store_ChainId", c => c.String(maxLength: 128));
            AddColumn("dbo.Items", "Store_StoreId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Items", new[] { "Store_StoreId", "Store_ChainId" });
            AddForeignKey("dbo.Items", new[] { "Store_StoreId", "Store_ChainId" }, "dbo.Stores", new[] { "StoreId", "ChainId" });
        }
    }
}
