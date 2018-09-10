namespace GinaBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Comment_CommentId", c => c.Int());
            CreateIndex("dbo.Comments", "Comment_CommentId");
            AddForeignKey("dbo.Comments", "Comment_CommentId", "dbo.Comments", "CommentId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "Comment_CommentId", "dbo.Comments");
            DropIndex("dbo.Comments", new[] { "Comment_CommentId" });
            DropColumn("dbo.Comments", "Comment_CommentId");
        }
    }
}
