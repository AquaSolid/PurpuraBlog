using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FilipBlog.Models {
    public class Report {
        public Report() {
            this.Posts = new List<Post>();
        }

        [Key] public int ReportId { get; set; }
        [Required] public string Content { get; set; }

        public DateTime DateOfCreation { get; set; }
        public DateTime DateOfModification { get; set; }

        [ForeignKey("Reporter")]
        public string ReporterRefId { get; set; }
        public virtual ApplicationUser Reporter { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}