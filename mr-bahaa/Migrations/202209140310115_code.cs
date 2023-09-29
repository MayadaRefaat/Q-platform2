namespace mr_bahaa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class code : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.codes", "code", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.codes", "code");
        }
    }
}
