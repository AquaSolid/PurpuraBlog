namespace FilipBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DislikePosts", "Dislike_DislikeId", "dbo.Dislikes");
            DropForeignKey("dbo.DislikePosts", "Post_PostId", "dbo.Posts");
            DropForeignKey("dbo.LikePosts", "Like_LikeId", "dbo.Likes");
            DropForeignKey("dbo.LikePosts", "Post_PostId", "dbo.Posts");
            DropIndex("dbo.DislikePosts", new[] { "Dislike_DislikeId" });
            DropIndex("dbo.DislikePosts", new[] { "Post_PostId" });
            DropIndex("dbo.LikePosts", new[] { "Like_LikeId" });
            DropIndex("dbo.LikePosts", new[] { "Post_PostId" });
            AddColumn("dbo.Posts", "Dislike_DislikeId", c => c.Int());
            AddColumn("dbo.Posts", "Like_LikeId", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Post_PostId", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Post_PostId1", c => c.Int());
            CreateIndex("dbo.Posts", "Dislike_DislikeId");
            CreateIndex("dbo.Posts", "Like_LikeId");
            CreateIndex("dbo.AspNetUsers", "Post_PostId");
            CreateIndex("dbo.AspNetUsers", "Post_PostId1");
            AddForeignKey("dbo.Posts", "Dislike_DislikeId", "dbo.Dislikes", "DislikeId");
            AddForeignKey("dbo.Posts", "Like_LikeId", "dbo.Likes", "LikeId");
            AddForeignKey("dbo.AspNetUsers", "Post_PostId", "dbo.Posts", "PostId");
            AddForeignKey("dbo.AspNetUsers", "Post_PostId1", "dbo.Posts", "PostId");
            DropTable("dbo.DislikePosts");
            DropTable("dbo.LikePosts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.LikePosts",
                c => new
                    {
                        Like_LikeId = c.Int(nullable: false),
                        Post_PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Like_LikeId, t.Post_PostId });
            
            CreateTable(
                "dbo.DislikePosts",
                c => new
                    {
                        Dislike_DislikeId = c.Int(nullable: false),
                        Post_PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Dislike_DislikeId, t.Post_PostId });
            
            DropForeignKey("dbo.AspNetUsers", "Post_PostId1", "dbo.Posts");
            DropForeignKey("dbo.AspNetUsers", "Post_PostId", "dbo.Posts");
            DropForeignKey("dbo.Posts", "Like_LikeId", "dbo.Likes");
            DropForeignKey("dbo.Posts", "Dislike_DislikeId", "dbo.Dislikes");
            DropIndex("dbo.AspNetUsers", new[] { "Post_PostId1" });
            DropIndex("dbo.AspNetUsers", new[] { "Post_PostId" });
            DropIndex("dbo.Posts", new[] { "Like_LikeId" });
            DropIndex("dbo.Posts", new[] { "Dislike_DislikeId" });
            DropColumn("dbo.AspNetUsers", "Post_PostId1");
            DropColumn("dbo.AspNetUsers", "Post_PostId");
            DropColumn("dbo.Posts", "Like_LikeId");
            DropColumn("dbo.Posts", "Dislike_DislikeId");
            CreateIndex("dbo.LikePosts", "Post_PostId");
            CreateIndex("dbo.LikePosts", "Like_LikeId");
            CreateIndex("dbo.DislikePosts", "Post_PostId");
            CreateIndex("dbo.DislikePosts", "Dislike_DislikeId");
            AddForeignKey("dbo.LikePosts", "Post_PostId", "dbo.Posts", "PostId", cascadeDelete: true);
            AddForeignKey("dbo.LikePosts", "Like_LikeId", "dbo.Likes", "LikeId", cascadeDelete: true);
            AddForeignKey("dbo.DislikePosts", "Post_PostId", "dbo.Posts", "PostId", cascadeDelete: true);
            AddForeignKey("dbo.DislikePosts", "Dislike_DislikeId", "dbo.Dislikes", "DislikeId", cascadeDelete: true);
        }
    }
}
