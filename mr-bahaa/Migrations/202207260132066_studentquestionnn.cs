namespace mr_bahaa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class studentquestionnn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.studentquestions", "teacherid", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.studentquestions", "teacherid");
        }
    }
}
