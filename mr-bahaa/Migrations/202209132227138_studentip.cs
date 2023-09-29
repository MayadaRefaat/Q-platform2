namespace mr_bahaa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class studentip : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.studentips",
                c => new
                    {
                        studentipid = c.Int(nullable: false, identity: true),
                        lectureid = c.Int(nullable: false),
                        studentid = c.String(),
                        ipaddress = c.String(),
                    })
                .PrimaryKey(t => t.studentipid)
                .ForeignKey("dbo.lectures", t => t.lectureid, cascadeDelete: true)
                .Index(t => t.lectureid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.studentips", "lectureid", "dbo.lectures");
            DropIndex("dbo.studentips", new[] { "lectureid" });
            DropTable("dbo.studentips");
        }
    }
}
