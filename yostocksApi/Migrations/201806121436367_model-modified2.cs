namespace yostocksApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modelmodified2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Fragments", "YostocksUser_Id", "dbo.YostocksUsers");
            DropIndex("dbo.Fragments", new[] { "YostocksUser_Id" });
            RenameColumn(table: "dbo.Fragments", name: "YostocksUser_Id", newName: "YostocksUserId");
            AlterColumn("dbo.Fragments", "YostocksUserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Fragments", "YostocksUserId");
            AddForeignKey("dbo.Fragments", "YostocksUserId", "dbo.YostocksUsers", "Id", cascadeDelete: true);
            DropColumn("dbo.Fragments", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Fragments", "UserId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Fragments", "YostocksUserId", "dbo.YostocksUsers");
            DropIndex("dbo.Fragments", new[] { "YostocksUserId" });
            AlterColumn("dbo.Fragments", "YostocksUserId", c => c.Int());
            RenameColumn(table: "dbo.Fragments", name: "YostocksUserId", newName: "YostocksUser_Id");
            CreateIndex("dbo.Fragments", "YostocksUser_Id");
            AddForeignKey("dbo.Fragments", "YostocksUser_Id", "dbo.YostocksUsers", "Id");
        }
    }
}
