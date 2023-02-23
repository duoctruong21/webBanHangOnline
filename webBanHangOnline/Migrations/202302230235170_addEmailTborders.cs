﻿namespace webBangHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addEmailTborders : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_order", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_order", "Email");
        }
    }
}
