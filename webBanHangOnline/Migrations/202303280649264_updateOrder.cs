namespace webBangHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateOrder : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.tb_order", "Quantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_order", "Quantity", c => c.Int(nullable: false));
        }
    }
}
