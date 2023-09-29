namespace mr_bahaa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class examhome : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.exams", "percentage", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.exams", "percentage");
        }
    }
}
