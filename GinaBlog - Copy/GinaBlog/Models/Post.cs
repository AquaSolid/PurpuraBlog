using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GinaBlog.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        [Required]
        [StringLength(160)]
        public String Title { get; set; }
        [StringLength(160)]
        public String Subtitle { get; set; }
        [Required]
        public String Content { get; set; }

[Required]
        public String AuthorRefId { get; set; }
      //  
        [ForeignKey("AuthorRefId")] //way2
        virtual public ApplicationUser Author { get; set; }



        public ICollection<String> ImageURLs { get; set; }
        public ICollection<String> VideoURLs { get; set; }


        virtual public ICollection<ApplicationUser> Likes { get; set; }

        virtual public ICollection<ApplicationUser> Dislikes { get; set; }



        [ForeignKey("PostRefId")]
        virtual public IDictionary<Comment, List<Comment>> Comments { get; set; }
        //[Required]
        public DateTime DatePosted { get; set; }
        public Boolean Flagged { get; set; }
        virtual public ICollection<ApplicationUser> ReportedBy { get; set; }
       
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")] //way1
        virtual public Category Category { get; set; }

        public Post()
        {
            ImageURLs = new List<String>();
            VideoURLs = new List<String>();
            Likes = new List<ApplicationUser>();
            Dislikes = new List<ApplicationUser>();
            ReportedBy = new List<ApplicationUser>();
            Comments = new Dictionary<Comment, List<Comment>>();

        }

       
    }
}