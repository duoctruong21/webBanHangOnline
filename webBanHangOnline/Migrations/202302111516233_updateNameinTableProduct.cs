namespace webBangHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateNameinTableProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_product", "IsFeature", c => c.Boolean(nullable: false));
            DropColumn("dbo.tb_product", "IsFeatủe");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_product", "IsFeatủe", c => c.Boolean(nullable: false));
            DropColumn("dbo.tb_product", "IsFeature");
        }
    }
}
