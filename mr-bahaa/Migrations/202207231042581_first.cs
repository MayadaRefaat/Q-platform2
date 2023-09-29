namespace mr_bahaa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.coursescategories",
                c => new
                    {
                        catigoriesid = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.catigoriesid);
            
            CreateTable(
                "dbo.Joins",
                c => new
                    {
                        Joinsid = c.Int(nullable: false, identity: true),
                        Studentid = c.Int(nullable: false),
                        subjectcoureid = c.Int(nullable: false),
                        apporved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Joinsid)
                .ForeignKey("dbo.Students", t => t.Studentid, cascadeDelete: true)
                .ForeignKey("dbo.subjectcoures", t => t.subjectcoureid, cascadeDelete: true)
                .Index(t => t.Studentid)
                .Index(t => t.subjectcoureid);
            
            CreateTable(
                "dbo.subjectcoures",
                c => new
                    {
                        subjectcourseid = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        teacherid = c.Int(nullable: false),
                        catigoriesid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.subjectcourseid)
                .ForeignKey("dbo.coursescategories", t => t.catigoriesid, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.teacherid, cascadeDelete: true)
                .Index(t => t.teacherid)
                .Index(t => t.catigoriesid);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        teacherid = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        info = c.String(nullable: false),
                        subject = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.teacherid);
            
            CreateTable(
                "dbo.subjectcoureimgs",
                c => new
                    {
                        photoid = c.Int(nullable: false, identity: true),
                        photoulr = c.String(),
                        subjectcoureid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.photoid);
            
            CreateTable(
                "dbo.teacherimgs",
                c => new
                    {
                        photoid = c.Int(nullable: false, identity: true),
                        photoulr = c.String(),
                        teacherid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.photoid);
            
            AddColumn("dbo.lectures", "subjectid", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "yourclass", c => c.String());
            AddColumn("dbo.exams", "subjectid", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Joins", "subjectcoureid", "dbo.subjectcoures");
            DropForeignKey("dbo.subjectcoures", "teacherid", "dbo.Teachers");
            DropForeignKey("dbo.subjectcoures", "catigoriesid", "dbo.coursescategories");
            DropForeignKey("dbo.Joins", "Studentid", "dbo.Students");
            DropIndex("dbo.subjectcoures", new[] { "catigoriesid" });
            DropIndex("dbo.subjectcoures", new[] { "teacherid" });
            DropIndex("dbo.Joins", new[] { "subjectcoureid" });
            DropIndex("dbo.Joins", new[] { "Studentid" });
            DropColumn("dbo.exams", "subjectid");
            DropColumn("dbo.Students", "yourclass");
            DropColumn("dbo.lectures", "subjectid");
            DropTable("dbo.teacherimgs");
            DropTable("dbo.subjectcoureimgs");
            DropTable("dbo.Teachers");
            DropTable("dbo.subjectcoures");
            DropTable("dbo.Joins");
            DropTable("dbo.coursescategories");
        }
    }
}
