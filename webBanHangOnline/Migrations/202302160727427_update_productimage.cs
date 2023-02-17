namespace webBangHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_productimage : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tb_productImage", "Product_Id", "dbo.tb_product");
            DropIndex("dbo.tb_productImage", new[] { "Product_Id" });
            DropColumn("dbo.tb_productImage", "ProductId");
            RenameColumn(table: "dbo.tb_productImage", name: "Product_Id", newName: "ProductId");
            AlterColumn("dbo.tb_productImage", "ProductId", c => c.Int(nullable: false));
            AlterColumn("dbo.tb_productImage", "IsDefault", c => c.Boolean(nullable: false));
            AlterColumn("dbo.tb_productImage", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.tb_productImage", "ProductId");
            AddForeignKey("dbo.tb_productImage", "ProductId", "dbo.tb_product", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tb_productImage", "ProductId", "dbo.tb_product");
            DropIndex("dbo.tb_productImage", new[] { "ProductId" });
            AlterColumn("dbo.tb_productImage", "ProductId", c => c.Int());
            AlterColumn("dbo.tb_productImage", "IsDefault", c => c.String());
            AlterColumn("dbo.tb_productImage", "ProductId", c => c.String());
            RenameColumn(table: "dbo.tb_productImage", name: "ProductId", newName: "Product_Id");
            AddColumn("dbo.tb_productImage", "ProductId", c => c.String());
            CreateIndex("dbo.tb_productImage", "Product_Id");
            AddForeignKey("dbo.tb_productImage", "Product_Id", "dbo.tb_product", "Id");
        }
    }
}
