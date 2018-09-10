using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GinaBlog.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        public int PostRefId { get; set; }
        [ForeignKey("PostRefId")]
        virtual public Post Post { get; set; }

        // [ForeignKey("UserId")]
        [ForeignKey("CommenterRefId")] //way2

        virtual public ApplicationUser Commenter { get; set; }
        public String CommenterRefId { get; set; }


        public String Content { get; set; }

        // [ForeignKey("UserId")]
        virtual public ICollection<ApplicationUser> Likes { get; set; }
        // [ForeignKey("UserId")]
        virtual public ICollection<ApplicationUser> Dislikes { get; set; }
        public DateTime DateCommented { get; set; }

        /*
        [ForeignKey("ParentRefId")]
        public virtual Comment ParentComment { get; set; }
        public int ParentRefId { get; set; }
        */
        //virtual public ICollection<Comment> Replies { get; set; }

        public Comment()
        {

        }

    }
}