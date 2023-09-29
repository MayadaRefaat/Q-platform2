namespace mr_bahaa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class studentquestion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.studentquestions", "teacherid", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.studentquestions", "teacherid");
        }
    }
}
