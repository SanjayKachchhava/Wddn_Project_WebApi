namespace DemoWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserModelTemps",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.VarificationModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VarificationCode = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserModelTemps", t => t.User_ID)
                .Index(t => t.User_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VarificationModels", "User_ID", "dbo.UserModelTemps");
            DropIndex("dbo.VarificationModels", new[] { "User_ID" });
            DropTable("dbo.VarificationModels");
            DropTable("dbo.Users");
            DropTable("dbo.UserModelTemps");
        }
    }
}
