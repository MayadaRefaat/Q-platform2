namespace mr_bahaa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class codes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.codes",
                c => new
                    {
                        codeid = c.Int(nullable: false, identity: true),
                        lectureid = c.Int(nullable: false),
                        ipaddress = c.String(),
                    })
                .PrimaryKey(t => t.codeid)
                .ForeignKey("dbo.lectures", t => t.lectureid, cascadeDelete: true)
                .Index(t => t.lectureid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.codes", "lectureid", "dbo.lectures");
            DropIndex("dbo.codes", new[] { "lectureid" });
            DropTable("dbo.codes");
        }
    }
}
