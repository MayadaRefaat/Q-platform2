namespace mr_bahaa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Students", "courseid", "dbo.courses");
            DropForeignKey("dbo.payments", "id", "dbo.Students");
            DropForeignKey("dbo.scores", "id", "dbo.Students");
            DropIndex("dbo.Students", new[] { "courseid" });
            DropIndex("dbo.payments", new[] { "id" });
            DropIndex("dbo.scores", new[] { "id" });
            DropColumn("dbo.Students", "courseid");
            DropTable("dbo.controls");
            DropTable("dbo.eqscores");
            DropTable("dbo.equestionimgs");
            DropTable("dbo.essayexams");
            DropTable("dbo.Essayquestions");
            DropTable("dbo.payments");
            DropTable("dbo.scores");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.scores",
                c => new
                    {
                        scoreid = c.Int(nullable: false, identity: true),
                        points = c.Double(nullable: false),
                        date = c.DateTime(nullable: false),
                        id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.scoreid);
            
            CreateTable(
                "dbo.payments",
                c => new
                    {
                        paymentid = c.Int(nullable: false, identity: true),
                        id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.paymentid);
            
            CreateTable(
                "dbo.Essayquestions",
                c => new
                    {
                        equestionid = c.Int(nullable: false, identity: true),
                        question = c.String(),
                        examid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.equestionid);
            
            CreateTable(
                "dbo.essayexams",
                c => new
                    {
                        examid = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        timeofexam = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.examid);
            
            CreateTable(
                "dbo.equestionimgs",
                c => new
                    {
                        imgid = c.Int(nullable: false, identity: true),
                        imgurl = c.String(),
                        eqeutionid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.imgid);
            
            CreateTable(
                "dbo.eqscores",
                c => new
                    {
                        eqscoreid = c.Int(nullable: false, identity: true),
                        eqeustionid = c.Int(nullable: false),
                        answar = c.String(),
                        userid = c.String(),
                        degree = c.Int(nullable: false),
                        examid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.eqscoreid);
            
            CreateTable(
                "dbo.controls",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        openregister = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.Students", "courseid", c => c.Int(nullable: false));
            CreateIndex("dbo.scores", "id");
            CreateIndex("dbo.payments", "id");
            CreateIndex("dbo.Students", "courseid");
            AddForeignKey("dbo.scores", "id", "dbo.Students", "id", cascadeDelete: true);
            AddForeignKey("dbo.payments", "id", "dbo.Students", "id", cascadeDelete: true);
            AddForeignKey("dbo.Students", "courseid", "dbo.courses", "courseid", cascadeDelete: true);
        }
    }
}
