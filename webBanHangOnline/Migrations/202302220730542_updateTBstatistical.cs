namespace webBangHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateTBstatistical : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Statisticals", newName: "tb_statistical");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.tb_statistical", newName: "Statisticals");
        }
    }
}
