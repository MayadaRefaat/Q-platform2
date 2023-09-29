namespace mr_bahaa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lecturelock : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.lecturelocks",
                c => new
                    {
                        lecturelockid = c.Int(nullable: false, identity: true),
                        lectureid = c.Int(nullable: false),
                        studentid = c.String(),
                        entered = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.lecturelockid)
                .ForeignKey("dbo.lectures", t => t.lectureid, cascadeDelete: true)
                .Index(t => t.lectureid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.lecturelocks", "lectureid", "dbo.lectures");
            DropIndex("dbo.lecturelocks", new[] { "lectureid" });
            DropTable("dbo.lecturelocks");
        }
    }
}
