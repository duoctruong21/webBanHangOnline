namespace webBangHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatenamethoigian : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_statistical", "ThoiGian", c => c.DateTime(nullable: false));
            DropColumn("dbo.tb_statistical", "Time");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_statistical", "Time", c => c.DateTime(nullable: false));
            DropColumn("dbo.tb_statistical", "ThoiGian");
        }
    }
}
