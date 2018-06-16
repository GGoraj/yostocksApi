namespace yostocksApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stockmodelgotRidOffCurrencyOfPurchase : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Stocks", "CurrencyOfPurchase");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stocks", "CurrencyOfPurchase", c => c.String(nullable: false));
        }
    }
}
