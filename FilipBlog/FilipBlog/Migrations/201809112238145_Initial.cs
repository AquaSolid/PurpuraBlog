namespace FilipBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        DateOfCreation = c.DateTime(nullable: false),
                        DateOfModification = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 160),
                        Subtitle = c.String(maxLength: 160),
                        Content = c.String(nullable: false),
                        AuthorRefId = c.String(nullable: false, maxLength: 128),
                        DateOfCreation = c.DateTime(nullable: false),
                        DateOfModification = c.DateTime(nullable: false),
                        IsFlagged = c.Boolean(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        ApplicationUser_Id1 = c.String(maxLength: 128),
                        ApplicationUser_Id2 = c.String(maxLength: 128),
                        ApplicationUser_Id3 = c.String(maxLength: 128),
                        ApplicationUser_Id4 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id1)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id2)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id3)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id4)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorRefId, cascadeDelete: true)
                .Index(t => t.AuthorRefId)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id1)
                .Index(t => t.ApplicationUser_Id2)
                .Index(t => t.ApplicationUser_Id3)
                .Index(t => t.ApplicationUser_Id4);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        BirthDay = c.DateTime(nullable: false),
                        Biography = c.String(),
                        ProfilePictureURL = c.String(),
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
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
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
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false),
                        DateOfCreation = c.DateTime(nullable: false),
                        DateOfModification = c.DateTime(nullable: false),
                        CommenterRefId = c.String(maxLength: 128),
                        Comment_CommentId = c.Int(),
                        Post_PostId = c.Int(),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.AspNetUsers", t => t.CommenterRefId)
                .ForeignKey("dbo.Comments", t => t.Comment_CommentId)
                .ForeignKey("dbo.Posts", t => t.Post_PostId)
                .Index(t => t.CommenterRefId)
                .Index(t => t.Comment_CommentId)
                .Index(t => t.Post_PostId);
            
            CreateTable(
                "dbo.Dislikes",
                c => new
                    {
                        DislikeId = c.Int(nullable: false, identity: true),
                        Value = c.Int(nullable: false),
                        DateOfCreation = c.DateTime(nullable: false),
                        DislikerRefId = c.String(maxLength: 128),
                        Comment_CommentId = c.Int(),
                    })
                .PrimaryKey(t => t.DislikeId)
                .ForeignKey("dbo.AspNetUsers", t => t.DislikerRefId)
                .ForeignKey("dbo.Comments", t => t.Comment_CommentId)
                .Index(t => t.DislikerRefId)
                .Index(t => t.Comment_CommentId);
            
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        LikeId = c.Int(nullable: false, identity: true),
                        Value = c.Int(nullable: false),
                        DateOfCreation = c.DateTime(nullable: false),
                        LikerRefId = c.String(maxLength: 128),
                        Comment_CommentId = c.Int(),
                    })
                .PrimaryKey(t => t.LikeId)
                .ForeignKey("dbo.AspNetUsers", t => t.LikerRefId)
                .ForeignKey("dbo.Comments", t => t.Comment_CommentId)
                .Index(t => t.LikerRefId)
                .Index(t => t.Comment_CommentId);
            
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
                "dbo.ImageLinks",
                c => new
                    {
                        ImageLinkId = c.Int(nullable: false, identity: true),
                        URL = c.String(),
                        PostRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ImageLinkId)
                .ForeignKey("dbo.Posts", t => t.PostRefId, cascadeDelete: true)
                .Index(t => t.PostRefId);
            
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        ReportId = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false),
                        DateOfCreation = c.DateTime(nullable: false),
                        DateOfModification = c.DateTime(nullable: false),
                        ReporterRefId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ReportId)
                .ForeignKey("dbo.AspNetUsers", t => t.ReporterRefId)
                .Index(t => t.ReporterRefId);
            
            CreateTable(
                "dbo.VideoLinks",
                c => new
                    {
                        VideoLinkId = c.Int(nullable: false, identity: true),
                        URL = c.String(),
                        PostRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VideoLinkId)
                .ForeignKey("dbo.Posts", t => t.PostRefId, cascadeDelete: true)
                .Index(t => t.PostRefId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.DislikePosts",
                c => new
                    {
                        Dislike_DislikeId = c.Int(nullable: false),
                        Post_PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Dislike_DislikeId, t.Post_PostId })
                .ForeignKey("dbo.Dislikes", t => t.Dislike_DislikeId, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.Post_PostId, cascadeDelete: true)
                .Index(t => t.Dislike_DislikeId)
                .Index(t => t.Post_PostId);
            
            CreateTable(
                "dbo.LikePosts",
                c => new
                    {
                        Like_LikeId = c.Int(nullable: false),
                        Post_PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Like_LikeId, t.Post_PostId })
                .ForeignKey("dbo.Likes", t => t.Like_LikeId, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.Post_PostId, cascadeDelete: true)
                .Index(t => t.Like_LikeId)
                .Index(t => t.Post_PostId);
            
            CreateTable(
                "dbo.PostCategories",
                c => new
                    {
                        Post_PostId = c.Int(nullable: false),
                        Category_CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Post_PostId, t.Category_CategoryId })
                .ForeignKey("dbo.Posts", t => t.Post_PostId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_CategoryId, cascadeDelete: true)
                .Index(t => t.Post_PostId)
                .Index(t => t.Category_CategoryId);
            
            CreateTable(
                "dbo.ReportPosts",
                c => new
                    {
                        Report_ReportId = c.Int(nullable: false),
                        Post_PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Report_ReportId, t.Post_PostId })
                .ForeignKey("dbo.Reports", t => t.Report_ReportId, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.Post_PostId, cascadeDelete: true)
                .Index(t => t.Report_ReportId)
                .Index(t => t.Post_PostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.VideoLinks", "PostRefId", "dbo.Posts");
            DropForeignKey("dbo.Reports", "ReporterRefId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ReportPosts", "Post_PostId", "dbo.Posts");
            DropForeignKey("dbo.ReportPosts", "Report_ReportId", "dbo.Reports");
            DropForeignKey("dbo.ImageLinks", "PostRefId", "dbo.Posts");
            DropForeignKey("dbo.Comments", "Post_PostId", "dbo.Posts");
            DropForeignKey("dbo.PostCategories", "Category_CategoryId", "dbo.Categories");
            DropForeignKey("dbo.PostCategories", "Post_PostId", "dbo.Posts");
            DropForeignKey("dbo.Posts", "AuthorRefId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "ApplicationUser_Id4", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "ApplicationUser_Id3", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "ApplicationUser_Id2", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "ApplicationUser_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "Comment_CommentId", "dbo.Comments");
            DropForeignKey("dbo.Likes", "Comment_CommentId", "dbo.Comments");
            DropForeignKey("dbo.LikePosts", "Post_PostId", "dbo.Posts");
            DropForeignKey("dbo.LikePosts", "Like_LikeId", "dbo.Likes");
            DropForeignKey("dbo.Likes", "LikerRefId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Dislikes", "Comment_CommentId", "dbo.Comments");
            DropForeignKey("dbo.DislikePosts", "Post_PostId", "dbo.Posts");
            DropForeignKey("dbo.DislikePosts", "Dislike_DislikeId", "dbo.Dislikes");
            DropForeignKey("dbo.Dislikes", "DislikerRefId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "CommenterRefId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.ReportPosts", new[] { "Post_PostId" });
            DropIndex("dbo.ReportPosts", new[] { "Report_ReportId" });
            DropIndex("dbo.PostCategories", new[] { "Category_CategoryId" });
            DropIndex("dbo.PostCategories", new[] { "Post_PostId" });
            DropIndex("dbo.LikePosts", new[] { "Post_PostId" });
            DropIndex("dbo.LikePosts", new[] { "Like_LikeId" });
            DropIndex("dbo.DislikePosts", new[] { "Post_PostId" });
            DropIndex("dbo.DislikePosts", new[] { "Dislike_DislikeId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.VideoLinks", new[] { "PostRefId" });
            DropIndex("dbo.Reports", new[] { "ReporterRefId" });
            DropIndex("dbo.ImageLinks", new[] { "PostRefId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Likes", new[] { "Comment_CommentId" });
            DropIndex("dbo.Likes", new[] { "LikerRefId" });
            DropIndex("dbo.Dislikes", new[] { "Comment_CommentId" });
            DropIndex("dbo.Dislikes", new[] { "DislikerRefId" });
            DropIndex("dbo.Comments", new[] { "Post_PostId" });
            DropIndex("dbo.Comments", new[] { "Comment_CommentId" });
            DropIndex("dbo.Comments", new[] { "CommenterRefId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Posts", new[] { "ApplicationUser_Id4" });
            DropIndex("dbo.Posts", new[] { "ApplicationUser_Id3" });
            DropIndex("dbo.Posts", new[] { "ApplicationUser_Id2" });
            DropIndex("dbo.Posts", new[] { "ApplicationUser_Id1" });
            DropIndex("dbo.Posts", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Posts", new[] { "AuthorRefId" });
            DropTable("dbo.ReportPosts");
            DropTable("dbo.PostCategories");
            DropTable("dbo.LikePosts");
            DropTable("dbo.DislikePosts");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.VideoLinks");
            DropTable("dbo.Reports");
            DropTable("dbo.ImageLinks");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Likes");
            DropTable("dbo.Dislikes");
            DropTable("dbo.Comments");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Posts");
            DropTable("dbo.Categories");
        }
    }
}
