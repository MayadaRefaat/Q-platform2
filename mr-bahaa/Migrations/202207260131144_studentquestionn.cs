namespace mr_bahaa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class studentquestionn : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.studentquestions", "teacherid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.studentquestions", "teacherid", c => c.String());
        }
    }
}
