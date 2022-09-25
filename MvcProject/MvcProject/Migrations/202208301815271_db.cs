namespace MvcProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        cid = c.Int(nullable: false, identity: true),
                        Products_Pid = c.Int(nullable: false),
                        Users_Id = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        TotalPrice = c.Int(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.cid)
                .ForeignKey("dbo.Products", t => t.Products_Pid, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Products_Pid)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Pid = c.Int(nullable: false, identity: true),
                        Users_Id = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Category = c.String(),
                        Gender = c.String(),
                        Price = c.Int(nullable: false),
                        Discount = c.Int(nullable: false),
                        Rating = c.Int(nullable: false),
                        Publish = c.DateTime(nullable: false),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.Pid)
                .ForeignKey("dbo.Users", t => t.Users_Id, cascadeDelete: true)
                .Index(t => t.Users_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Mobileno = c.Int(nullable: false),
                        ImageUrl = c.String(),
                        Emailid = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 50),
                        Usertype = c.String(nullable: false),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Carts", "Products_Pid", "dbo.Products");
            DropForeignKey("dbo.Products", "Users_Id", "dbo.Users");
            DropIndex("dbo.Products", new[] { "Users_Id" });
            DropIndex("dbo.Carts", new[] { "User_Id" });
            DropIndex("dbo.Carts", new[] { "Products_Pid" });
            DropTable("dbo.Users");
            DropTable("dbo.Products");
            DropTable("dbo.Carts");
        }
    }
}
