namespace mr_bahaa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class onelectures : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.onelectures",
                c => new
                    {
                        oneid = c.Int(nullable: false, identity: true),
                        lectureid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.oneid)
                .ForeignKey("dbo.lectures", t => t.lectureid, cascadeDelete: true)
                .Index(t => t.lectureid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.onelectures", "lectureid", "dbo.lectures");
            DropIndex("dbo.onelectures", new[] { "lectureid" });
            DropTable("dbo.onelectures");
        }
    }
}
