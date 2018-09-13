namespace FilipBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ReportPosts", "Report_ReportId", "dbo.Reports");
            DropForeignKey("dbo.ReportPosts", "Post_PostId", "dbo.Posts");
            DropForeignKey("dbo.Comments", "Post_PostId", "dbo.Posts");
            DropIndex("dbo.Comments", new[] { "Post_PostId" });
            DropIndex("dbo.PostCategories", new[] { "Category_CategoryId" });
            DropIndex("dbo.ReportPosts", new[] { "Report_ReportId" });
            DropIndex("dbo.ReportPosts", new[] { "Post_PostId" });
            AddColumn("dbo.Reports", "Post_PostId", c => c.Int());
            AlterColumn("dbo.Comments", "Post_PostId", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "Post_PostId");
            CreateIndex("dbo.Reports", "Post_PostId");
            CreateIndex("dbo.PostCategories", "Category_CategoryID");
            AddForeignKey("dbo.Reports", "Post_PostId", "dbo.Posts", "PostId");
            AddForeignKey("dbo.Comments", "Post_PostId", "dbo.Posts", "PostId", cascadeDelete: true);
            DropTable("dbo.ReportPosts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ReportPosts",
                c => new
                    {
                        Report_ReportId = c.Int(nullable: false),
                        Post_PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Report_ReportId, t.Post_PostId });
            
            DropForeignKey("dbo.Comments", "Post_PostId", "dbo.Posts");
            DropForeignKey("dbo.Reports", "Post_PostId", "dbo.Posts");
            DropIndex("dbo.PostCategories", new[] { "Category_CategoryID" });
            DropIndex("dbo.Reports", new[] { "Post_PostId" });
            DropIndex("dbo.Comments", new[] { "Post_PostId" });
            AlterColumn("dbo.Comments", "Post_PostId", c => c.Int());
            DropColumn("dbo.Reports", "Post_PostId");
            CreateIndex("dbo.ReportPosts", "Post_PostId");
            CreateIndex("dbo.ReportPosts", "Report_ReportId");
            CreateIndex("dbo.PostCategories", "Category_CategoryId");
            CreateIndex("dbo.Comments", "Post_PostId");
            AddForeignKey("dbo.Comments", "Post_PostId", "dbo.Posts", "PostId");
            AddForeignKey("dbo.ReportPosts", "Post_PostId", "dbo.Posts", "PostId", cascadeDelete: true);
            AddForeignKey("dbo.ReportPosts", "Report_ReportId", "dbo.Reports", "ReportId", cascadeDelete: true);
        }
    }
}
