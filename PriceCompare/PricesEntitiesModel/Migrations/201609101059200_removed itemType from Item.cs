namespace PricesEntitiesModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeditemTypefromItem : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Items", "ItemType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "ItemType", c => c.Int(nullable: false));
        }
    }
}
