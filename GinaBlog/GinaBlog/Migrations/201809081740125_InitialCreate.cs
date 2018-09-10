namespace GinaBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        PostRefId = c.Int(nullable: false),
                        CommenterRefId = c.String(maxLength: 128),
                        Content = c.String(),
                        DateCommented = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.AspNetUsers", t => t.CommenterRefId)
                .ForeignKey("dbo.Posts", t => t.PostRefId, cascadeDelete: true)
                .Index(t => t.PostRefId)
                .Index(t => t.CommenterRefId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Comment_CommentId = c.Int(),
                        Comment_CommentId1 = c.Int(),
                        Post_PostId = c.Int(),
                        Post_PostId1 = c.Int(),
                        Post_PostId2 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comments", t => t.Comment_CommentId)
                .ForeignKey("dbo.Comments", t => t.Comment_CommentId1)
                .ForeignKey("dbo.Posts", t => t.Post_PostId)
                .ForeignKey("dbo.Posts", t => t.Post_PostId1)
                .ForeignKey("dbo.Posts", t => t.Post_PostId2)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Comment_CommentId)
                .Index(t => t.Comment_CommentId1)
                .Index(t => t.Post_PostId)
                .Index(t => t.Post_PostId1)
                .Index(t => t.Post_PostId2);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 160),
                        Subtitle = c.String(maxLength: 160),
                        Content = c.String(nullable: false),
                        AuthorRefId = c.String(nullable: false, maxLength: 128),
                        DatePosted = c.DateTime(nullable: false),
                        Flagged = c.Boolean(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorRefId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.AuthorRefId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Comments", "PostRefId", "dbo.Posts");
            DropForeignKey("dbo.AspNetUsers", "Post_PostId2", "dbo.Posts");
            DropForeignKey("dbo.AspNetUsers", "Post_PostId1", "dbo.Posts");
            DropForeignKey("dbo.AspNetUsers", "Post_PostId", "dbo.Posts");
            DropForeignKey("dbo.Posts", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Posts", "AuthorRefId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Comment_CommentId1", "dbo.Comments");
            DropForeignKey("dbo.AspNetUsers", "Comment_CommentId", "dbo.Comments");
            DropForeignKey("dbo.Comments", "CommenterRefId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Posts", new[] { "CategoryId" });
            DropIndex("dbo.Posts", new[] { "AuthorRefId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Post_PostId2" });
            DropIndex("dbo.AspNetUsers", new[] { "Post_PostId1" });
            DropIndex("dbo.AspNetUsers", new[] { "Post_PostId" });
            DropIndex("dbo.AspNetUsers", new[] { "Comment_CommentId1" });
            DropIndex("dbo.AspNetUsers", new[] { "Comment_CommentId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Comments", new[] { "CommenterRefId" });
            DropIndex("dbo.Comments", new[] { "PostRefId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Posts");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Comments");
            DropTable("dbo.Categories");
        }
    }
}
