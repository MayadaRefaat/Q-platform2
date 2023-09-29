namespace mr_bahaa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class homeworks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.lecturehomeworks",
                c => new
                    {
                        lecturehomeworkid = c.Int(nullable: false, identity: true),
                        Studentid = c.Int(nullable: false),
                        examid = c.Int(nullable: false),
                        allow = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.lecturehomeworkid)
                .ForeignKey("dbo.exams", t => t.examid, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.Studentid, cascadeDelete: true)
                .Index(t => t.Studentid)
                .Index(t => t.examid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.lecturehomeworks", "Studentid", "dbo.Students");
            DropForeignKey("dbo.lecturehomeworks", "examid", "dbo.exams");
            DropIndex("dbo.lecturehomeworks", new[] { "examid" });
            DropIndex("dbo.lecturehomeworks", new[] { "Studentid" });
            DropTable("dbo.lecturehomeworks");
        }
    }
}
