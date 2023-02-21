namespace webBangHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatepaymantinorder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_order", "TypePayment", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_order", "TypePayment");
        }
    }
}
