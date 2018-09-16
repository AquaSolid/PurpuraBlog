using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilipBlog.Models {
    public class Comment {

        public Comment() {
            this.Likes = new List<Like>();
            this.Dislikes = new List<Dislike>();
            this.Replies = new List<Comment>();
        }

        [Key] public int CommentId { get; set; }
        [Required] public string Content { get; set; }

        public DateTime DateOfCreation { get; set; }
        public DateTime DateOfModification { get; set; }

        [ForeignKey("Commenter")]
        public string CommenterRefId { get; set; }
        public virtual ApplicationUser Commenter { get; set; }

     //  [ForeignKey("ParentComment")]
        public int ParentComment_CommentId { get; set; }
       // public virtual Comment ParentComment {get;set;}
        
        [ForeignKey("Post")]
        public int Post_PostId { get; set; }
        public virtual Post Post { get; set; }
        //not used
        public virtual ICollection<Like> Likes { get; set; } 
        //not used
        public virtual ICollection<Dislike> Dislikes { get; set; } 
        public virtual ICollection<Comment> Replies { get; set; }

        public String howLongAgo ()
        {
            int seconds = (int) DateTime.Now.Subtract(DateOfCreation).TotalSeconds;
          
            if (seconds < 60) return String.Format("{0} seconds ago", seconds);
            if (seconds > 60 && seconds < 3600) return String.Format("{0} minutes ago", (int) seconds/60);
            if (seconds > 3600 && seconds < 3600*24) return String.Format("{0} hours ago", (int)seconds / 3600);
            return String.Format("{0} days ago", (int)seconds / (3600*24));
        }
    }
}