namespace PricesEntitiesModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class replacediteminpricewithitemcodeforeignkey : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Prices", name: "Item_ItemCode", newName: "ItemCode");
            RenameIndex(table: "dbo.Prices", name: "IX_Item_ItemCode", newName: "IX_ItemCode");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Prices", name: "IX_ItemCode", newName: "IX_Item_ItemCode");
            RenameColumn(table: "dbo.Prices", name: "ItemCode", newName: "Item_ItemCode");
        }
    }
}
