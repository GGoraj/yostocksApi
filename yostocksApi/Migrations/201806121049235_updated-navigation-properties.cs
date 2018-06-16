namespace yostocksApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatednavigationproperties : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Fragments", name: "YostocksUserModel_Id", newName: "YostocksUser_Id");
            RenameIndex(table: "dbo.Fragments", name: "IX_YostocksUserModel_Id", newName: "IX_YostocksUser_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Fragments", name: "IX_YostocksUser_Id", newName: "IX_YostocksUserModel_Id");
            RenameColumn(table: "dbo.Fragments", name: "YostocksUser_Id", newName: "YostocksUserModel_Id");
        }
    }
}
