namespace mr_bahaa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class homework : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.lectures", "secexamid", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.lectures", "secexamid");
        }
    }
}
