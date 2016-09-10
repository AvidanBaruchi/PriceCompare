namespace PricesEntitiesModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedquantitiesandweightprops : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Items", "UnitQuantity");
            DropColumn("dbo.Items", "IsWeighted");
            DropColumn("dbo.Items", "QuantityInPackage");
            DropColumn("dbo.Prices", "UnitOfMeasurePrice");
            DropColumn("dbo.Prices", "UnitOfMeasure");
            DropColumn("dbo.Prices", "Quantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Prices", "Quantity", c => c.String());
            AddColumn("dbo.Prices", "UnitOfMeasure", c => c.String());
            AddColumn("dbo.Prices", "UnitOfMeasurePrice", c => c.Double(nullable: false));
            AddColumn("dbo.Items", "QuantityInPackage", c => c.String());
            AddColumn("dbo.Items", "IsWeighted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Items", "UnitQuantity", c => c.String());
        }
    }
}
