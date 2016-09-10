namespace PricesEntitiesModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class itemcodeiskey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Prices", "Item_ItemId", "dbo.Items");
            DropIndex("dbo.Prices", new[] { "Item_ItemId" });
            RenameColumn(table: "dbo.Prices", name: "Item_ItemId", newName: "Item_ItemCode");
            DropPrimaryKey("dbo.Items");
            AlterColumn("dbo.Items", "ItemCode", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Prices", "Item_ItemCode", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.Items", "ItemCode");
            CreateIndex("dbo.Prices", "Item_ItemCode");
            AddForeignKey("dbo.Prices", "Item_ItemCode", "dbo.Items", "ItemCode");
            DropColumn("dbo.Items", "ItemId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "ItemId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Prices", "Item_ItemCode", "dbo.Items");
            DropIndex("dbo.Prices", new[] { "Item_ItemCode" });
            DropPrimaryKey("dbo.Items");
            AlterColumn("dbo.Prices", "Item_ItemCode", c => c.Int());
            AlterColumn("dbo.Items", "ItemCode", c => c.String());
            AddPrimaryKey("dbo.Items", "ItemId");
            RenameColumn(table: "dbo.Prices", name: "Item_ItemCode", newName: "Item_ItemId");
            CreateIndex("dbo.Prices", "Item_ItemId");
            AddForeignKey("dbo.Prices", "Item_ItemId", "dbo.Items", "ItemId");
        }
    }
}
