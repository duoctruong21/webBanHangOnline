namespace webBangHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tbproduct_pricesale : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_product", "PriceSale", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_product", "PriceSale", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
