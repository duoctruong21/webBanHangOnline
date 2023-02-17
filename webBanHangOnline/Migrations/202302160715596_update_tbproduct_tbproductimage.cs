namespace webBangHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_tbproduct_tbproductimage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_productImage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.String(),
                        Image = c.String(),
                        IsDefault = c.String(),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tb_product", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
            AlterColumn("dbo.tb_product", "Detail", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tb_productImage", "Product_Id", "dbo.tb_product");
            DropIndex("dbo.tb_productImage", new[] { "Product_Id" });
            AlterColumn("dbo.tb_product", "Detail", c => c.String(nullable: false));
            DropTable("dbo.tb_productImage");
        }
    }
}
